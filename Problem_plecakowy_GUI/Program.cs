using System;
using System.Windows.Forms;

namespace Problem_plecakowy_GUI
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1()); // Uruchomienie Form1 zamiast MainForm
        }
    }
}
