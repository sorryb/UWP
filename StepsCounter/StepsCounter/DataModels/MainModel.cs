﻿
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;

namespace StepsCounter
{
    /// <summary>
    /// Main model used in the application
    /// </summary>
    public class MainModel : INotifyPropertyChanged
    {
        #region Constants
        /// <summary>
        /// Resolution of the graph in minutes
        /// </summary>
        /// <remarks>Minimum resolution is five minutes</remarks>
        private const int GRAPH_RESOLUTION = 30;

        /// <summary>
        /// Zoom scales (maximum number of steps to show in graph)
        /// </summary>
        private readonly List<uint> ZOOM_SCALES_STEPS = new List<uint>() { 5000, 10000, 20000 };

        /// <summary>
        /// Graph canvas margin in pixels
        /// </summary>
        private const int GRAPH_MARGIN_X = 6;
        #endregion

        #region Events
        /// <summary>
        /// Property changed event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Private members
        /// <summary>
        /// Day offset to today, i.e. 0 = today, 1 = yesterday etc.
        /// </summary>
        private uint _dayOffset;

        /// <summary>
        /// Walking steps count for selected day
        /// </summary>
        private uint _walkingSteps = 0;

        /// <summary>
        /// Running steps count for selected day
        /// </summary>
        private uint _runningSteps = 0;

        /// <summary>
        /// Width of graph canvas in pixels
        /// </summary>
        private double _width;

        /// <summary>
        /// Height of graph canvas in pixels
        /// </summary>
        private double _height;

        /// <summary>
        /// Current zoom level
        /// </summary>
        private int _zoomIndex = 0;

        /// <summary>
        /// Collection of steps for currently selected day
        /// </summary>
        private List<KeyValuePair<TimeSpan, uint>> _steps = new List<KeyValuePair<TimeSpan, uint>>();

        /// <summary>
        /// Loads resources dynamically
        /// </summary>
        private readonly ResourceLoader _resourceLoader = ResourceLoader.GetForCurrentView("Resources");
        #endregion

        /// <summary>
        /// Day offset to today, i.e. 0 = today, 1 = yesterday etc.
        /// </summary>
        public uint DayOffset
        {
            get
            {
                return _dayOffset;
            }
            set
            {
                _dayOffset = value;
                NotifyPropertyChanged("DateString");
                NotifyPropertyChanged("DayOffset");
            }
        }

        /// <summary>
        /// Gets maximum steps range for graph
        /// </summary>
        public string ScaleMax { get { return ZOOM_SCALES_STEPS[_zoomIndex].ToString(); } }

        /// <summary>
        /// Gets half steps range for graph
        /// </summary>
        public string ScaleHalf { get { return (ZOOM_SCALES_STEPS[_zoomIndex] / 2).ToString(); } }

        /// <summary>
        /// Gets margin of the step graph
        /// </summary>
        public string GraphMarginX { get { return (GRAPH_MARGIN_X).ToString(); } }

        /// <summary>
        /// Get the date for the graph
        /// </summary>
        public string DateString
        {
            get
            {
                CultureInfo ci = new CultureInfo("en-GB");
                string format = "D";
                if (DayOffset == 0)
                {
                    return "Today";//_resourceLoader.GetString("TimeWindow.Today");  
                }
                else if (DayOffset == 1)
                {
                    return "Yesterday";//_resourceLoader.GetString("TimeWindow.Yesterday"); 
                }
                else
                {
                    DateTime time = DateTime.Now - TimeSpan.FromDays(DayOffset);
                    return time.ToString(format, ci);
                }
            }
        }

        /// <summary>
        /// Number of walking steps for the day
        /// </summary>
        public uint TotalWalkingSteps
        {
            get
            {
                return _walkingSteps;
            }
            set
            {
                if (value != _walkingSteps)
                {
                    _walkingSteps = value;
                    NotifyPropertyChanged("TotalWalkingSteps");
                    NotifyPropertyChanged("TotalSteps");
                }
            }
        }

        /// <summary>
        /// Number of running steps for the day
        /// </summary>
        public uint TotalRunningSteps
        {
            get
            {
                return _runningSteps;
            }
            set
            {
                if (value != _runningSteps)
                {
                    _runningSteps = value;
                    NotifyPropertyChanged("TotalRunningSteps");
                    NotifyPropertyChanged("TotalSteps");
                }
            }
        }

        /// <summary>
        /// Total number of steps for the day
        /// </summary>
        public uint TotalSteps
        {
            get
            {
                return _walkingSteps + _runningSteps;
            }
        }

        /// <summary>
        /// Total number of calories for steps for the day.
        /// </summary>
        public uint TotalCalories
        {
            get
            {
                uint weight = 75; //pounds
                uint stepsDistance = (uint)(TotalSteps * 0.000473484848485); // miles : 1 mile = 2000 steps; 1 step = 0.002 miles
                return (uint)(0.57 * weight * stepsDistance);
            }
            set
            {
                TotalCalories = value;
                NotifyPropertyChanged("TotalCalories");

            }
        }

        /// <summary>
        /// Total walking distance. In Km.
        /// </summary>
        public string TotalDistance
        {
            get
            {

                double stepsDistanceInMiles = TotalSteps * 0.000473484848485; // miles : 1 mile = 2000 steps; 1 step = 0.002 miles
                return (1.60934 * stepsDistanceInMiles).ToString("F") + " Km";//in Km
            }
            set
            {
                TotalDistance = value;
                NotifyPropertyChanged("TotalDistance");

            }
        }

        /// <summary>
        /// This property makes canvas path that is displayed to the user. 
        /// For instance string could look like "M 6,255 L 10,255  L 14,255  L 18,255  L 23,255...
        /// where M 6, 255 is a start point for path ( 6 pixels from left, 255 pixels down)
        /// L 10, 255 line that goes 4 pixels to right from previous point 
        /// More details here: http://msdn.microsoft.com/en-us/library/ms752293(v=vs.110).aspx
        /// </summary>
        public string PathString
        {
            get
            {
                if (_steps == null) return "";
                String path = "";
                if (_steps.Count > 1)
                {
                    double xOffs = GRAPH_MARGIN_X + ((_width - 2 * GRAPH_MARGIN_X) * _steps[0].Key.TotalMinutes) / (24 * 60);
                    double yOffs = (_height - (Math.Min(ZOOM_SCALES_STEPS[_zoomIndex], _steps[0].Value) * _height / ZOOM_SCALES_STEPS[_zoomIndex]));
                    path = "M " + (uint)xOffs + "," + (uint)yOffs;
                    foreach (var item in _steps)
                    {
                        uint stepcount = Math.Min(ZOOM_SCALES_STEPS[_zoomIndex], item.Value);
                        xOffs = GRAPH_MARGIN_X + ((_width - 2 * GRAPH_MARGIN_X) * item.Key.TotalMinutes) / (24 * 60);
                        yOffs = _height - (stepcount * _height / ZOOM_SCALES_STEPS[_zoomIndex]);
                        path += " L " + (uint)xOffs + "," + (uint)yOffs;
                    }
                }
                return path;
            }
        }

        /// <summary>
        /// Increases day offset
        /// </summary>
        /// <returns>Asynchronous task</returns>
        public async Task IncreaseDayOffsetAsync()
        {
            if (DayOffset < 10)//6
            {
                DayOffset++;
                await UpdateAsync();
            }
        }

        /// <summary>
        /// Decreases day offset
        /// </summary>
        /// <returns>Asynchronous task</returns>
        public async Task DecreaseDayOffsetAsync()
        {
            if (DayOffset != 0)
            {
                DayOffset--;
                await UpdateAsync();
            }
        }

        /// <summary>
        /// Updates model
        /// </summary>
        /// <returns>Asynchronous task</returns>
        public async Task UpdateAsync()
        {
            _steps = null;
            try
            {
                var stepCount = await App.Engine.GetTotalStepCountAsync(DateTime.Today - TimeSpan.FromDays(DayOffset));
                TotalRunningSteps = stepCount.RunningCount;
                TotalWalkingSteps = stepCount.WalkingCount;

                _steps = await App.Engine.GetStepsCountsForDay(DateTime.Today - TimeSpan.FromDays(DayOffset), GRAPH_RESOLUTION);
                NotifyPropertyChanged("");
            }
            catch (Exception)
            {
                TotalRunningSteps = 0;
                TotalWalkingSteps = 0;
                NotifyPropertyChanged("");
            }
        }

        /// <summary>
        /// Sets graph dimensions
        /// </summary>
        /// <param name="width">Width of graph canvas in pixels</param>
        /// <param name="height">Height of graph canvas in pixels</param>
        public void SetDimensions(double width, double height)
        {
            _width = width;
            _height = height;
            NotifyPropertyChanged("");
        }

        /// <summary>
        /// Cycles zoom level
        /// </summary>
        public void CycleZoomLevel()
        {
            _zoomIndex = (_zoomIndex + 1) % ZOOM_SCALES_STEPS.Count;
            NotifyPropertyChanged("");
        }

        /// <summary>
        /// Executes when a property has changed
        /// </summary>
        /// <param name="propertyName">Property which will be changed</param>
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
