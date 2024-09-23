using BootCamp_.net_Programa2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using static BootCamp_.net_Programa2.Jugadores;

namespace BootCamp_.net_Programa2
{
    internal class Poker
    {

        Jugadores jugador;
        Baraja cartasBaraja;

        List<Jugadores> listaJugadores;
        int apuesta = 0;


        public Poker()
        {
            cartasBaraja = new Baraja();
            listaJugadores = new List<Jugadores>();
            cartasBaraja = new Baraja();  // Crear la baraja una vez en el constructor
            cartasBaraja.CrearBarajaFrancesa();  // Crear la baraja francesa
            cartasBaraja.MezclarBaraja();  // Mezclarla una vez
        }

        public void IniciarPoker()
        {

            bool substitulo = false;

            // Muestra el titulo del juego
            Menu.ImprimirConColor("\nPÓKER TEXAS HOLD 'EM", ConsoleColor.Yellow);

            // Muestra las normas del juego
            Console.WriteLine(@"
·Este juego se juega con 52 cartas de la baraja francesa (1-10, jack, reina y rey),
con los respectivos palos (picas, corazones, diamantes y treboles).

·Es un juego de dos a diez jugadores.

Selecciona si quieres saber mas informacion sobre el juego o quieres empezar a jugar

0.Volver al menú de juegos
1.Mostrar reglas del juego
2.Jugar
");

            string op = "";

            while (op != "0")
            {

                if (substitulo)
                {
                    Menu.ImprimirConColor("\nOTRA PARTIDA? OTRO JUEGO?", ConsoleColor.Yellow);

                    Console.WriteLine(@"

Selecciona si quieres saber mas informacion sobre el juego o quieres empezar a jugar

0.Volver al menú de juegos
1.Mostrar reglas del juego
2.Jugar");
                }
                op = Console.ReadLine();

                switch (op)
                {
                    case "0":
                        break;
                    case "1":
                        MostrarReglasJuego();
                        break;
                    case "2":
                        int dinero = IntroducirDinero();
                        int jugadores = IntroducirJugadores(dinero);
                        EmpezarJuego();
                        Rondas(true);
                        substitulo = true;
                        break;
                }


            }
        }

        public void MostrarReglasJuego()
        {
            Console.WriteLine(@"
-OBJETIVO DEL JUEGO:

·Ganar fichas obteniendo la mejor mano de póker de cinco cartas, ya sea con las cartas propias y las comunitarias o forzando a los demás jugadores a retirarse.
Número de Jugadores:

-DISTRIBUCIÓN DE CARTAS:

·Cada jugador recibe dos cartas privadas (ocultas).

·Se colocan cinco cartas comunitarias en la mesa en tres fases: el ""Flop"" (3 cartas), el ""Turn"" (1 carta) y el ""River"" (1 carta).

-RONDAS DE APUESTAS:

·Pre-Flop: Después de recibir las cartas privadas.
·Flop: Después de mostrar las primeras tres cartas comunitarias.
·Turn: Después de mostrar la cuarta carta comunitaria.
·River: Después de mostrar la quinta y última carta comunitaria.

·En cada ronda, los jugadores pueden apostar, igualar (call), subir (raise) o retirarse (fold).

-MANOS DE PÓKER (DE MAYOR A MENOR VALOR):

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

-ACCIONES EN LA MESA:

·Check: No apostar, pero mantener el turno.
·Bet: Apostar fichas.
·Call: Igualar la apuesta actual.
·Raise: Aumentar la apuesta actual.
·Fold: Retirarse de la mano y perder las fichas apostadas.

-GANADOR:

·Al final de la última ronda de apuestas, el jugador con la mejor mano de cinco cartas gana el bote. Si hay un empate, el bote se divide.");
        }
        public static string CapitalizarPrimeraLetra(string texto)
        {
            if (string.IsNullOrEmpty(texto))
                return texto; // Retorna el texto original si es nulo o vacío

            return char.ToUpper(texto[0]) + texto.Substring(1);
        }
        public int IntroducirDinero()
        {
            int dinero;

            Console.WriteLine("\nPrimero introduce la cantidad de fichas que tendra cada jugador entre 1.500 y 5.000.\n");

            try
            {
                while (!int.TryParse(Console.ReadLine(), out dinero) || dinero < 1500 || dinero > 5000)
                {
                    Menu.ImprimirConColor("\nNo se puede empezar la partida con esa cantidad. Introduce un numero de fichas entre 1.500 y 5.000\n", ConsoleColor.Red);
                }
                return dinero;
            }
            catch (Exception ex)
            {
                Menu.ImprimirConColor($"\nHa sucedido un error mientras se introducia las fichas iniciales: {ex.Message}\n", ConsoleColor.Red);
                return 0;
            }
        }
        public int IntroducirJugadores(int dinero)
        {

            int jugadores = 0;
            Jugadores.eGenero genero;
            int op;


            try
            {
                try
                {
                    Console.WriteLine("\nCuantos jugadores sois? Debe de ser como minimo dos y maximo 10.\n");

                    while (!int.TryParse(Console.ReadLine(), out jugadores) || jugadores < 2 || jugadores > 10)
                    {
                        Menu.ImprimirConColor("\nNumero de jugador no valido, debe de ser entre 2 y 10 jugadores.\n", ConsoleColor.Red);
                    }
                }
                catch (Exception ex)
                {
                    Menu.ImprimirConColor($"\nHa sudedido un error introducioendo el numero de jugadores: {ex}", ConsoleColor.Red);

                }

                for (int contadorJugadores = 1; contadorJugadores <= jugadores; contadorJugadores++)
                {
                    Console.WriteLine($@"
Introduce el genero del jugador nº {contadorJugadores}:

1.Femenino
2.Masculino
3.No especificar
");
                    try
                    {
                        while (!int.TryParse(Console.ReadLine(), out op) || op < 1 || op > 3)
                        {
                            Menu.ImprimirConColor("\nSelección inválida. Por favor, elige 1, 2 o 3.\n", ConsoleColor.Red);
                        }

                        genero = (Jugadores.eGenero)op;
                    }
                    catch (Exception ex)
                    {
                        Menu.ImprimirConColor($"\nHa sudedido un error introducioendo el genero del jugador: {ex.Message}\n", ConsoleColor.Red);
                        break;
                    }
                    try
                    {
                        Console.WriteLine("\nIntroduce el nombre del jugador.\n");

                        string nombreJugador = Console.ReadLine();

                        // Crear una nueva instancia del jugador
                        jugador = new Jugadores
                        {
                            Nombre = CapitalizarPrimeraLetra(nombreJugador),
                            Genero = genero,
                            Dinero = dinero,
                            Apuesta = 0

                        };

                        listaJugadores.Add(jugador);

                        Menu.ImprimirConColor($"\nHola {(jugador.Genero == Jugadores.eGenero.Masculino ? "Sr. " : jugador.Genero == Jugadores.eGenero.Femenino ? "Sra. " : "")}{jugador.Nombre}! Se te ha añadido correctamente a la partida con {dinero} fichas.\n", ConsoleColor.Green);

                    }
                    catch (Exception ex)
                    {
                        Menu.ImprimirConColor($"\nHa sucedido un error al introducir los jugadores: {ex.Message}\n", ConsoleColor.Red);
                    }
                }

                // Crear una nueva instancia del jugador
                jugador = new Jugadores
                {
                    Nombre = CapitalizarPrimeraLetra("RobertoMesa#0001BotCrupier"),
                    Genero = eGenero.Masculino,
                    Dinero = 0,
                    Apuesta = 0,
                    Jugando = false
                };

                listaJugadores.Add(jugador);


            }
            catch (Exception ex)
            {
                Menu.ImprimirConColor($"\nHa sucedido un error al intentar introducir el numero de jugadores: {ex.Message}\n", ConsoleColor.Red);
                return 0;
            }

            return jugadores;
        }
        public void EmpezarJuego()
        {

            string op;

            Console.WriteLine($@"
Bienvenidos a Poker Texas Hold 'em.

Soy Roberto, el crupier de este juego. Veo que esta vez sois {listaJugadores.Count} y que vais a empezar con {jugador.Dinero} fichas.

Perfeto! Estoy preparando las cartas y la mesa para empezar la partida. Por si acaso, antes de comenzar... conoceis las normas? (Y/N)

");

            do
            {
                op = Console.ReadLine().ToUpper();

                if (op != "Y" && op != "N" && op != "YES" && op != "NO" && op != "SI")
                {
                    Console.WriteLine("No entiendo lo que intentas decir. ¿Conocéis las normas? (Y/N)\n");
                }

            } while (op != "Y" && op != "N" && op != "YES" && op != "NO" && op != "SI");

            if (op == "SI" || op == "Y" || op == "YES")
            {
                Console.WriteLine("\nGenial, entonces ya podemos comenzar. Justo habia acabado de preparar todo.");

            }
            else
            {
                Console.WriteLine("\nNo os preocupeis, yo os paso el manual mientras acabo de preparar todo. Sin prisa. Avisadme cuando esteis listos.\n");
                MostrarReglasJuego();
                Console.WriteLine("\n*Pulsa cualquier tecla para avisar a Roberto de que ya estais listos.");
                Console.ReadLine();
                Console.WriteLine("\nGenial, entonces ya podemos comenzar. Justo habia acabado de preparar todo.");
            }

            Console.WriteLine($"\nEmpezare a barajar las cartas y repartire 5 a cada uno.\n");

            RepartirCartasJugador(2);

        }
        public void RepartirCartasJugador(int NumeroCartas)
        {

            foreach (var jugador in listaJugadores.Where(j => j.Jugando))
            {
                jugador.Mazo = new List<Carta>(); // Inicializa el mazo aquí

                // Verifica que haya suficientes cartas en la baraja
                if (cartasBaraja.cartas.Count < NumeroCartas)
                {
                    Menu.ImprimirConColor("No hay suficientes cartas en la baraja para repartir.", ConsoleColor.Red);
                    return; // Sale del método si no hay suficientes cartas
                }

                // Reparte 5 cartas al jugador
                for (int i = 0; i < NumeroCartas; i++)
                {
                    jugador.Mazo.Add(cartasBaraja.cartas[0]); // Añadir la primera carta
                    cartasBaraja.cartas.RemoveAt(0); // Eliminar la carta de la baraja
                }

                Menu.ImprimirConColor($"{(jugador.Genero == Jugadores.eGenero.Masculino ? "El Sr. " : jugador.Genero == Jugadores.eGenero.Femenino ? "La Sra. " : "")}{jugador.Nombre} ya tiene {NumeroCartas} cartas sobre la mesa boca abajo.", ConsoleColor.DarkGray);

            }
        }
        public void Rondas(bool primeraApuesta)
        {
            int op = 0;
            bool rondaActiva = true;
            bool partidaActiva = true;
            int ronda = 1;



            if (primeraApuesta)
            {

                Console.WriteLine(@"
Ya tenemos las cartas repartidas, es hora de comenzar con lo interesante. Que comiencen esas apuestas!!. 
Se empieza con el pre-flop, antes de que revele las 3 primeras cartas del centro.
Podeis pasar (check), apostar (bet) o retirarse (fold). Y cuando alguien apueste se podra subir la apuesta (raise) o igualarla (call)");

            }
            while (partidaActiva)
            {
                rondaActiva = CentroDeMesa(ronda);

                if (!rondaActiva){ break; }

                while (rondaActiva)
                {
                    rondaActiva = false;

                    foreach (var jugador in listaJugadores.Where(j => j.Jugando))
                    {
                        bool turnoRealizado = false;

                        while (!turnoRealizado)
                        {
                            MostrarCartasConColor($"\n{(jugador.Genero == Jugadores.eGenero.Masculino ? "El Sr. " : jugador.Genero == Jugadores.eGenero.Femenino ? "La Sra. " : "")}{jugador.Nombre} dispone de {jugador.Dinero} fichas y estas son sus cartas: ", jugador);
                            Console.WriteLine($@"
¿Qué es lo que quieres hacer? 
1. Check - Pasar
2. Bet - Apostar
3. Call - Igualar
4. Raise - Subir
5. Fold - Abandonar

La apuesta actual es de {apuesta}");

                            while (!int.TryParse(Console.ReadLine(), out op) || op < 1 || op > 5)
                            {
                                Menu.ImprimirConColor("\nOpción no válida, elige una opción entre 1 y 5.\n", ConsoleColor.Red);
                            }

                            switch (op)
                            {
                                case 1:
                                    Menu.ImprimirConColor("\nHas elegido pasar turno.", ConsoleColor.DarkGray);
                                    if (apuesta > jugador.Apuesta)
                                    {
                                        Menu.ImprimirConColor("\nNo puedes pasar turno, hay una apuesta en juego. Iguala, sube o abandona la ronda.", ConsoleColor.Red);
                                        Menu.ImprimirConColor($"\n{(jugador.Genero == Jugadores.eGenero.Masculino ? "El Sr. " : jugador.Genero == Jugadores.eGenero.Femenino ? "La Sra. " : "")}{jugador.Nombre} debe de elegir otra opcion.", ConsoleColor.Gray);
                                    }
                                    else
                                    {
                                        turnoRealizado = true;
                                    }
                                    break;
                                case 2:
                                    Menu.ImprimirConColor("\nHas elegido apostar fichas.", ConsoleColor.DarkGray);
                                    if (Apuestas(jugador))
                                    {
                                        turnoRealizado = true;
                                        rondaActiva = true;

                                    }
                                    break;
                                case 3:
                                    Menu.ImprimirConColor("\nHas elegido igualar la apuesta.", ConsoleColor.DarkGray);
                                    if (IgualarApuesta(jugador))
                                    {
                                        turnoRealizado = true;
                                    }
                                    break;
                                case 4:
                                    Menu.ImprimirConColor("\nHas elegido subir la apuesta.", ConsoleColor.DarkGray);
                                    if (SubirApuesta(jugador))
                                    {
                                        turnoRealizado = true;
                                        rondaActiva = true;
                                    }
                                    break;
                                case 5:
                                    Menu.ImprimirConColor("\nHas elegido retirarte.", ConsoleColor.DarkGray);
                                    turnoRealizado = true;
                                    jugador.Jugando = false;
                                    break;
                            }
                        }
                    }
                }

                ronda++;
            }

            DeterminadrGanador();
        }
        public bool SubirApuesta(Jugadores jugador)
        {
            int apuestaNueva = 0;

            if (apuesta == 0)
            {
                Menu.ImprimirConColor("\nAun no se ha realizado ninguna apuesta.\n", ConsoleColor.Red);
                Menu.ImprimirConColor($"\n{(jugador.Genero == Jugadores.eGenero.Masculino ? "El Sr. " : jugador.Genero == Jugadores.eGenero.Femenino ? "La Sra. " : "")}{jugador.Nombre} debe de elegir otra opcion.\n", ConsoleColor.Gray);
                return false;
            }
            else if (apuesta > jugador.Dinero)
            {
                Menu.ImprimirConColor("\nNo puedes igualar la apuesta. No tienes tantas fichas.\n", ConsoleColor.Red);
                Menu.ImprimirConColor($"\n{(jugador.Genero == Jugadores.eGenero.Masculino ? "El Sr. " : jugador.Genero == Jugadores.eGenero.Femenino ? "La Sra. " : "")}{jugador.Nombre} debe de elegir otra opcion.\n", ConsoleColor.Gray);

                return false;
            }
            else
            {
                Console.WriteLine("\nIngresa una cantidad de fichas que quieras subir a la apuesta.\n");

                while (!int.TryParse(Console.ReadLine(), out apuestaNueva) || apuestaNueva <= 0 || apuestaNueva > jugador.Dinero - apuesta)
                {
                    Menu.ImprimirConColor("\nApuesta inválida. Ingresa un valor positivo y no mayor a tus fichas disponibles.\n", ConsoleColor.Red);

                    return false;
                }

                jugador.Apuesta = apuesta + apuestaNueva;
                jugador.Dinero -= apuestaNueva;
                apuesta = jugador.Apuesta;

                Menu.ImprimirConColor($"\n{(jugador.Genero == Jugadores.eGenero.Masculino ? "El Sr. " : jugador.Genero == Jugadores.eGenero.Femenino ? "La Sra. " : "")}{jugador.Nombre} sube la apuesta a {jugador.Apuesta}\n", ConsoleColor.DarkGray);

                return true;
            }


        }
        public bool IgualarApuesta(Jugadores jugador)
        {



            if (apuesta == 0)
            {
                Menu.ImprimirConColor("\nAun no se ha realizado ninguna apuesta.\n", ConsoleColor.Red);
                Menu.ImprimirConColor($"\n{(jugador.Genero == Jugadores.eGenero.Masculino ? "El Sr. " : jugador.Genero == Jugadores.eGenero.Femenino ? "La Sra. " : "")}{jugador.Nombre} debe de elegir otra opcion.\n", ConsoleColor.Gray);
                return false;
            }
            else if (apuesta > jugador.Dinero)
            {
                Menu.ImprimirConColor("\nNo puedes igualar la apuesta. No tienes tantas fichas.\n", ConsoleColor.Red);
                Menu.ImprimirConColor($"\n{(jugador.Genero == Jugadores.eGenero.Masculino ? "El Sr. " : jugador.Genero == Jugadores.eGenero.Femenino ? "La Sra. " : "")}{jugador.Nombre} debe de elegir otra opcion.\n", ConsoleColor.Gray);

                return false;
            }
            else
            {
                jugador.Apuesta = apuesta;
                jugador.Dinero -= apuesta;

                Menu.ImprimirConColor($"\n{(jugador.Genero == Jugadores.eGenero.Masculino ? "El Sr. " : jugador.Genero == Jugadores.eGenero.Femenino ? "La Sra. " : "")}{jugador.Nombre} iguala la apuesta con {jugador.Apuesta}\n", ConsoleColor.DarkGray);

                return true;
            }


        }
        public bool Apuestas(Jugadores jugador)
        {
            int apuestaNueva = 0;


            if (apuesta > jugador.Apuesta)
            {
                Menu.ImprimirConColor("\nYa hay una apuesta en juego y debes igualarla primero, o subirla.\n", ConsoleColor.Red);
                return false;
            }
            else if (apuestaNueva >= 1 || apuestaNueva <= jugador.Dinero)
            {
                apuesta += jugador.Apuesta;

                jugador.Apuesta = apuesta;
                jugador.Dinero -= apuesta;


                Menu.ImprimirConColor($"\n{(jugador.Genero == Jugadores.eGenero.Masculino ? "El Sr. " : jugador.Genero == Jugadores.eGenero.Femenino ? "La Sra. " : "")}{jugador.Nombre} apuesta {jugador.Apuesta}\n", ConsoleColor.DarkGray);
                return true;
            }
            Menu.ImprimirConColor("\nApuesta inválida. Ingresa un valor positivo y no mayor a tus fichas disponibles.\n", ConsoleColor.Red);
            return false;
            
        }
        public void DeterminadrGanador()
        {
            Console.WriteLine("\nHemos llegado al momento de determinar el ganador de la ronda. Vamos a ver las cartas de cada uno y de la mesa.");

            foreach (var jugador in listaJugadores.Where(j => j.Nombre != "RobertoMesa#0001BotCrupier"))
            {
                if (jugador.Jugando)
                {
                    MostrarCartasConColor($"\n{(jugador.Genero == Jugadores.eGenero.Masculino ? "El Sr. " : jugador.Genero == Jugadores.eGenero.Femenino ? "La Sra. " : "")}{jugador.Nombre} dispone de {jugador.Dinero} fichas y estas son sus cartas: ", jugador);
                }
                apuesta += jugador.Apuesta;
            }

            var roberto = listaJugadores.FirstOrDefault(j => j.Nombre == "RobertoMesa#0001BotCrupier");
            MostrarCartasConColor($"\nEl crupier Roberto dispone en el centro de la mesa estas cartas: ", roberto);

            Console.WriteLine($"\nSe esta jugando la mano a {apuesta}");

        }
        public void RepartoDeCartasRoberto(int NumeroCartas, Jugadores jugador)
        {

            // Verifica que haya suficientes cartas en la baraja
            if (cartasBaraja.cartas.Count < NumeroCartas)
            {
                Menu.ImprimirConColor("No hay suficientes cartas en la baraja para repartir.", ConsoleColor.Red);
                return; // Sale del método si no hay suficientes cartas
            }

            // Reparte 5 cartas al jugador
            for (int i = 0; i < NumeroCartas; i++)
            {
                jugador.Mazo.Add(cartasBaraja.cartas[0]); // Añadir la primera carta
                cartasBaraja.cartas.RemoveAt(0); // Eliminar la carta de la baraja
            }
        }
        public bool CentroDeMesa(int ronda)
        {

            var roberto = listaJugadores.FirstOrDefault(j => j.Nombre == "RobertoMesa#0001BotCrupier");

            if (ronda == 1)
            {

                jugador.Mazo = new List<Carta>(); // Inicializa el mazo aquí

                RepartoDeCartasRoberto(3, roberto);

                Menu.ImprimirConColor("\nRoberto pone 3 cartas boca abajo en el centro de la mesa para la ronda del flop.", ConsoleColor.DarkGray);

                return true;

            }
            else if (ronda == 2)
            {
                MostrarCartasConColor("\nRoberto muestra las 3 cartas que tiene sobre la mesa.", roberto);

                RepartoDeCartasRoberto(1, roberto);

                Menu.ImprimirConColor("\nRoberto pone 1 carta mas boca abajo en el centro de la mesa para la ronda del turn.", ConsoleColor.DarkGray);

                return true;

            }
            else if (ronda == 3)
            {
                MostrarCartasConColor("\nRoberto muestra las 4 cartas que tiene sobre la mesa.", roberto);

                RepartoDeCartasRoberto(1, roberto);

                Menu.ImprimirConColor("\nRoberto pone 1 carta mas boca abajo en el centro de la mesa para la ronda del river.", ConsoleColor.DarkGray);

                return true;
            }
            else
            {
                MostrarCartasConColor("Roberto muestra las 5 cartas que tiene sobre la mesa.", roberto);

                Menu.ImprimirConColor("\nRoberto ya no tiene mas cartas que mostrar. Esta es la ronda final.", ConsoleColor.DarkGray);

                Console.WriteLine("\nSeñores y señoras, esta es la ronda final. Teneis que acabar vuestras apuestas aqui!");

                return false;

            }
        }
        public void ReiniciarBaraja()
        {
            cartasBaraja = new Baraja();  // Crea una nueva instancia de la baraja
            cartasBaraja.CrearBarajaFrancesa();  // Genera las cartas nuevamente
            cartasBaraja.MezclarBaraja();  // Mezcla la baraja

        }
        public void MostrarCartasConColor(string texto, Jugadores jugador)
        {

            Console.Write($"{texto}\n");

            // Verifica si el mazo tiene cartas
            if (jugador.Mazo.Count > 0)
            {
                foreach (Carta carta in jugador.Mazo)
                {
                    // Determina el color basado en el palo de la carta
                    ConsoleColor color = carta.paloFrances == Carta.ePaloFrancesa.Picas ||
                                          carta.paloFrances == Carta.ePaloFrancesa.Treboles ?
                                          ConsoleColor.DarkBlue :
                                          ConsoleColor.DarkRed;

                    // Imprime la carta con el color determinado
                    Menu.ImprimirConColor(carta.ToString(), color);
                }
            }
            else
            {
                Menu.ImprimirConColor("\nNo hay cartas en el mazo.\n", ConsoleColor.Red);
            }


        }
    }
}
