using System;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.ApplicationModel.Resources;
using Windows.UI;
using Windows.UI.StartScreen;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

using Lumia.Sense;
using Windows.UI.Xaml.Media;
using System.Globalization;
using Windows.Foundation;
using Windows.UI.Popups;

namespace StepsCounter
{
    public sealed partial class MainPageChart : Page
    {
        #region Private constants
        /// <summary>
        /// Tile ID
        /// </summary>
        private const string TILE_ID = "SecondaryTile.Steps";

        /// <summary>
        /// Target daily step count
        /// </summary>
        private const uint TARGET_STEPS = 10000;

        /// <summary>
        /// Number of large meter images
        /// </summary>
        private const uint NUM_LARGE_METER_IMAGES = 9;

        /// <summary>
        /// Number of small meter images
        /// </summary>
        private const uint NUM_SMALL_METER_IMAGES = 4;
        #endregion

        #region Private members
        /// <summary>
        /// Model for the app
        /// </summary>
        private MainModel _model = null;

        /// <summary>
        /// Synchronization object
        /// </summary>
        private SemaphoreSlim _sync = new SemaphoreSlim(1);

        /// <summary>
        /// Timer to update step counts periodically
        /// </summary>
        private DispatcherTimer _pollTimer;

        /// <summary>
        /// Loads resources dynamically
        /// </summary>
        private readonly ResourceLoader _resourceLoader = ResourceLoader.GetForCurrentView("Resources");

        #endregion

        int x1, x2;
        public MainPageChart()
        {
            this.InitializeComponent();

            _model = new MainModel();
            LayoutRoot.DataContext = _model;

            Chart.Model = _model;
            Chart.StepGraphHeight = ChartContainer.Height;
            Chart.StepGraphWidth = ChartContainer.Width;

            ManipulationMode = ManipulationModes.TranslateX;//| ManipulationModes.TranslateY;
            ManipulationStarted += (s, e) => x1 = (int)e.Position.X;
            ManipulationCompleted += (s, e) => 
            {
                x2 = (int)e.Position.X;
                if (x1 > x2)
                {
                    //System.Diagnostics.Debug.WriteLine("swipe left");
                    ApplicationBarIconButtonNext_Click(nextButton, e);
                }
                else
                {
                    // System.Diagnostics.Debug.WriteLine("swipe right");
                    ApplicationBarIconButtonBack_Click(backButton, e);
                }
            };
        }

        #region NavigationHelper registration
        /// <summary>
        /// Called when a page is no longer the active page in a frame.
        /// </summary>
        /// <param name="e">Provides data for non-cancelable navigation events</param>
        protected async override void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (_pollTimer != null)
            {
                _pollTimer.Stop();
                _pollTimer = null;
            }
            await App.Engine.DeactivateAsync();
        }

        /// <summary>
        /// Called when navigating to this page
        /// </summary>
        /// <param name="e">Event arguments</param>
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            await App.Engine.ActivateAsync();

            UpdateMenuAndAppBarIcons();

            await _sync.WaitAsync();
            try
            {
                await _model.UpdateAsync();
            }
            finally
            {
                _sync.Release();
            }

            // Start poll timer to update steps counts periodically
            if (_pollTimer == null)
            {
                _pollTimer = new DispatcherTimer();
                _pollTimer.Interval = TimeSpan.FromSeconds(5);
                _pollTimer.Tick += PollTimerTick;
                _pollTimer.Start();
            }
        }

        #endregion

        /// <summary>
        /// Step counter poll timer callback
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private async void PollTimerTick(object sender, object e)
        {
            await _sync.WaitAsync();
            try
            {
                // No need to update if we are not looking at today
                if (_model.DayOffset == 0)
                {
                    await _model.UpdateAsync();
                }
            }
            finally
            {
                _sync.Release();
            }
        }


        /// <summary>
        /// Decrease opacity of the command bar when closed
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">Event arguments</param>
        private void CommandBar_Closed(object sender, object e)
        {
            cmdBar.Opacity = 0.5;
        }

        /// <summary>
        /// Increase opacity of command bar when opened
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">Event arguments</param>
        private void CommandBar_Opened(object sender, object e)
        {
            cmdBar.Opacity = 1;
        }

        /// <summary>
        /// About menu item click event handler
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AboutPage));
        }

        /// <summary>
        /// Removes background task
        /// </summary>
        /// <param name="taskName">Name of task to be removed</param>
        /// <returns>Asynchronous task</returns>
        private async static Task RemoveBackgroundTaskAsync(string taskName)
        {
            BackgroundAccessStatus result = await BackgroundExecutionManager.RequestAccessAsync();
            if (result != BackgroundAccessStatus.Denied)
            {
                // Remove previous registration
                foreach (var task in BackgroundTaskRegistration.AllTasks)
                {
                    if (task.Value.Name == taskName)
                    {
                        task.Value.Unregister(true);
                    }
                }
            }
        }

        /// <summary>
        /// Registers background task
        /// </summary>
        /// <param name="trigger">Task trigger</param>
        /// <param name="taskName">Task name</param>
        /// <param name="taskEntryPoint">Task entry point</param>
        /// <returns>Asynchronous task</returns>
        private async static Task RegisterBackgroundTaskAsync(IBackgroundTrigger trigger, String taskName, String taskEntryPoint)
        {
            BackgroundAccessStatus result = await BackgroundExecutionManager.RequestAccessAsync();
            if (result != BackgroundAccessStatus.Denied)
            {
                await RemoveBackgroundTaskAsync(taskName);

                // Register task
                BackgroundTaskBuilder myTaskBuilder = new BackgroundTaskBuilder();
                myTaskBuilder.SetTrigger(trigger);
                myTaskBuilder.TaskEntryPoint = taskEntryPoint;
                myTaskBuilder.Name = taskName;
                BackgroundTaskRegistration myTask = myTaskBuilder.Register();
            }
        }

        /// <summary>
        /// Creates or removes a secondary tile
        /// </summary>
        /// <param name="removeTile"><c>true</c> to remove tile, <c>false</c> to create tile</param>
        /// <returns>Asynchronous task</returns>
        private async Task CreateOrRemoveTileAsync(bool removeTile)
        {
            if (!removeTile)
            {
                var steps = await App.Engine.GetTotalStepCountAsync(DateTime.Now.Date);
                uint stepCount = steps.TotalCount;
                uint meter = (NUM_SMALL_METER_IMAGES - 1) * Math.Min(stepCount, TARGET_STEPS) / TARGET_STEPS;
                uint meterSmall = (NUM_LARGE_METER_IMAGES - 1) * Math.Min(stepCount, TARGET_STEPS) / TARGET_STEPS;
                try
                {
                    var secondaryTile = new SecondaryTile(TILE_ID, "Steps", "/MainPage.xaml", new Uri("ms-appx:///Assets/Tiles/square" + meterSmall + ".png", UriKind.Absolute), TileSize.Square150x150);
                    secondaryTile.VisualElements.Square71x71Logo = new Uri("ms-appx:///Assets/Tiles/small_square" + meterSmall + ".png", UriKind.Absolute);
                    secondaryTile.VisualElements.ShowNameOnSquare150x150Logo = true;
                    secondaryTile.VisualElements.ShowNameOnSquare310x310Logo = false;
                    secondaryTile.VisualElements.ShowNameOnWide310x150Logo = false;
 
                    string colorcode = "#FFFB28E8";
                    int argb = Int32.Parse(colorcode.Replace("#", ""), NumberStyles.HexNumber);
                    secondaryTile.VisualElements.BackgroundColor = Color.FromArgb((byte)((argb & -16777216) >> 0x18),
                                          (byte)((argb & 0xff0000) >> 0x10),
                                          (byte)((argb & 0xff00) >> 8),
                                          (byte)(argb & 0xff));
                    secondaryTile.RoamingEnabled = false;
                    await secondaryTile.RequestCreateAsync();
                }
                catch (Exception)
                {
                }
            }
            else
            {
                SecondaryTile secondaryTile = new SecondaryTile(TILE_ID);
                await secondaryTile.RequestDeleteAsync();
                UpdateMenuAndAppBarIcons();
            }
        }

        /// <summary>
        /// Updates menu and app bar icons
        /// </summary>
        private void UpdateMenuAndAppBarIcons()
        {
            // Show unpin or pin button
            if (!SecondaryTile.Exists(TILE_ID))
            {
                var icon = new BitmapIcon();
                icon.UriSource = new Uri("ms-appx:///Assets/Images/pin-48px.png", UriKind.Absolute);
                pinButton.Icon = icon;
                pinButton.Label = _resourceLoader.GetString("PinButton/Label");
            }
            else
            {
                var icon = new BitmapIcon();
                icon.UriSource = new Uri("ms-appx:///Assets/Images/unpin-48px.png", UriKind.Absolute);
                pinButton.Icon = icon;
                pinButton.Label = _resourceLoader.GetString("UnpinLabel");
            }

            backButton.IsEnabled = _model.DayOffset != 10;//6
            nextButton.IsEnabled = _model.DayOffset != 0;
        }

        /// <summary>
        /// Creates secondary tile if it is not yet created or removes the tile if it already exists.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private async void ApplicationBar_PinTile(object sender, RoutedEventArgs e)
        {
            bool removeTile = SecondaryTile.Exists(TILE_ID);
            if (removeTile)
            {
                await RemoveBackgroundTaskAsync("StepTriggered");
            }
            else
            {
                ApiSupportedCapabilities caps = await SenseHelper.GetSupportedCapabilitiesAsync();
                // Use StepCounterUpdate to trigger live tile update if it is supported. Otherwise we use time trigger
                if (caps.StepCounterTrigger)
                {
                    var myTrigger = new DeviceManufacturerNotificationTrigger(SenseTrigger.StepCounterUpdate, false);
                    await RegisterBackgroundTaskAsync(myTrigger, "StepTriggered", "BackgroundTasks.StepTriggerTask");
                }
                else
                {
                    BackgroundAccessStatus status = await BackgroundExecutionManager.RequestAccessAsync();
                    IBackgroundTrigger trigger = new TimeTrigger(15, false);
                    await RegisterBackgroundTaskAsync(trigger, "StepTriggered", "BackgroundTasks.StepTriggerTask");
                }
            }
            await CreateOrRemoveTileAsync(removeTile);
        }

        /// <summary>
        /// Next day button click event handler
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private async void ApplicationBarIconButtonNext_Click(object sender, RoutedEventArgs e)
        {
            await _sync.WaitAsync();
            try
            {
                await _model.DecreaseDayOffsetAsync();
                UpdateMenuAndAppBarIcons();
            }
            finally
            {
                _sync.Release();
            }
        }

        /// <summary>
        /// Previous day button click event handler
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private async void ApplicationBarIconButtonBack_Click(object sender, RoutedEventArgs e)
        {
            await _sync.WaitAsync();
            try
            {
                await _model.IncreaseDayOffsetAsync();
                UpdateMenuAndAppBarIcons();
            }
            finally
            {
                _sync.Release();
            }
        }

    
    }
}
