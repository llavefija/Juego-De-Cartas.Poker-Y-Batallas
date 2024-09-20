using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BootCamp_.net_Programa2;


namespace BootCamp_.net_Programa2
{
    internal class Menu
    {
        // Metodo principal del programa
        static void Main(string[] args)
        {

            string op = ""; // Opcion que eligira el usuario.

            bool substitulo = false;

            ImprimirConColor("BIENVIENIDO AL JUEGO DE CARTAS LOCAL.", ConsoleColor.Yellow); // Presentacion del menu con varios juegos a elegir

            while (op != "0")
            {

                // Multiples opciones que se le muestra al usuario.

                if (substitulo)
                {
                    ImprimirConColor("\nA QUE JUEGO LE APETECE JUGAR AHORA?", ConsoleColor.Yellow); // Presentacion del menu con varios una vez elegido uno.

                }

                Console.WriteLine(@"
Introduce el juego al que quieras jugar:

1.Ataque directo, batalla de cartas.
2.Ataque con mazo, batalla de cartas.
3.Poker Texas Hold 'Em
0.Salir del programa.
");

                op = Console.ReadLine();

                Batalla batalla = new Batalla();
                Poker poker = new Poker();


                switch (op)
                {
                    case "0":
                        break; // Opcion para salir del programa.
                    case "1":
                        batalla.IniciarBatalla(true); // Opcion para iniciar una batalla donde se juega 1 carta al momento
                        substitulo = true;
                        break;
                    case "2":
                        batalla.IniciarBatalla(false); // Opcion para iniciar una batalla donde cada jugador tiene un mazo
                        substitulo = true;
                        break;
                    case "3":
                        poker.IniciarPoker(); // Opcion para iniciar el juego de poker texas hold 'em
                        substitulo = true;
                        break;
                    default:
                        ImprimirConColor($"{op} no es una opcion valida. Introduce de forma numerica una opcion del menu.", ConsoleColor.Red); // Muestra un texto en caso de no ser valido
                        break;
                }
            }


        }

        // Metodo para imprimir texto con el color deseado
        public static void ImprimirConColor(string texto, ConsoleColor color)
        {
            // Guardar el color original
            ConsoleColor colorOriginal = Console.ForegroundColor;

            // Cambiar el color del texto
            Console.ForegroundColor = color;

            // Imprimir el texto
            Console.WriteLine(texto);

            // Restaurar el color original
            Console.ForegroundColor = colorOriginal;
        }
    }
}
