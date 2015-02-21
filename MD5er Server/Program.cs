using System;
using System.Collections;
using System.Windows.Forms;

namespace MD5er_Server
{
    static class Program
    {
        private static ArrayList myData = null;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Program.myData = new ArrayList();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            m_form = new frmMain();
            Application.Run(m_form);

        }

        public static ArrayList Data
        {
            get { return Program.myData; }
        }

        public static long StoreRecover
        {
            get { return Properties.Settings.Default.StoreProgress; }
            set
            {
                    Properties.Settings.Default.StoreProgress = value;
                    Properties.Settings.Default.Save();
            }
        }

        private static frmMain m_form;

        public static frmMain MainForm
        {
            get { return m_form; }
        }

    }
}
