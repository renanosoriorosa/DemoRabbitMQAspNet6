using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estoque.API.Services
{
    public class EstoqueService
    {
        public bool PossuiEstoque()
        {
            Random randNum = new Random();
            int numero = randNum.Next(1, 200);

            if(NumeroPar(numero))
                return true;

            return false;
        }

        private bool NumeroPar(int numero)
        {
            if (numero % 2 == 0) return true;
            
            return false;
        }
    }
}
