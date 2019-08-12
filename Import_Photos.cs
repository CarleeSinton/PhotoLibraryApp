//using Microsoft.Azure.Amqp.Framing;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Data;
//using System.Drawing;
//using System.Text;
//using System.Windows.Forms;

//namespace Photo_Library_App
//{
//    public partial class Import_Photos: Form
//    {
//        public Import_Photos()
//        {
//            InitializeComponent();
//        }
//        private void Import_Photos_Button_Click(object sender, RoutedEventArgs e)
//        {
//            Open opf = new Open();
//            opf.Filter = "Choose Image(*.jpg;*.png;*.gif)|*.jpg;*png;*.gif";
//            if(opf.ShowDialog()== DialogResult.OK)
//            {
//                pictureBox1.Impage = BadImageFormatException.FromFile(opf.FileName);
//            }
//        }
//    }
