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
using Windows.Storage;
using Windows.System;
using Windows.UI.Xaml.Shapes;
using System.Diagnostics;
using Windows.Storage.Pickers;
using Xamarin.Forms;
using System.Text;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage.Streams;
using ImageSource = Xamarin.Forms.ImageSource;
using System.Collections.ObjectModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Photo_Library_App
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Windows.UI.Xaml.Controls.Page
    {

        public MainPage()
        {
            InitializeComponent();
        }

    
       // ObservableCollection<BitmapImage> pictures = new ObservableCollection<BitmapImage>();


        

        //public ObservableCollection<BitmapImage> CollectionsPageImage { get => collectionsPageImage; set => collectionsPageImage = value; }

        public async void Import_Photos_Button_ClickAsync(object sender, RoutedEventArgs e)
        {
            
            {
                var picker = new Windows.Storage.Pickers.FileOpenPicker();
                picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
                picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
                picker.FileTypeFilter.Add(".jpg");
                picker.FileTypeFilter.Add(".jpeg");
                picker.FileTypeFilter.Add(".png");
                picker.FileTypeFilter.Add(".bmp");

                //Xamarin.Forms.StreamImageSource


                var files = await picker.PickMultipleFilesAsync();
              

                if (files.Count > 0)
                {

                    // Application now has read/write access to the picked file(s)
                    foreach (Windows.Storage.StorageFile file in files)
                    {
                        using(Windows.Storage.Streams.IRandomAccessStream fileStream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read))
                        {
                            Windows.UI.Xaml.Media.Imaging.BitmapImage bitmapImage = new Windows.UI.Xaml.Media.Imaging.BitmapImage();
                            bitmapImage.SetSource(fileStream);
                            image1.Source = bitmapImage;
                            

                        }

                       
                    }
                    
                }
                else
                {
                    //
                }

            }

           
        }
    }
}
        
