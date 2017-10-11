using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Windows.Storage.Pickers;

namespace UWPNotepad.ViewModel
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public MainPageViewModel()
        {
            App.FileReceived += async (s, e) => File = await _fileService.LoadAsync(e);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private Models.FileInfo _file;

        public Models.FileInfo File
        {

            get { return _file; }

            set
            {
                _file = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(File)));
            }

        }

        public List<string> FileProperties
        {

            get { return _fileProperties; }

            set
            {
                _fileProperties = value ;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FileProperties)));
            }

        }
        private List<string> _fileProperties = new List<string>() { "Proprietatile fisierului:"};

        Services.FileService _fileService = new Services.FileService();
        Services.ToastService _toastService = new Services.ToastService();

        public async void Save()
        {
            try
            {
                if (_file != null)
                {
                    await _fileService.SaveAsync(_file);
                    _toastService.ShowToast(_file, "File  succesfully saved.");
                }
                else
                {
                    var savePicker = new Windows.Storage.Pickers.FileSavePicker();
                    savePicker.SuggestedStartLocation =
                        Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
                    // Dropdown of file types the user can save the file as
                    savePicker.FileTypeChoices.Add("Plain Text", new List<string>() { ".txt" });
                    // Default file name if the user does not type one in or select a file to replace
                    savePicker.SuggestedFileName = "New Document";

                    Windows.Storage.StorageFile file = await savePicker.PickSaveFileAsync();
                    if (file != null)
                    {
                        // Prevent updates to the remote version of the file until
                        // we finish making changes and call CompleteUpdatesAsync.
                        Windows.Storage.CachedFileManager.DeferUpdates(file);
                        // write to file
                        await Windows.Storage.FileIO.WriteTextAsync(file, file.Name);
                        // Let Windows know that we're finished changing the file so
                        // the other app can update the remote version of the file.
                        // Completing updates may require Windows to ask for user input.
                        Windows.Storage.Provider.FileUpdateStatus status =
                            await Windows.Storage.CachedFileManager.CompleteUpdatesAsync(file);
                        if (status == Windows.Storage.Provider.FileUpdateStatus.Complete)
                        {
                          await new Windows.UI.Popups.MessageDialog("File " + file.Name + " was saved.").ShowAsync();
                        }
                        else
                        {
                            await new Windows.UI.Popups.MessageDialog("File " + file.Name + " couldn't be saved.").ShowAsync();
                        }
                    }
                    else
                    {
                        await new Windows.UI.Popups.MessageDialog("Operation cancelled.").ShowAsync();
                    }

                }
            }
            catch (Exception ex)
            {
                _toastService.ShowToast(_file, "Save failed. Error : " + ex.Message);
            }
        }

        public async void Open()
        {
            var picker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.List,
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary,

            };
            picker.FileTypeFilter.Add(".txt");

            var file = await picker.PickSingleFileAsync();
            if (file == null)
                await new Windows.UI.Popups.MessageDialog("No file selected !").ShowAsync();
            else
                File = await _fileService.LoadAsync(file);


            List < string > fileProperties = new List<string>();

            fileProperties.Add("File name: " + file.Name);
            fileProperties.Add("File type: " + file.FileType);

            // Get file's basic properties.
            Windows.Storage.FileProperties.BasicProperties basicProperties =
                await file.GetBasicPropertiesAsync();
            string fileSize = string.Format("{0:n0}", basicProperties.Size);
            fileProperties.Add("File size: " + fileSize + " bytes");
            fileProperties.Add("Date modified: " + basicProperties.DateModified);

            // Define property names to be retrieved for Extended properties.
            const string dateAccessedProperty = "System.DateAccessed";
            const string fileOwnerProperty = "System.FileOwner";

            var propertyNames = new List<string>();
            propertyNames.Add(dateAccessedProperty);
            propertyNames.Add(fileOwnerProperty);

            // Get extended properties.
            IDictionary<string, object> extraProperties =
                await file.Properties.RetrievePropertiesAsync(propertyNames);

            // Get date-accessed property.
            var propValue = extraProperties[dateAccessedProperty];
            if (propValue != null)
            {
                fileProperties.Add("Date accessed: " + propValue);
            }

            // Get file-owner property.
            propValue = extraProperties[fileOwnerProperty];
            if (propValue != null)
            {
                fileProperties.Add("File owner: " + propValue);
            }

            FileProperties = fileProperties;
        }
    }
}
