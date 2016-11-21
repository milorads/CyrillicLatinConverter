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
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Cyril_and_Methodius
{
    public partial class Form1 : Form
    {
        public string applicationName = "Cyril & Methodius - Converter";
        private Translator _translator = new Translator();
        private FileHandler _filer = new FileHandler();
        private string filesystemFileName;
        private string webFileLocation;
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
            Dictionary<DropdownOptions, string> test = new Dictionary<DropdownOptions, string>();
            test.Add(DropdownOptions.Filesystem, "Filesystem");
            test.Add(DropdownOptions.Web, "Web");
            test.Add(DropdownOptions.Input, "Input");
            comboBox1.DataSource = new BindingSource(test, null);
            comboBox1.DisplayMember = "Value";
            comboBox1.ValueMember = "Key";

        }

        public enum DropdownOptions{
            Filesystem,
            Web,
            Input
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
                filesystemFileName = fDialog.FileName;
                ToolTip ToolTip1 = new ToolTip();
                ToolTip1.SetToolTip(label4, filesystemFileName);
                label4.Text = Path.GetFileName(filesystemFileName);
            }
            //Console.WriteLine(size); // <-- Shows file size in debugging mode.
            //Console.WriteLine(result); // <-- For debugging use.
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
                if (Helper.RemoteDestinationExists(linkText))
                {
                    if (Helper.GetPageTitle(linkText) != "")
                    {
                        linkLabel1.Text = Helper.GetPageTitle(linkText);
                        LinkLabel.Link link = new LinkLabel.Link();
                        link.LinkData = linkText;
                        for (int i = 0; i < linkLabel1.Links.Count; i++)
                        {
                            linkLabel1.Links.RemoveAt(i);
                        }
                        linkLabel1.Links.Add(link);
                    }
                    else
                    {
                        linkLabel1.Text = linkText;
                    }

                    webFileLocation = linkText;
                }
                else
                {
                    linkLabel1.Text = "Targeted url was unreachable.";
                }
            }
            else
            {
                linkLabel1.Text = "Invalid link inserted.";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DropdownOptions option = (DropdownOptions)comboBox1.SelectedValue;
            switch (option)
            {
                case DropdownOptions.Filesystem:
                    if (!String.IsNullOrEmpty(filesystemFileName))
                    {
                        // handle filesystemFileName
                        string textFromFile =  _filer.ReadFromFile(filesystemFileName);
                        textBox3.Text = _translator.Translate(textFromFile, _translator.Detect());
                    }
                    break;
                case DropdownOptions.Web:
                    if (!String.IsNullOrEmpty(webFileLocation))
                    {
                        try
                        {
                            string textFromWeb = _filer.ReadFromWeb(webFileLocation);
                            textBox3.Text = _translator.Translate(textFromWeb, _translator.Detect());
                        }
                        catch (WebReadException)
                        {
                            MessageBox.Show("Error reading the file from the web page.", applicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            // add log4j
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Generic error appeared, please report to developer.", applicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            // log4j
                        }
                    }
                    break;
                case DropdownOptions.Input:
                    if (!(String.IsNullOrWhiteSpace(textBox2.Text) || String.IsNullOrEmpty(textBox2.Text)))
                    {
                        textBox3.Text = _translator.Translate(textBox2.Text, _translator.Detect());
                    }
                    else
                    {
                        MessageBox.Show("The field is empty.", applicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    break;
                default:
                    break;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (e.Link.LinkData != null)
            {
                Process.Start(e.Link.LinkData as string);
            }
            linkLabel1.LinkVisited = true;
        }
    }
}
