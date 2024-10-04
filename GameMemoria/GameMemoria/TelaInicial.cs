using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameMemoria
{
    public partial class TelaInicial : Form
    {
        public TelaInicial()
        {
            InitializeComponent();
        }

        private void TelaInicial_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Increment(progressBar1.Step);
            label1.Text = progressBar1.Value.ToString() + "%";

           
            if (progressBar1.Value == 100)
            {
                timer1.Enabled = false;
                this.Hide();
                Game game = new Game();
                game.ShowDialog();
                this.Close();

            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }
    }
}
