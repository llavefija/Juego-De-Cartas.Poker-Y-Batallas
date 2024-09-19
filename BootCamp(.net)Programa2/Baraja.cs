using System;
using System.Collections.Generic;
using BootCamp_.net_Programa2;
using static BootCamp_.net_Programa2.Carta;

namespace BootCamp_.net_Programa2
{
    internal class Baraja
    {
        List<Carta> cartas;

        public Baraja()
        {
            // Inicializar la lista cartas
            cartas = new List<Carta>();
        }

        internal List<Carta> Cartas { get => cartas; set => cartas = value; }

        // Metodo para crear una baraja española. 48 cartas. 12 por palo. Espada, oro, copa y basto.
        public void CrearBarajaEspañola()
        {
            foreach (var item in Enum.GetValues(typeof(ePaloEspanola)))
            {
                for (int num = 1; num <= 12; num++)
                {
                    ePaloEspanola palo = (ePaloEspanola)item; // Convertir el entero a tipo Palo (enum)
                    Carta c = new Carta(num, palo);
                    Cartas.Add(c); // Añadir la carta a la baraja
                }
            }
        }
        // Metodo para crear una baraja francesa. 52 cartas. 13 por palo. Pica, corazon, diamante y trebol.
        public void CrearBarajaFrancesa()
        {
            foreach (var item in Enum.GetValues(typeof(ePaloEspanola)))
            {
                for (int num = 1; num <= 13; num++)
                {
                    ePaloEspanola palo = (ePaloEspanola)item; // Convertir el entero a tipo Palo (enum)
                    Carta c = new Carta(num, palo);
                    Cartas.Add(c); // Añadir la carta a la baraja
                }
            }
        }

    }
}
