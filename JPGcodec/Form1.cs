using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Drawing.Imaging;

namespace JPGcodec
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //CompressImage(Image.FromFile(Application.StartupPath + @"\mySourcePicture.jpg"), 30, Application.StartupPath + @"\myCompressedPicture.jpg");
            //CompressImage(Image.FromFile(@"C:\Users\Benedict\Pictures\Saved Pictures\SoftenFilter.jpg"), 30, @"C:\Users\Benedict\Pictures\Saved Pictures\MeSoftenFilterCompression.jpg");
        }
        private void CompressImage(Image sourceImage, int imageQuality, string savePath)
        {
            try
            {
                //Create an ImageCodecInfo-object for the codec information
                ImageCodecInfo jpegCodec = null;

                //Set quality factor for compression
                EncoderParameter imageQualitysParameter = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, imageQuality);

                //List all avaible codecs (system wide)
                ImageCodecInfo[] allCodecs = ImageCodecInfo.GetImageEncoders();

                EncoderParameters codecParameter = new EncoderParameters(1);
                codecParameter.Param[0] = imageQualitysParameter;

                //Find and choose JPEG codec
                for (int i = 0; i < allCodecs.Length; i++)
                {
                    if (allCodecs[i].MimeType == "image/jpeg")
                    {
                        jpegCodec = allCodecs[i];
                        break;
                    }
                }

                //Save compressed image
                sourceImage.Save(savePath, jpegCodec, codecParameter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select an image file.";
            ofd.Filter = "Jpeg Images(*.jpg)|*.jpg";
            ofd.Filter += "|Png Images(*.png)|*.png";
            ofd.Filter += "|Bitmap Images(*.bmp)|*.bmp";
            ofd.Filter += "|All(*.JPG, *.PNG, *.bmp)| *.JPG; *.PNG; *.bmp";
            ofd.FilterIndex = 1;

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtFilePthOrig.Text = ofd.FileName;
                Image img = Image.FromFile(txtFilePthOrig.Text);
                pictureBox.Image = img;
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtFilePthOrig.Text == "")
            {
                MessageBox.Show("Please load image first!");
            }
            else if (txtFilePthOrig.Text.Contains(".jpg")) 
            {
                CompressImage(Image.FromFile(txtFilePthOrig.Text), 30, txtFilePthOrig.Text.Insert(txtFilePthOrig.Text.Length - 4, " JPEG Compressed Image"));
                MessageBox.Show("Image compressed!");
            }
            else
            {
                string x = txtFilePthOrig.Text.Insert(txtFilePthOrig.Text.Length - 4, " JPEG Compressed Image");
                string y = "abcdefg";
                //x.Substring(0, x.Length-4)+".jpg"
                //MessageBox.Show(txtFilePthOrig.Text.Insert(txtFilePthOrig.Text.Length - 4, " Compressed Image").Substring(0, x.Length - 4) + ".jpg");

                CompressImage(Image.FromFile(txtFilePthOrig.Text), 30, txtFilePthOrig.Text.Insert(txtFilePthOrig.Text.Length - 4, " JPEG Compressed Image").Substring(0, x.Length - 4) + ".jpg");
                MessageBox.Show("Image compressed!");
            }
        }
    }
}
