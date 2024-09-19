using System;
using BootCamp_.net_Programa2;


namespace BootCamp_.net_Programa2
{
    internal class Carta
    {
        // Palo de la baraja Española
        public enum ePaloEspanola
        {
            Copas,
            Bastos,
            Espadas,
            Oros
        }

        // Palo de la baraja Francesa
        public enum ePaloFrancesa
        {
            Picas,
            Diamantes,
            Corazones,
            Treboles
        }

        private int numero; // Se instancia el numero
        private ePaloEspanola paloESP; // Se intancia el enum palo español
        private ePaloFrancesa paloFRA; // Se instancia el enum palo frances

        // Constructor de Carta vacio
        public Carta()
        {

        }

        // Constructor de la Carta para baraja española
        public Carta(int numero, ePaloEspanola paloESP)
        {
            this.numero = numero;
            this.paloESP = paloESP; 
        }

        // Constructor de la Carta para baraja francesa
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
