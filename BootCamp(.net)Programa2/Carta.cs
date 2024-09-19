using System;
using BootCamp_.net_Programa2;


namespace BootCamp_.net_Programa2
{
    internal class Carta
    {
        public enum ePaloEspanola
        {
            Copas,
            Bastos,
            Espadas,
            Oros
        }

        public enum ePaloFrancesa
        {
            Picas,
            Diamantes,
            Corazones,
            Treboles
        }

        private int numero;
        private ePaloEspanola paloESP;
        private ePaloFrancesa paloFRA;


        public Carta()
        {

        }

        public Carta(int numero, ePaloEspanola paloESP)
        {
            this.numero = numero;
            this.paloESP = paloESP; 
        }

        public Carta(int numero, ePaloFrancesa paloFRA)
        {
            this.numero = numero;
            this.paloFRA = paloFRA;
        }

        public int Numero { get => numero; set => numero = value; }
        public ePaloEspanola paloEspanol { get => paloESP; set => paloESP = value; }

        public ePaloFrancesa paloFrances { get => paloFRA; set => paloFRA = value; }

    }
}
