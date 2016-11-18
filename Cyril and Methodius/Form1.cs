﻿using System;
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
            fDialog.Title = "Open txt File";
            fDialog.Filter = "Txt Files|*.txt";
            fDialog.InitialDirectory = @"C:\";
            int size = -1;
            DialogResult result = fDialog.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string file = fDialog.FileName;
                label1.Text = file;
            }
            Console.WriteLine(size); // <-- Shows file size in debugging mode.
            Console.WriteLine(result); // <-- For debugging use.
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string linkText = textBox1.Text;
            string httpCheck = new string(linkText.Take(7).ToArray());
            string httpsCheck = new string(linkText.Take(8).ToArray());
            string wwwCheck = new string(linkText.Take(3).ToArray());
            if (String.Compare("www", wwwCheck, true) == 0 || String.Compare("http://", httpCheck, true) == 0 || String.Compare("https://", httpsCheck) == 0)
            {
                if (String.Compare("www", wwwCheck, true) == 0)
                {
                    linkText = "http://" + linkText;
                }
            }
            else
            {
                linkText = "http://www." + linkText;
            }
            if (linkText == String.Empty)
            {
                linkLabel1.Text = "No link inserted.";
            }
            else if (Helper.CheckURLValid(linkText))
            {
                linkLabel1.Text = linkText;
                //TODO
            }
            else
            {
                linkLabel1.Text = "Invalid link inserted.";
            }
        }
    }
}
