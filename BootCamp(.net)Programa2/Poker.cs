using BootCamp_.net_Programa2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootCamp_.net_Programa2
{
    internal class Poker
    {
        public Poker()
        {

        }

        public void IniciarPoker()
        {
            // Muestra el titulo del juego
            Menu.ImprimirConColor("\nPÓKER TEXAS HOLD 'EM", ConsoleColor.Yellow);

            // Muestra las normas del juego
            Console.WriteLine(@"
·Este juego se juega con 52 cartas de la baraja francesa (1-10, jack, reina y rey),
con los respectivos palos (picas, corazones, diamantes y treboles).

·Es un juego de dos a diez jugadores.

Selecciona si quieres saber mas informacion sobre el juego o quieres empezar a jugar"
            );

            string op = "";

            while (op != "0")
            {
                op = Console.ReadLine();

                switch (op)
                {
                    case "0":
                        break;
                    case "1":
                        MostrarReglasJuego();
                        break;
                }


            }
        }

        public void MostrarReglasJuego()
        {
            Console.WriteLine(@"
-Objetivo del Juego:

·Ganar fichas obteniendo la mejor mano de póker de cinco cartas, ya sea con las cartas propias y las comunitarias o forzando a los demás jugadores a retirarse.
Número de Jugadores:

-Distribución de Cartas:

·Cada jugador recibe dos cartas privadas (ocultas).

·Se colocan cinco cartas comunitarias en la mesa en tres fases: el ""Flop"" (3 cartas), el ""Turn"" (1 carta) y el ""River"" (1 carta).

-Rondas de Apuestas:

·Pre-Flop: Después de recibir las cartas privadas.
·Flop: Después de mostrar las primeras tres cartas comunitarias.
·Turn: Después de mostrar la cuarta carta comunitaria.
·River: Después de mostrar la quinta y última carta comunitaria.

·En cada ronda, los jugadores pueden apostar, igualar (call), subir (raise) o retirarse (fold).

-Manos de Póker (de mayor a menor valor):

·Escalera Real (Royal Flush): 5 cartas secuenciales del mismo palo, del 10 al As.
·Escalera de Color (Straight Flush): 5 cartas secuenciales del mismo palo.
·Póker (Four of a Kind): 4 cartas del mismo valor.
·Full House: 3 cartas de un valor y 2 cartas de otro valor.
·Color (Flush): 5 cartas del mismo palo, no secuenciales.
·Escalera (Straight): 5 cartas secuenciales de distintos palos.
·Trío (Three of a Kind): 3 cartas del mismo valor.
·Doble Pareja (Two Pair): 2 pares de cartas del mismo valor.
·Pareja (One Pair): 2 cartas del mismo valor.
·Carta Alta (High Card): La carta de mayor valor si no se forma ninguna de las manos anteriores.

-Acciones en la Mesa:

·Check: No apostar, pero mantener el turno.
·Bet: Apostar fichas.
·Call: Igualar la apuesta actual.
·Raise: Aumentar la apuesta actual.
·Fold: Retirarse de la mano y perder las fichas apostadas.

-Ganador:

·Al final de la última ronda de apuestas, el jugador con la mejor mano de cinco cartas gana el bote. Si hay un empate, el bote se divide.");
        }

        public int IntroducirJugadores()
        {
            int jugadores;


            while (true)
            {
                try
                {
                    int.TryParse(Console.ReadLine(), out jugadores);

                    if (jugadores < 2 || jugadores > 5)
                    {
                        Menu.ImprimirConColor("\nNumero de jugador no valido, debe de ser entre 2 y 5 jugadores.", ConsoleColor.Red);
                    }
                    else { break; }
                }
                catch (Exception ex)
                {
                    Menu.ImprimirConColor($"\nHa sucedido un error al intentar introducir el numero de jugadores: {ex.Message}", ConsoleColor.Red);

                }
            }
            return jugadores;
        }

    }
}
