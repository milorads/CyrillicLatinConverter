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

namespace Cyril_and_Methodius
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            // test
            string a = "Тест ћирилична метода, са још по неким карактером. ХЕ ЏА џ xzwq QQ // џ њ љ Џ Њ Љ";
            string b = "Теst latinična metoda, sa još po nekim karakterom. Xe dža Dž DŽ xzwq QQ // dŽ dž Dž DŽ Nj nj NJ nJ lj lJ LJ Lj";
            Translator t = new Translator();
            string outS = Translator.Translate(a, true);
            string outS2 = Translator.Translate(b, false);
            Console.Read();
            // test
            InitializeComponent();
        }
    }
}
