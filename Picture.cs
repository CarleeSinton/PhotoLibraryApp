using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

namespace PhotoLibraryApp
{
    class Picture
    {
        // File to store application's data
        private const string TEXT_FILE_NAME = "Library.txt";

        // Path of the picture file
        public string Path { get; set; }

        // BitmapImage to be used as the souce of the image control
        public BitmapImage ImageSource { get; set; }

        /// <summary>
        /// Adds pictures to the library and updates storage file
        /// </summary>
        /// <param name="picture"></param>
        public static void AddPictures(List<StorageFile> pictureFiles)
        {
            // todo: add the pictures to the global library observable collection

            // update the storage
            foreach (var file in pictureFiles)
            {
                var pictureData = file.Path;
                FileHelper.WriteTextFileAsync(TEXT_FILE_NAME, pictureData);
            }           
        }

        /// <summary>
        /// Gets all pictures from the library data file
        /// </summary>
        /// <returns>A list of pictures</returns>
        public async static Task<ICollection<Picture>> GetAllPicturesAsync()
        {
            var pictures = new List<Picture>();
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

                    var storageFile = await StorageFile.GetFileFromPathAsync(file);
                    BitmapImage bitmapImage = new BitmapImage();
                    var stream = await storageFile.OpenAsync(Windows.Storage.FileAccessMode.Read);
                    bitmapImage.SetSource(stream);

                    var pic = new Picture();
                    pic.Path = storageFile.Path;
                    pic.ImageSource = bitmapImage;

                    pictures.Add(pic);
                }
            }

            return pictures;
        }
    }
}