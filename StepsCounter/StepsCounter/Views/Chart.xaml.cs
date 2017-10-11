using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236
//http://social.technet.microsoft.com/wiki/contents/articles/32795.uwp-creating-user-control.aspx
namespace Steps.Views
{
    public sealed partial class Chart : UserControl
    {

        /// <summary>
        /// Model for the app
        /// </summary>
        private MainModel _model = null;
  
        public MainModel Model
        {
            get { return _model; }
            set { _model = value; }
        }

        public double StepGraphWidth;

        public double StepGraphHeight;

        public Chart()
        {
            this.InitializeComponent();

            StepGraph.Loaded += StepGraph_Loaded;
            StepGraph.Width = StepGraphWidth;
            StepGraph.Height = StepGraphHeight;

        }

        /// <summary>
        /// Executes when the Step graph finished loading.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void StepGraph_Loaded(object sender, RoutedEventArgs e)
        {
            _model.SetDimensions(StepGraph.ActualWidth, StepGraph.ActualHeight);
        }

        /// <summary>
        /// Step graph tap event handler
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void StepGraph_Tapped(object sender, TappedRoutedEventArgs e)
        {
            _model.CycleZoomLevel();
        }
    }
}
