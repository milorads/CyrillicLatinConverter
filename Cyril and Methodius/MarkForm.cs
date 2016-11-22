using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cyril_and_Methodius
{
    public partial class MarkForm : Form
    {
        private bool stateExisting = true;
        private bool stateCustom = false;
        public MarkForm()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            Dictionary<string, string> replacementOptions = new Dictionary<string, string>();
            foreach (var item in ReplacementSigns)
            {
                replacementOptions.Add(item,"Mark: \" "+item+" \"");
            }
            comboBox1.DataSource = new BindingSource(replacementOptions, null);
            comboBox1.DisplayMember = "Value";
            comboBox1.ValueMember = "Key";
        }
        private string[] ReplacementSigns =
        {
            "!","?","|","/","\"","\\","-","_",":","::"
        };

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (stateExisting && !stateCustom)
            {
                radioButton1.Checked = false;
                comboBox1.Enabled = false;
                radioButton2.Checked = true;
                textBox1.Enabled = true;
            }
            else
            {
                radioButton1.Checked = true;
                comboBox1.Enabled = true;
                radioButton2.Checked = false;
                textBox1.Enabled = false;
            }
            stateExisting = !stateExisting;
            stateCustom = !stateCustom;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (!stateExisting && stateCustom)
            {
                radioButton1.Checked = false;
                comboBox1.Enabled = false;
                radioButton2.Checked = true;
                textBox1.Enabled = true;
            }
            else
            {
                radioButton1.Checked = true;
                comboBox1.Enabled = true;
                radioButton2.Checked = false;
                textBox1.Enabled = false;
            }
            stateExisting = !stateExisting;
            stateCustom = !stateCustom;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string replacementSign;
            if (stateExisting && !stateCustom)
            {
                replacementSign = comboBox1.SelectedValue.ToString();
            }
            else
            {
                replacementSign = textBox1.Text;
            }
            Form1.customReplacementMark = replacementSign;
            this.Hide();
        }
    }
}
