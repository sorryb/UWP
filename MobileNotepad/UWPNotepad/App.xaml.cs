﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace UWPNotepad
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        private Frame RootFrame => (Window.Current.Content as Frame) ?? (Window.Current.Content = new Frame()) as Frame;
        public static event EventHandler<Windows.Storage.StorageFile> FileReceived;

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {

            var jumpList = e.TileId == "App" && !string.IsNullOrEmpty(e.Arguments);
            if (jumpList)
            {
                try
                {
                    var file = await Windows.Storage.StorageFile.GetFileFromPathAsync(e.Arguments);
                    if (RootFrame.Content == null)
                        //RootFrame.Navigate(typeof(MainPage), file);
                        RootFrame.Navigate(typeof(MainPage/*RichBox*/), file);
                    else
                        FileReceived?.Invoke(this, file);

                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                //RootFrame.Navigate(typeof(MainPage), e.Arguments);
                RootFrame.Navigate(typeof(MainPage/*RichBox*/), e.Arguments);
            }


            Window.Current.Activate();
        }

        protected override void OnFileActivated(FileActivatedEventArgs args)
        {
            if (!args.Files.Any())
                return;
            if (args.PreviousExecutionState == ApplicationExecutionState.Running)
                FileReceived?.Invoke(this, args.Files.First() as Windows.Storage.StorageFile);
            else
                RootFrame.Navigate(typeof(MainPage/*RichBox*/), args.Files.First());
            //RootFrame.Navigate(typeof(MainPage), args.Files.First());

            Window.Current.Activate();
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
