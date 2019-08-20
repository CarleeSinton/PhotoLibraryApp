using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Popups;

namespace PhotoLibraryApp
{
    class Picture
    {
        // File to store application's data
        private const string TEXT_FILE_NAME = "Library.txt";

        // Global collection of pictures
        public static ObservableCollection<Picture> Collection = new ObservableCollection<Picture>();

        // Path of the picture file
        public string Path { get; set; }

        // BitmapImage to be used as the souce of the image control
        public BitmapImage ImageSource { get; set; }

        /// <summary>
        /// Adds pictures to the library and updates storage file
        /// </summary>
        /// <param name="picture"></param>
        public static async Task AddPictures(IReadOnlyList<StorageFile> storageFiles)
        {
            foreach (var storageFile in storageFiles)
            {
                
                    // Add picture to the global collection
                    await AddPictureToCollection(storageFile.Path);

                   // Save picture file path in storage data file
                   FileHelper.WriteTextFileAsync(TEXT_FILE_NAME, storageFile.Path);
            }           
        }

        /// <summary>
        /// Loads all pictures from the library data file
        /// </summary>
        /// <returns>A list of pictures</returns>
        public async static Task LoadAllPicturesAsync()
        {
            var content = await FileHelper.ReadTextFileAsync(TEXT_FILE_NAME);

            if (!string.IsNullOrWhiteSpace(content))
            {
                var fileList = content.Split();

                foreach (var file in fileList)
                {
                    if (string.IsNullOrEmpty(file))
                    {
                        continue;
                    }

                    await AddPictureToCollection(file);

                }
            }
        }
        
        private static async Task AddPictureToCollection(string filePath)
        {
            // Create a bitmap
            StorageFile storageFile = null;

            try
            {
                 storageFile = await StorageFile.GetFileFromPathAsync(filePath);
            }
            catch (UnauthorizedAccessException)
            {
                throw new Exception("Access denied to the folder");               
            }

            BitmapImage bitmapImage = new BitmapImage();
            var stream = await storageFile.OpenAsync(Windows.Storage.FileAccessMode.Read);
            bitmapImage.SetSource(stream);

            // Create Picture object
            var pic = new Picture();
            pic.Path = storageFile.Path;
            pic.ImageSource = bitmapImage;

            // Add Picture object to the global observable collection
            Collection.Add(pic);
        }
    }
}