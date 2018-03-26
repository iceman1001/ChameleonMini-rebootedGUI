using System.Windows.Forms;

namespace ChameleonMiniGUI
{
    public static class ThreadHelperClass
    {
        delegate void SetTextCallback(Form f, Control c, string text);
        /// <summary>
        /// Set text property of various controls
        /// </summary>
        /// <param name="f">The calling form</param>
        /// <param name="c"></param>
        /// <param name="text"></param>
        public static void SetText(Form f, Control c, string text)
        {
            // InvokeRequired required compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (c.InvokeRequired)
            {
                var d = new SetTextCallback(SetText);
                f.Invoke(d, f, c, text);
            }
            else
            {
                c.Text = text;
            }
        }
    }
}