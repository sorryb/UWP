using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.AccessCache;

/// <summary>
/// Services are that abstraction of logic that work as utilities for ViewModels.
/// </summary>
namespace UWPNotepad.Services
{
    class FileService
    {
        public async Task SaveAsync(Models.FileInfo model)
        {
            if (model != null)
                await Windows.Storage.FileIO.WriteTextAsync(model.Ref, model.Text);
        }
        public async Task<Models.FileInfo> LoadAsync(StorageFile file)
        {
            //add to jump list
            if (Windows.UI.StartScreen.JumpList.IsSupported())
            {
                var jumpList = await Windows.UI.StartScreen.JumpList.LoadCurrentAsync();
                jumpList.SystemGroupKind = Windows.UI.StartScreen.JumpListSystemGroupKind.None;
                while (jumpList.Items.Count > 4)
                    jumpList.Items.RemoveAt(jumpList.Items.Count - 1);
                if (!jumpList.Items.Any(x => x.Arguments == file.Path))
                {
                    var jumpItem = Windows.UI.StartScreen.JumpListItem.CreateWithArguments(file.Path, file.DisplayName);
                    jumpList.Items.Add(jumpItem);
                }

                await jumpList.SaveAsync();
            }

            //add to MRU
            var mruList = Windows.Storage.AccessCache.StorageApplicationPermissions.MostRecentlyUsedList;
            while (mruList.Entries.Count >= mruList.MaximumItemsAllowed)
                mruList.Remove(mruList.Entries.First().Token);
            if (!mruList.Entries.Any(x => x.Metadata == file.Path))
                mruList.Add(file, file.Path);
            var futureAccessList = StorageApplicationPermissions.FutureAccessList;
            futureAccessList.Add(file);


            return new Models.FileInfo
            {
                Text = await Windows.Storage.FileIO.ReadTextAsync(file),
                Name = file.Name,
                Ref = file,
                Path = file.Path
            };
        }


    }
}
