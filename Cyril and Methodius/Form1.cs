using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cyril_and_Methodius.Model;
using System.IO;

namespace Cyril_and_Methodius
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            // test
            //string a = "Тест ћирилична метода, са још по неким карактером. ХЕ ЏА џ xzwq QQ // џ њ љ Џ Њ Љ";
            //string b = "Теst latinična metoda, sa još po nekim karakterom. Xe dža Dž DŽ xzwq QQ // dŽ dž Dž DŽ Nj nj NJ nJ lj lJ LJ Lj";
            //Translator t = new Translator();
            //string outS = Translator.Translate(a, true);
            //string outS2 = Translator.Translate(b, false);
            //Console.Read();
            // test
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fDialog = new OpenFileDialog();
            fDialog.Title = "Open XML/UML File";
            fDialog.Filter = "XML Files|*.txt";
            fDialog.InitialDirectory = @"C:\";
            int size = -1;
            DialogResult result = fDialog.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string file = fDialog.FileName;
                try
                {
                    string text = File.ReadAllText(file);
                    size = text.Length;
                }
                catch (IOException)
                {
                }
            }
            Console.WriteLine(size); // <-- Shows file size in debugging mode.
            Console.WriteLine(result); // <-- For debugging use.
        }
    }
}
