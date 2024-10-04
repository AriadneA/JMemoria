using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace GameMemoria
{
    class Jogo
    {

        public int escolhaMinuto(int nivel)
        {
            int minuto = 0;

            if ( nivel == 1)
            {
                minuto = 1;
            }
            else if (nivel == 2)
            {
                minuto = 0;
            }
            else if (nivel == 3)
            {
                minuto = 0;
            }
            return minuto;
        }
        public int escolhaSegundo(int nivel)
        {
            int segundo = 0;

            if (nivel == 1)
            {
                segundo = 00;
            }
            else if (nivel == 2)
            {
                segundo = 50;
            }
            else if (nivel == 3)
            {
                segundo = 30;
            }
            return segundo;
        }


    }
    
}
