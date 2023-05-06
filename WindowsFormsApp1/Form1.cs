using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace Player
{
    public partial class VideoForm : Form
    {
        VideoCapture capture;
        public VideoForm()
        {
            InitializeComponent();
            new Thread(() => Hello()).Start();
            // Hello();
        }
        private void Hello()
        {
            try
            {
                // rtsp://Admin:1234@192.168.15.52:554/snl/live/1/1/bPvJ5dg=-HK2XuA==
                // rtsp://Admin:admin321@192.168.1.5:554/h264
                capture = new VideoCapture("rtsp://Admin:1234@192.168.0.157:554/snl/live/1/1/bPvJ5dg=-HK2XuA==");
                capture.ImageGrabbed += CurrentDevice_ImageGrabbed;
                capture.SetCaptureProperty(CapProp.Fps, 0);
                capture.Start();
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex);
            }
        }

        private void CurrentDevice_ImageGrabbed(object sender, EventArgs e)
        {
            try
            {
                Console.WriteLine(this.Size);
                Mat m = new Mat(this.Size, DepthType.Cv8U, 3);
                capture.Retrieve(m, 0);
                pictureBox1.Image = BitmapExtension.ToBitmap(m);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
