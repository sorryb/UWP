//---------------------------------------------------------------------------
//
// <copyright file="DoMoreDetailPage.xaml.cs" company="Microsoft">
//    Copyright (C) 2015 by Microsoft Corporation.  All rights reserved.
// </copyright>
//
// <createdOn>10/22/2015 10:25:19 AM</createdOn>
//
//---------------------------------------------------------------------------

using System;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using AppStudio.DataProviders.Instagram;
using Windows10News.Sections;
using Windows10News.Services;
using Windows10News.ViewModels;

namespace Windows10News.Views
{
    public sealed partial class DoMoreDetailPage : Page
    {
        private DataTransferManager _dataTransferManager;

        public DoMoreDetailPage()
        {
            this.ViewModel = new DetailViewModel<InstagramDataConfig, InstagramSchema>(new DoMoreConfig());
            this.ViewModel.SupportSlideShow = true;
            this.ViewModel.SupportFullScreen = true;

            this.InitializeComponent();
            new Microsoft.ApplicationInsights.TelemetryClient().TrackPageView(this.GetType().FullName);
        }

        public DetailViewModel<InstagramDataConfig, InstagramSchema> ViewModel { get; set; }        

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            await this.ViewModel.LoadDataAsync(e.Parameter as ItemViewModel);

            _dataTransferManager = DataTransferManager.GetForCurrentView();
            _dataTransferManager.DataRequested += OnDataRequested;
            FullScreenService.CurrentPageSupportFullScreen = true;

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _dataTransferManager.DataRequested -= OnDataRequested;
            FullScreenService.CurrentPageSupportFullScreen = false;

            base.OnNavigatedFrom(e);
        }

        private void OnDataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            ViewModel.ShareContent(args.Request);
        }

        private void AppBarButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            AppBarButton button = sender as AppBarButton;
            int newFontSize = Int32.Parse(button.Tag.ToString());
            mainPanel.BodyFontSize = newFontSize;
            mainPanel.UpdateFontSize();
        }
    }
}