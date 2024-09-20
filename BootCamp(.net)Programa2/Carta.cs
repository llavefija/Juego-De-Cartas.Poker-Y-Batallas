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

        // Constructor de la Carta para baraja española
        public Carta(int numero, ePaloEspanola paloESP)
        {
            this.Numero = numero;
            this.paloEspanol = paloESP;
        }

        // Constructor de la Carta para baraja francesa
        public Carta(int numero, ePaloFrancesa paloFRA)
        {
            this.Numero = numero;
            this.paloFrances = paloFRA;
        }

        // Constructor de Carta vacío, que representa una carta especial o "sin cartas"
        public Carta()
        {
            // Asignar valores especiales que indiquen que la carta está vacía
            this.numero = 0;
            this.paloESP = ePaloEspanola.Copas; // Puede ser cualquier palo
            this.paloFRA = ePaloFrancesa.Picas; // Puede ser cualquier palo
        }

        // Método para verificar si la carta es "sin cartas" (vacía)
        public bool EsCartaVacia()
        {
            return this.numero == 0;
        }


        public int Numero { get => numero; set => numero = value; }
        public ePaloEspanola paloEspanol { get => paloESP; set => paloESP = value; }

        public ePaloFrancesa paloFrances { get => paloFRA; set => paloFRA = value; }

    }
}
