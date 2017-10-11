
using System;
using System.IO;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Storage;
using System.Xml;
using System.Xml.Linq;

/// <summary>
/// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556
/// </summary>
namespace StepsCounter
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AboutPage : Page
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public AboutPage()
        {
            this.InitializeComponent();
            Loaded += AboutPage_Loaded;
        }

        /// <summary>
        /// Loaded event raised after the component is initialized
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">Event arguments</param>
        async void AboutPage_Loaded(object sender, RoutedEventArgs e)
        {
            string version = "";
            var uri = new System.Uri("ms-appx:///AppxManifest.xml");
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(uri);
            using (var rastream = await file.OpenReadAsync())
            using (var appManifestStream = rastream.AsStreamForRead())
            {
                using (var reader = XmlReader.Create(appManifestStream, new XmlReaderSettings { IgnoreWhitespace = true, IgnoreComments = true }))
                {
                    var doc = XDocument.Load(reader);
                    var app = doc.Descendants(doc.Root.Name.Namespace + "Identity").FirstOrDefault();
                    if (app != null)
                    {
                        var versionAttribute = app.Attribute("Version");
                        if (versionAttribute != null)
                        {
                            version = versionAttribute.Value;
                        }
                    }
                }
            }
            VersionNumber.Text = version;
        }
    }
}