using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Übung_7_Snake
{
    public partial class Übersicht : Form
    {
        public Übersicht(List<Form1.Highscore_obj> Top5)
        {
            InitializeComponent();
            string Platz1Name = Top5[0].Name;
            string Platz2Name = Top5[1].Name;
            string Platz3Name = Top5[2].Name;
            string Platz4Name = Top5[3].Name;
            string Platz5Name = Top5[4].Name;

            var Platz1Score = Convert.ToInt32(Top5[0].Score);
            var Platz2Score = Convert.ToInt32(Top5[1].Score);
            var Platz3Score = Convert.ToInt32(Top5[2].Score);
            var Platz4Score = Convert.ToInt32(Top5[3].Score);
            var Platz5Score = Convert.ToInt32(Top5[4].Score);

            label6.Text = (Platz1Name + " mit " + Platz1Score + " Punkten!");
            label7.Text = (Platz2Name + " mit " + Platz2Score + " Punkten!");
            label8.Text = (Platz3Name + " mit " + Platz3Score + " Punkten!");
            label9.Text = (Platz4Name + " mit " + Platz4Score + " Punkten!");
            label10.Text = (Platz5Name + " mit " + Platz5Score + " Punkten!");
        }// Gibt den Highscore im Fenster aus

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }// Naja schließt halt das Fenster
    }
}
