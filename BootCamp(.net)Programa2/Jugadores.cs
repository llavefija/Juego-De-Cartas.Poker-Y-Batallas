using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootCamp_.net_Programa2
{
    internal class Jugadores
    {
        string nombre;
        List<Carta> mazo;
        int dinero;
        int apuesta;

        public string Nombre { get => nombre; set => nombre = value; }
        public int Dinero { get => dinero; set => dinero = value; }
        public int Apuesta { get => apuesta; set => apuesta = value; }
        internal List<Carta> Mazo { get => mazo; set => mazo = value; }
    }
}
