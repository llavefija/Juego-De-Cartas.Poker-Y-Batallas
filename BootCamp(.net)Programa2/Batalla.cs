using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BootCamp_.net_Programa2;
using static BootCamp_.net_Programa2.Baraja;

namespace BootCamp_.net_Programa2
{
    internal class Batalla
    {
        Baraja baraja; // Se crea la clase baraja
        List<Carta> cartasJugadores; // Se crea una lista que almacenara la carta del jugador
        List<List<Carta>> mazosJugadores; // Se crea una lista para almacenar los mazos de los jugadores
        List<Carta> cartasSeleccionadas; // Se crea una lista para almacenar la carta seleccionada por cada jugador
        Dictionary<int, int> puntos; // Se crea un diccionario para almacenar los puntos de los jugadores

        // Metodo constructor de Batalla
        public Batalla()
        {
            baraja = new Baraja(); // Se inicializa la clase baraja.
            baraja.CrearBarajaEspañola(); // Se crea una baraja española.
            cartasJugadores = new List<Carta>(); // Se inicializa la lista cartas jugadores
            mazosJugadores = new List<List<Carta>>(); // Se inicializa la lista mazos jugadores
            cartasSeleccionadas = new List<Carta>(); // Inicializar la lista de cartas seleccionadas
            puntos = new Dictionary<int, int>(); // Se inicializa el diccionario puntos
        }

        #region Metodo para iiciar batallas del modo seleccionado
        // Metodo que inicia la batalla dependiendo del tipo seleccionado
        public void IniciarBatalla(bool directo)
        {
            // Muestra el titulo del juego
            Menu.ImprimirConColor("\nJUEGO DE BATALLAS " + (directo == true ? "ATAQUE DIRECTO INICIADO" : "ATAQUE CON MAZOS INICIADO"), ConsoleColor.Yellow);

            // Muestra las normas del juego
            Console.WriteLine(@"
·Este juego se juega con 48 cartas de la baraja española (1-9, sota, caballo y rey),
con los respectivos palos (oros, bastos, espadas y copas).

·Es un juego de dos hasta cinco jugadores, gana quien consiga el número más alto del 1 al 12.");

            if (directo)
            {
                Console.WriteLine("\n·En el modo directo cada jugador agarra solo una carta y la muestra al instante.");
                AtaqueDirecto(directo); // Inicia el juego con el modo directo

            }
            else
            {
                Console.WriteLine(@"
·En el modo batalla con mazo cada jugador dispone de un mazo con cartas y luchan hasta agotarlo.
Gana quien tenga mas rondas ganadas acumuladas la partida acaba una vez todos se queden sin cartas. O hasta llegar a la ronda numero 150.");
                AtaqueConMazo(directo); // Inicia el juego con el modo por mazos
            }


        }
        #endregion

        #region Metodos que inician las batallas directas o con mazo
        // Metodo que inicia la batalla por ataque directo
        public void AtaqueDirecto(bool directo)
        {

            try
            {
                Console.WriteLine("\nCuantos jugadores van a jugar? (2-5)\n"); // Pregunta cuantos jugadores va a haber en la partida

                int jugadores = IntroducirJugadores(); // Invoca el metodo para introducir el numero de jugadores

                MostrarYRepartirCartas(jugadores, directo); // Reparte una carta aleatoria a cada jugador y la muestra

                DeterminarGanador(directo); // Determina el ganador e inicia un desempate si hace falta
            }
            catch (Exception ex)
            {
                Menu.ImprimirConColor($"\nOcurrió un error durante la batalla: {ex.Message}", ConsoleColor.Red); // Muestra un mensage de error si sucede alguno durante la ejecucion
            }
        }

        // Metodo que inicia la batalla por ataque con mazos
        public void AtaqueConMazo(bool directo)
        {
            try
            {
                Console.WriteLine("\nCuantos jugadores van a jugar? (2-5)\n"); // Pregunta cuantos jugadores va a haber en la partida

                int jugadores = IntroducirJugadores(); // Invoca el metodo para introducir el numero de jugadores

                int numeroCartasJugador = CartasPorJugador(jugadores); // Invoca el metodo para saber cuantas cartas se van a repartir a cada jugador

                Console.WriteLine($"\nSe juega con un mazo de {numeroCartasJugador * jugadores} para formar un mazo {numeroCartasJugador} cartas de cada jugador repartidas al azar."); // Informa del numero de cartas total y mazo de cada jugador

                RepartirMazos(jugadores, numeroCartasJugador, directo); // Invoca el metodo que reparte los mazos a cada jugador

                BatallaMazos(jugadores, directo); // Invoca el metodo para realizar el juego y su proceso de seleccion de cartas y victoria

                ImprimirResultados(directo); // Invoca el metodo para imprimir los resultados de la batalla

            }
            catch (Exception ex)
            {
                Menu.ImprimirConColor($"\nOcurrió un error durante la batalla: {ex.Message}", ConsoleColor.Red); // Muestra un mensaje de error si sucede alguno durante la ejecucion
            }
        }
        #endregion

        #region Metodos principales del programa: Introducir jugadores y saber cuantas cartas se repartiran

        public int CartasPorJugador(int jugadores)
        {
            int numeroCartasJugador = 48;

            numeroCartasJugador = numeroCartasJugador / jugadores;

            for (int i = 0; numeroCartasJugador % 2 != 0; i++)
            {
                if (numeroCartasJugador % 2 != 0)
                {
                    numeroCartasJugador--;
                }
            }

            return numeroCartasJugador;

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
        #endregion

        #region Metodos para realizar la batallas: Repartir cartas > Mostrar o seleccionar carta > Determinar ganador y desempatar > Mostrar Resultados
        // Metodo que sigue el proceso de la batalla por mazos
        public void BatallaMazos(int jugadores, bool directo)
        {
            int ronda = 1;
            do
            {
                cartasSeleccionadas.Clear(); // Limpiar las cartas seleccionadas de rondas anteriores

                Console.WriteLine($"\nRonda nº{ronda}.\n");
                Console.WriteLine($"Los jugadores cogen la primera carta que tengan arriba de su mazo.\n");

                // Seleccionar cartas de cada jugador
                for (int numeroDelJugador = 1; numeroDelJugador <= jugadores; numeroDelJugador++)
                {
                    cartasJugadores = mazosJugadores[numeroDelJugador - 1]; // Obtener el mazo del jugador actual

                    if (cartasJugadores.Count == 0)
                    {
                        Carta cartaSeleccionada = cartaSeleccionada = new Carta(); // Crear una carta vacía si no hay cartas
                        Console.WriteLine($"El jugador nº{numeroDelJugador} no tiene cartas");
                        cartasSeleccionadas.Add(cartaSeleccionada); // Añadir a cartas seleccionadas

                    }
                    else
                    {
                        Carta cartaSeleccionada = cartasJugadores[0]; // Obtener la primera carta
                        MostrarCartasConColor($"El jugador nº{numeroDelJugador} ha seleccionado la carta {cartaSeleccionada.Numero} ", cartaSeleccionada);
                        cartasSeleccionadas.Add(cartaSeleccionada); // Añadir a cartas seleccionadas
                    }
                }

                Console.Write($"\nPresiona cualquier tecla para ver el resultado de la ronda.\n");
                Console.ReadLine();

                int ganador = DeterminarGanador(directo); // Determinar el ganador

                // Repartir cartas según el ganador
                for (int numeroDelJugador = 1; numeroDelJugador <= jugadores; numeroDelJugador++)
                {
                    if (mazosJugadores[numeroDelJugador - 1].Count == 0) continue; // Saltar si no hay cartas

                    if (ganador == 0) // Empate, descartar cartas
                    {
                        mazosJugadores[numeroDelJugador - 1].RemoveAt(0); // Eliminar la primera carta
                    }
                    else
                    {
                        Carta cartaSeleccionada = mazosJugadores[numeroDelJugador - 1][0]; // Obtener carta
                        mazosJugadores[ganador - 1].Add(cartaSeleccionada); // Añadir al mazo del ganador
                        mazosJugadores[numeroDelJugador - 1].RemoveAt(0); // Eliminar la carta del mazo del jugador perdedor
                    }

                }

                // Si la partida llega al maximo de partidas finaliza
                if (ronda == 150)
                {
                    Menu.ImprimirConColor("\nLa partida finaliza en esta ronda. Se ha llegado al maximo de 150 rondas.", ConsoleColor.Red);
                    break;
                }

                ronda++;

            }
            // Continuar hasta que solo un jugador tenga cartas
            while (mazosJugadores.Count(m => m.Count > 0) > 1);
        }

        // Metodo para barajar las cartas y repartir mazos o cartas unicas a los jugadores
        public Carta BarajarYRepartirCartas(bool directo)
        {
            Random r = new Random();

            if (directo)
            {
                Console.WriteLine("Pulsa cualquier tecla para coger una carta."); // Espera a que el usuario coja una carta aleatoria si es unicamente ataque directo

                Console.ReadLine();
            }

            int index = r.Next(baraja.cartas.Count); // Seleccionar un índice aleatorio
            Carta carta = baraja.cartas[index]; // Obtener la carta en ese índice
            baraja.cartas.RemoveAt(index); // Eliminar la carta de la baraja para no seleccionar la misma

            //Menu.ImprimirConColor($"\nEl jugador nº{numeroDelJugador} elegido una carta correctamente.", ConsoleColor.Green);

            return carta;
        }

        // Metodo que sirve para que los usuarios recojan una carta en ataque directo
        public void MostrarYRepartirCartas(int jugadores, bool directo)
        {
            cartasJugadores.Clear(); // Limpiar las cartas anteriores

            try
            {
                // Se inicia un bucle para que todos los usuarios cojan una carta aleatoria
                for (int numeroDelJugador = 1; numeroDelJugador <= jugadores; numeroDelJugador++)
                {
                    // Turno del jugador
                    Console.WriteLine($"\nTurno del jugador nº{numeroDelJugador}\n"); // Informa del numero del jugador
                    Carta cartaSeleccionada = BarajarYRepartirCartas(directo); // Invoca al metodo para recoger una carta para la batalla
                    cartasJugadores.Add(cartaSeleccionada); // Añade la carta a la baraja del jugador, en este caso es carta unica

                    MostrarCartasConColor($"El jugador nº{numeroDelJugador} ha seleccionado: {cartaSeleccionada.Numero} de ", cartaSeleccionada); // Se invoca el metodo para mostrar la carta seleccionada de manera estetica

                    cartasSeleccionadas.Add(cartaSeleccionada); // Añade la carta a la lista de cartas seleccionadas
                }
            }
            catch (Exception ex)
            {
                Menu.ImprimirConColor($"\nHa sucedido un error al intentar agarrar cartas y mostrarlas: {ex.Message}", ConsoleColor.Red); // Se informa si sucede algun error
            }
        }

        // Metodo para repartir los mazos entre los jugadores
        public void RepartirMazos(int jugadores, int cartasMazo, bool directo)
        {
            try
            {
                mazosJugadores.Clear(); // Limpiar mazos anteriores

                // Recorre todos los jugadores para repartir un mazo aleatorio entre ellos
                for (int numeroDelJugador = 1; numeroDelJugador <= jugadores; numeroDelJugador++)
                {
                    List<Carta> mazo = new List<Carta>(); // Inicializa un nuevo mazo
                    for (int numeroCartasRepartidas = 1; numeroCartasRepartidas <= cartasMazo; numeroCartasRepartidas++)
                    {
                        Carta cartaSeleccionada = BarajarYRepartirCartas(directo); // Invoca el metodo para recoger las cartas que forman el mazo
                        mazo.Add(cartaSeleccionada); // Añade la carta al mazo del jugador
                    }
                    Console.WriteLine($"\nEl jugador nº{numeroDelJugador} ha recogido sus {cartasMazo} cartas correspondientes al azar."); // Informa de que el jugador tiene las cartas al azar bien repartidas
                    mazosJugadores.Add(mazo); // Añade todo el mazo a la lista de mazos de los jugadores
                    puntos[numeroDelJugador] = 0; // Inicializa los puntos del jugador a 0
                }
            }
            catch (Exception ex)
            {
                Menu.ImprimirConColor($"\nHa sucedido un error al intentar repartir las cartas: {ex.Message}", ConsoleColor.Red); // Informa si ha sucedido algun error durante la ejecucion
            }
        }

        // Metodo que determina si hay algun ganador en la batalla
        public int DeterminarGanador(bool directo)
        {
            try
            {
                int ganadorIndex = 0;
                Carta cartaGanadora = cartasSeleccionadas.OrderByDescending(c => c.Numero).First(); // Ordena las cartas por orden descendiente y coge la primera
                var ganadores = cartasSeleccionadas.Where(c => c.Numero == cartaGanadora.Numero).ToList(); // Comprueba si hay varios ganadores

                if (ganadores.Count == 1)
                {
                    ganadorIndex = cartasSeleccionadas.IndexOf(cartaGanadora) + 1; // Si solo hay un ganador mostrara el ganador y con que carta ha ganado
                    Menu.ImprimirConColor($"\nEl jugador {ganadorIndex} ha ganado la batalla con la carta {cartaGanadora.Numero} de {cartaGanadora.paloEspanol}.", ConsoleColor.Cyan);

                    // Si el modo es por mazos le sumara un punto a este jugador
                    if (!directo)
                    {
                        if (!puntos.ContainsKey(ganadorIndex))
                        {
                            puntos[ganadorIndex] = 0; // Inicializar si no existe
                        }
                        puntos[ganadorIndex]++; // Incrementa en uno los puntos
                    }
                }
                else
                {
                    // Si hay mas de un ganador se inicia un desempate

                    Menu.ImprimirConColor($"\nHay un empate entre los jugadores con la carta {cartaGanadora.Numero}.", ConsoleColor.Magenta);
                    if (directo)
                    {
                        // Si el modo es una batalla directa se iniciara el desempate entre los jugadores que han ganado
                        Console.WriteLine($"\n¡Se hará un desempate! Los jugadores con la carta {cartaGanadora.Numero} volverán a seleccionar una carta en orden de jugador.\n");
                        IniciarDesempatePorCartas(ganadores, directo);
                    }
                    else { Console.WriteLine($"\nNo se sumara victoria para nadie.\n"); } // Si no es directo solo se informara que no suma puntos

                }
                return ganadorIndex;
            }
            catch (Exception ex)
            {
                Menu.ImprimirConColor($"\nHa sucedido un error al intentar asignar un ganador: {ex.Message}", ConsoleColor.Red);
                return 0;
            }

        }

        // Metodo para desempatar en ataque por mazos
        public void IniciarDesempatePorJugadores(List<int> jugadoresEmpatados, bool directo)
        {
            List<Carta> cartasDesempate = new List<Carta>(); // Lista de cartas del desempate
            List<int> jugadoresIndices = new List<int>(); // Lista de índices de jugadores

            // Recorre los jugadores empatados para realizar el desempate
            for (int i = 0; i < jugadoresEmpatados.Count; i++)
            {
                int jugadorIndex = jugadoresEmpatados[i];
                jugadoresIndices.Add(jugadorIndex);

                Console.Write($"\nTurno del jugador nº{jugadorIndex}"); // Mostrar el turno del jugador

                // Repartir nueva carta para el desempate
                baraja.CrearBarajaEspañola(); // Se crea una nueva baraja porque la antigua ya esta vacia
                Carta nuevaCarta = BarajarYRepartirCartas(true);
                cartasDesempate.Add(nuevaCarta); // Añadir carta a la lista de desempate

                // Mostrar la nueva carta seleccionada con formato
                MostrarCartasConColor($"El jugador ha seleccionado para el desempate: {nuevaCarta.Numero} de ", nuevaCarta);
            }

            // Determinar la carta ganadora
            Carta cartaGanadoraDesempate = cartasDesempate.OrderByDescending(c => c.Numero).First();
            var nuevosGanadores = cartasDesempate.Where(c => c.Numero == cartaGanadoraDesempate.Numero).ToList();

            // Verificar si hay un único ganador tras el desempate
            if (nuevosGanadores.Count == 1)
            {
                int ganadorIndex = jugadoresIndices[cartasDesempate.IndexOf(cartaGanadoraDesempate)];
                Menu.ImprimirConColor($"\nEl jugador nº{ganadorIndex} ha ganado la batalla con la carta {cartaGanadoraDesempate.Numero} de {cartaGanadoraDesempate.paloEspanol}.", ConsoleColor.Cyan);
            }
            else
            {
                // Si hay empate nuevamente, iniciar otro desempate
                Menu.ImprimirConColor("\nEl desempate ha terminado en empate. Iniciando otra ronda de desempate...", ConsoleColor.Magenta);
                IniciarDesempatePorJugadores(nuevosGanadores.Select(c => c.Numero).ToList(), directo);
            }
        }

        // Metodo para desempatar en ataque directo
        public void IniciarDesempatePorCartas(List<Carta> cartasEmpate, bool directo)
        {
            List<Carta> cartasDesempate = new List<Carta>(); // Lista de cartas del desempate
            List<int> jugadoresIndices = new List<int>(); // Lista de índices de jugadores

            // Recorre los jugadores empatados para realizar el desempate
            for (int i = 0; i < cartasEmpate.Count; i++)
            {
                // Obtener el índice del jugador basado en la carta seleccionada
                int jugadorIndex = cartasSeleccionadas.IndexOf(cartasEmpate[i]) + 1;
                jugadoresIndices.Add(jugadorIndex);

                Console.Write($"\nTurno del jugador nº{jugadorIndex}\n"); // Mostrar el turno del jugador

                // Repartir nueva carta para el desempate
                Carta nuevaCarta = BarajarYRepartirCartas(directo);
                cartasDesempate.Add(nuevaCarta); // Añadir carta a la lista de desempate

                // Mostrar la nueva carta seleccionada con formato
                MostrarCartasConColor($"El jugador ha seleccionado para el desempate: {nuevaCarta.Numero} de ", nuevaCarta);
            }

            // Determinar la carta ganadora
            Carta cartaGanadoraDesempate = cartasDesempate.OrderByDescending(c => c.Numero).First();
            var nuevosGanadores = cartasDesempate.Where(c => c.Numero == cartaGanadoraDesempate.Numero).ToList();

            // Verificar si hay un único ganador tras el desempate
            if (nuevosGanadores.Count == 1)
            {
                int ganadorIndex = jugadoresIndices[cartasDesempate.IndexOf(cartaGanadoraDesempate)];
                Menu.ImprimirConColor($"\nEl jugador nº{ganadorIndex} ha ganado la batalla con la carta {cartaGanadoraDesempate.Numero} de {cartaGanadoraDesempate.paloEspanol}.", ConsoleColor.Cyan);
            }
            else
            {
                // Si hay empate nuevamente, iniciar otro desempate
                Menu.ImprimirConColor("\nEl desempate ha terminado en empate. Iniciando otra ronda de desempate...", ConsoleColor.Magenta);
                IniciarDesempatePorCartas(nuevosGanadores, directo);
            }
        }


        public void ImprimirResultados(bool directo)
        {
            // Ordenar los puntos en orden descendente
            puntos = puntos.OrderByDescending(p => p.Value).ToDictionary(p => p.Key, p => p.Value);

            // Obtener la puntuación más alta
            int puntuacionMaxima = puntos.Values.First();

            // Filtrar los jugadores con la puntuación más alta
            var ganadores = puntos.Where(c => c.Value == puntuacionMaxima).ToList();

            int posicion = 1;

            // Mostrar las posiciones de los jugadores
            foreach (KeyValuePair<int, int> jugadorPuntos in puntos)
            {
                Console.WriteLine($"\nEn posición número {posicion} el jugador nº{jugadorPuntos.Key} con {jugadorPuntos.Value} puntos.");
                posicion++;
            }

            if (ganadores.Count > 1)
            {
                // Convertir la lista de KeyValuePair a una lista de enteros (jugadores)
                List<int> jugadoresEmpatados = ganadores.Select(g => g.Key).ToList();

                // Llamar al método para iniciar el desempate con los jugadores empatado
                IniciarDesempatePorJugadores(jugadoresEmpatados, directo);
            }
            else
            {
                var ganador = ganadores.First(); // Obtener el único ganador
                Menu.ImprimirConColor($"\nEl jugador nº{ganador.Key} es el ganador con {ganador.Value} puntos.", ConsoleColor.Cyan);
            }
        }


        // Metodo para mostrar con colores un texto y la carta segun el tipo de palo
        public void MostrarCartasConColor(string texto, Carta carta)
        {
            Console.Write($"{texto}");
            Menu.ImprimirConColor(carta.paloEspanol.ToString(),
                (carta.paloEspanol == Carta.ePaloEspanola.Espadas ? ConsoleColor.Blue :
                 carta.paloEspanol == Carta.ePaloEspanola.Copas ? ConsoleColor.Red :
                 carta.paloEspanol == Carta.ePaloEspanola.Bastos ? ConsoleColor.Green :
                 carta.paloEspanol == Carta.ePaloEspanola.Oros ? ConsoleColor.Yellow :
                 ConsoleColor.White));
        }

        #endregion

    }


}
