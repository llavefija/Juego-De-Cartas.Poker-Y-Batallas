using System;
using System.Collections.Generic;
using BootCamp_.net_Programa2;
using static BootCamp_.net_Programa2.Carta;

namespace BootCamp_.net_Programa2
{
    internal class Baraja
    {
        public List<Carta> cartas; // Instancia de cartas
        Random random; // Instancia de Random

        public Baraja()
        {
            cartas = new List<Carta>(); // Inicializar Carta
            random = new Random(); // Inicializar Random 
        }

        // Método para crear una baraja española. 48 cartas. 12 por palo. Espada, oro, copa y basto.
        public void CrearBarajaEspañola()
        {
            foreach (var item in Enum.GetValues(typeof(ePaloEspanola)))
            {

                for (int num = 1; num <= 12; num++)
                {
                    ePaloEspanola palo = (ePaloEspanola)item; // Convertir el entero a tipo Palo (enum)
                    Carta c = new Carta(num, palo);
                    cartas.Add(c); // Añadir la carta a la baraja
                }
            }
        }

        // Método para crear una baraja francesa. 52 cartas. 13 por palo. Pica, corazón, diamante y trébol.
        public void CrearBarajaFrancesa()
        {
            foreach (var item in Enum.GetValues(typeof(ePaloFrancesa))) // Asegúrate de que ePaloFrances esté definido
            {


                for (int num = 1; num <= 13; num++)
                {
                    ePaloFrancesa palo = (ePaloFrancesa)item; // Convertir el entero a tipo Palo (enum)
                    Carta c = new Carta(num, palo);
                    cartas.Add(c); // Añadir la carta a la baraja
                }

            }
        }

        // Metodo para robar una carta
        public Carta RobarCarta()
        {
            if (cartas.Count == 0)
                throw new InvalidOperationException("No hay cartas en la baraja.");

            int index = random.Next(cartas.Count); // Seleccionar un índice aleatorio
            Carta carta = cartas[index]; // Obtener la carta en ese índice
            cartas.RemoveAt(index); // Eliminar la carta de la baraja para no seleccionar la misma
            return carta;
        }

        public void MezclarBaraja()
        {

            Random random = new Random();
            int n = cartas.Count;

            for (int i = n - 1; i > 0; i--)
            {
                // Seleccionar un índice aleatorio entre 0 y i
                int j = random.Next(0, i + 1);

                // Intercambiar cartas en índices i y j
                Carta temp = cartas[i];
                cartas[i] = cartas[j];
                cartas[j] = temp;
            }


        }

    }
}
