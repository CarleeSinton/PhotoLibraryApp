using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace PhotoLibraryApp
{
    public static class FileHelper
    {
        public async static void WriteTextFileAsync(string filename, string content)
        {
            var storageFolder = ApplicationData.Current.LocalFolder;
            var textFile = await storageFolder.CreateFileAsync(filename,
                CreationCollisionOption.OpenIfExists);

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(textFile.Path, true))
            {
                file.WriteLine(content);
            }

            //var textStream = await textFile.OpenAsync(FileAccessMode.ReadWrite);
            //var textWriter = new StreamWriter(textStream);
            //textWriter..WriteString(content);
            //await textWriter.StoreAsync();
        }

        public async static Task<string> ReadTextFileAsync(string filename)
        {
            var storageFolder = ApplicationData.Current.LocalFolder;

            StorageFile textFile;
            try
            {
                textFile = await storageFolder.GetFileAsync(filename);
            }
            catch (FileNotFoundException)
            {
                return null;
            }
            
            var textStream = await textFile.OpenReadAsync();
            var textReader = new DataReader(textStream);
            var textLength = textStream.Size;
            await textReader.LoadAsync((uint)textLength);
            return textReader.ReadString((uint)textLength);
        }
    }
}
