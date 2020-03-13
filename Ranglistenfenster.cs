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
    public partial class Ranglistenfenster : Form
    {
        public string namedesspielers { get; set; }

        public Ranglistenfenster(int Position, int Punkte)
        {
            InitializeComponent();
            Platzierung_label.Text = Position.ToString();
            Punkte_label.Text = Punkte.ToString();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(Namebox.Text))
            {
                MessageBox.Show("Gib einen gültigen Namen ein!");
            }
            else
            {
                namedesspielers = Namebox.Text;
                this.Hide();
                
            }
        }

        //public string AskName()
        //{
        //    this.ShowDialog();
        //    return this.namedesspielers;
        //}
    }
}
