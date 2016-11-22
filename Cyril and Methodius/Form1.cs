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
        private string customReplacementMark = "";
        private Translator _translator = new Translator();
        private TextHandler _filer = new TextHandler();
        private string filesystemFileName;
        private string webFileLocation;
        private static Dictionary<string, bool> QWYXstate = new Dictionary<string, bool>()
        {
            {"CheckBoxEnabled", true},
            {"ComboBoxEnabled", false}
        };
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
            // out options
            Dictionary<DropdownOptions, string> outOptions = new Dictionary<DropdownOptions, string>();
            var outValues = Enum.GetValues(typeof(DropdownOptions));
            foreach (var item in outValues)
            {
                outOptions.Add((DropdownOptions)item, item.ToString());
            }
            comboBox1.DataSource = new BindingSource(outOptions, null);
            comboBox1.DisplayMember = "Value";
            comboBox1.ValueMember = "Key";
            // replacement options
            Dictionary<Translator.LatinEnglishCharacter, string> replacementOptions = new Dictionary<Translator.LatinEnglishCharacter, string>();
            var replacementValues = Enum.GetValues(typeof(Translator.LatinEnglishCharacter));
            foreach (var item in replacementValues)
            {
                replacementOptions.Add((Translator.LatinEnglishCharacter)item, item.ToString());
            }
            comboBox2.DataSource = new BindingSource(replacementOptions, null);
            comboBox2.DisplayMember = "Value";
            comboBox2.ValueMember = "Key";
            // rest
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
            DialogResult result = fDialog.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                filesystemFileName = fDialog.FileName;
                ToolTip ToolTip1 = new ToolTip();
                ToolTip1.SetToolTip(label4, filesystemFileName);
                label4.Text = Path.GetFileName(filesystemFileName);
            }
            comboBox1.SelectedValue = DropdownOptions.Filesystem;
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
                    comboBox1.SelectedValue = DropdownOptions.Web;
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
            Translator.LatinEnglishCharacter additionalOption = new Translator.LatinEnglishCharacter();
             if (QWYXstate["CheckBoxEnabled"] == false && QWYXstate["ComboBoxEnabled"] == true)
            {
                additionalOption = (Translator.LatinEnglishCharacter)comboBox2.SelectedValue;
            }
            switch (option)
            {
                case DropdownOptions.Filesystem:
                    if (!String.IsNullOrEmpty(filesystemFileName))
                    {
                        string textFromFile =  _filer.ReadFromFile(filesystemFileName);
                        TranslationHandler(additionalOption, textFromFile);
                    }
                    break;
                case DropdownOptions.Web:
                    if (!String.IsNullOrEmpty(webFileLocation))
                    {
                        webTranslationHandler(additionalOption);
                    }
                    else
                    {
                        button2.PerformClick();
                        webTranslationHandler(additionalOption);
                    }
                    break;
                case DropdownOptions.Input:
                    if (!(String.IsNullOrWhiteSpace(textBox2.Text) || String.IsNullOrEmpty(textBox2.Text)))
                    {
                        TranslationHandler(additionalOption, textBox2.Text);
                    }
                    else
                    {
                        MessageBox.Show("The field is empty.", applicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    break;
                default:
                    break;
            }
            textBox2.Text = String.Empty;
        }

        private void webTranslationHandler(Translator.LatinEnglishCharacter additionalOption)
        {
            try
            {
                string textFromWeb = _filer.ReadFromWeb(webFileLocation);
                TranslationHandler(additionalOption, textFromWeb);
            }
            catch (WebReadException) //e -> log4j
            {
                MessageBox.Show("Error reading the file from the web page.", applicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                // add log4j
            }
            catch (Exception) //e -> log4j
            {
                MessageBox.Show("Generic error appeared, please report to developer.", applicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                // log4j
            }
        }

        private void TranslationHandler(Translator.LatinEnglishCharacter additionalOption, string givenText)
        {
            if (additionalOption == Translator.LatinEnglishCharacter.Include)
            {
                textBox3.Text = _translator.Translate(givenText, _translator.Detect(givenText));
            }
            else if (additionalOption == Translator.LatinEnglishCharacter.Delete)
            {
                textBox3.Text = _translator.Translate(givenText, _translator.Detect(givenText), Translator.LatinEnglishCharacter.Delete);
            }
            else if (additionalOption == Translator.LatinEnglishCharacter.Mark)
            {
                if (customReplacementMark == "")
                {
                    textBox3.Text = _translator.Translate(givenText, _translator.Detect(givenText), Translator.LatinEnglishCharacter.Mark);
                }
                else
                {
                    textBox3.Text = _translator.Translate(givenText, _translator.Detect(givenText), Translator.LatinEnglishCharacter.Mark, customReplacementMark);
                }
            }
            additionalOption = Translator.LatinEnglishCharacter.Include;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (e.Link.LinkData != null)
            {
                Process.Start(e.Link.LinkData as string);
            }
            linkLabel1.LinkVisited = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            stateSwitch();
            comboBox2.Enabled = QWYXstate["ComboBoxEnabled"];
        }
        private static void stateSwitch()
        {
            bool checkBoxState = false;
            bool comboBoxState = false;
            QWYXstate.TryGetValue("CheckBoxEnabled", out checkBoxState);
            QWYXstate.TryGetValue("ComboBoxEnabled", out comboBoxState);
            if (checkBoxState == true && comboBoxState == false)
            {
                QWYXstate["CheckBoxEnabled"] = !checkBoxState;
                QWYXstate["ComboBoxEnabled"] = !comboBoxState;
            }
            else if (checkBoxState == false && comboBoxState == true)
            {
                QWYXstate["CheckBoxEnabled"] = !checkBoxState;
                QWYXstate["ComboBoxEnabled"] = !comboBoxState;
            }
        }

        private void InputTextBoxChanged(object sender, EventArgs e)
        {
            textBox2.TextChanged -= InputTextBoxChanged;
            comboBox1.SelectedValue = DropdownOptions.Input;
        }

        private void InputTextBoxLeave(object sender, EventArgs e)
        {
            textBox2.TextChanged += InputTextBoxChanged;
        }

        private void ReplacementChange(object sender, EventArgs e)
        {
            if (comboBox2.SelectedValue.ToString() == Translator.LatinEnglishCharacter.Mark.ToString())
            {
                // open new form
            }
        }
    }
}
