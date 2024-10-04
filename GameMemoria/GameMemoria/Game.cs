using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace GameMemoria
{
    public partial class Game : Form
    {      
        Jogo jogo = new Jogo();

        int tentativas, clicks, cartasEncontradas, tagIndex;

        Image[] img = new Image[6];

        List<String> lista = new List<string>();

        int[] tags = new int[2];

        int nivel = 1;
        int segundo = 0;
        int minuto = 0;

        public Game()
        {
            InitializeComponent();
            
                    
        }
      
        private void Inicio()
        {
            foreach (PictureBox item in Controls.OfType<PictureBox>())
            {
                
                tagIndex = int.Parse(String.Format("{0}", item.Tag));
                img[tagIndex] = item.Image;
                item.Image = GameMemoria.Properties.Resources.verso;
                item.Refresh();
                item.Enabled = true;
                minuto = jogo.escolhaMinuto(nivel);
                segundo = jogo.escolhaSegundo(nivel);
                label3.Text = "0" + minuto + ":0" + segundo;

            }
            Posicao();
        }
     

        private void Posicao()
        {
            foreach (PictureBox item in Controls.OfType<PictureBox>())
            {
               
                Random random = new Random();
                int[] xP = { 142, 284, 417, 556 };
                int[] yP = { 87, 206, 323 };

            Repete:
                var x = xP[random.Next(0, xP.Length)];
                var y = yP[random.Next(0, yP.Length)];

                string verificacao = x.ToString() + y.ToString();

                if (lista.Contains(verificacao))
                {
                    goto Repete;
                }
                else
                {
                    item.Location = new Point(x, y);
                    lista.Add(verificacao);
                }

            }
            
        }

        private void ImagensClick_Click(object sender, EventArgs e)
        {
            bool parEncontrado = false;
            timer1.Enabled = true;
            PictureBox pic = (PictureBox)sender;
            clicks++;
            tagIndex = int.Parse(String.Format("{0}", pic.Tag));
            pic.Image = img[tagIndex];
            pic.Refresh();
            if (clicks == 1)
            {
                tags[0] = int.Parse(String.Format("{0}", pic.Tag));
            }
            else if (clicks == 2)
            {
                tentativas++;
                label5.Text = "Tentativas " + tentativas.ToString();
                tags[1] = int.Parse(String.Format("{0}", pic.Tag));
                parEncontrado = ConferirPares();
                Desvirar(parEncontrado);
            }
        }
        
        private void label5_Click(object sender, EventArgs e)
        {
        }

        private bool ConferirPares()
        {
            clicks = 0;
            if (tags[0] == tags[1])
            {
                return true;
            }
            else
                return false;
        }

        private void Desvirar(bool check)
        {
            Thread.Sleep(500);
            foreach (PictureBox item in Controls.OfType<PictureBox>())
            {
                if (int.Parse(String.Format("{0}", item.Tag)) == tags[0]
                    || int.Parse(String.Format("{0}", item.Tag)) == tags[1])
                {
                    if (check == true)
                    {
                        item.Enabled = false;
                        cartasEncontradas++;
                    }
                    else
                    {
                        item.Image = GameMemoria.Properties.Resources.verso;
                        item.Refresh();
                    }

                }

            }
            FinalJogo();
        }

        private void niveisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void facilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nivel = 1;
            minuto = jogo.escolhaMinuto(nivel);
            segundo = jogo.escolhaSegundo(nivel);
            label3.Text = "0" + minuto + ":0" + segundo;
        }

        private void medioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nivel = 2;
            minuto = jogo.escolhaMinuto(nivel);
            segundo = jogo.escolhaSegundo(nivel);
            label3.Text = "0" + minuto + ":" + segundo;
        }

        private void difícilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nivel = 3;
            minuto = jogo.escolhaMinuto(nivel);
            segundo = jogo.escolhaSegundo(nivel);
            label3.Text = "0" + minuto + ":" + segundo;
        }


        private void ajudaToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("Ache os pares.\n\rEscolha um nível:\n\rSe o nível escolhido for o facil = você terá 01:00 para achar todos os pares;\n\rSe for medio = você terá 00:50 para achar todos os pares;\n\rSe for o dificil = você terá 00:30 para achar todos os pares");
        }

        private void sairToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Deseja Realmente Sair?", "Pergunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                this.Close();
            }
        }



        private void FinalJogo()
        {
            if (cartasEncontradas == (img.Length * 2))
            {
                timer1.Enabled = false;
                MessageBox.Show("Parabens, você terminou o jogo com " + tentativas.ToString() + " tentativas");
                DialogResult mensagem = MessageBox.Show("Deseja iniciar um novo jogo?", "Caixa de pergunta", MessageBoxButtons.YesNo);
                if (mensagem == DialogResult.Yes)
                {
                   
                    tentativas = 0;
                    label5.Text = "Tentativas " + tentativas.ToString();
                    clicks = 0;                   
                    cartasEncontradas = 0;
                    lista.Clear();
                    Inicio();
                    minuto = jogo.escolhaMinuto(nivel);
                    segundo = jogo.escolhaSegundo(nivel);
                    label3.Text = "0" + minuto + ":" + segundo;
                    this.Refresh();

                }
                else if (mensagem == DialogResult.No)
                {
                    MessageBox.Show("Obrigada por jogar!");
                    Application.Exit();
                }
            }
        }
        private void Game_Load(object sender, EventArgs e)
        {
            label4.Text = "Escolha um nível";
          
            
                Inicio();
            
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           
            segundo--;
            if(minuto > 0)
            {
                if(segundo < 0)
                {
                    segundo = 59;
                    minuto--;
                }
            }
            label3.Text = "0" + minuto + ":" + segundo;
            if (minuto == 0 && segundo == 0)
            {
                timer1.Enabled = false;
                MessageBox.Show("Seu tempo acabou, tente novamente.");
                tentativas = 0;
                label5.Text = "Tentativas " + tentativas.ToString();
                clicks = 0;
                cartasEncontradas = 0;
                lista.Clear();
                Inicio();
                minuto = jogo.escolhaMinuto(nivel);
                segundo = jogo.escolhaSegundo(nivel);
                label3.Text = "0" + minuto + ":" + segundo;
                this.Refresh();

            }

        }


    }
}
