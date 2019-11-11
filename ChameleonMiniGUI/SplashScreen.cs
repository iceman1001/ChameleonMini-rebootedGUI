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

namespace ChameleonMiniGUI
{
    public partial class SplashScreen : Form
    {
        public SplashScreen()
        {
            InitializeComponent();
            label1.Parent = pictureBox1;
            label1.Text = Properties.Settings.Default.version;
        }
        //Delegate for cross thread call to close
        private delegate void CloseDelegate();

        //The type of form to be displayed as the splash screen.
        private static SplashScreen splashScreen;

        static public void ShowSplashScreen()
        {
            // Make sure it is only launched once.

            if (splashScreen != null)
                return;
            Thread thread = new Thread(new ThreadStart(SplashScreen.ShowForm));
            thread.IsBackground = true;
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        static private void ShowForm()
        {
            splashScreen = new SplashScreen();
            Application.Run(splashScreen);
        }

        static public void CloseForm()
        {
            splashScreen.Invoke(new CloseDelegate(SplashScreen.CloseFormInternal));
        }

        static private void CloseFormInternal()
        {
            splashScreen.Close();
            splashScreen = null;
        }
    }
}
