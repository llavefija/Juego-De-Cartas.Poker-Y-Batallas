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
        eGenero genero;
        bool jugando = true;


        public enum eGenero { 
            Femenino = 1,
            Masculino = 2,
            NoEspecificar = 3
        }
        public Jugadores(string nombre, int dinero, int apuesta, eGenero genero, bool jugando)
        {
            this.Nombre = nombre;
            this.Dinero = dinero;
            this.Apuesta = apuesta;
            this.Genero = genero;
            this.Jugando = jugando;
        }

        public Jugadores()
        {
            this.Nombre = nombre;
            this.Dinero = dinero;
            this.Apuesta = apuesta;
            this.Genero = genero;
            this.Jugando = jugando;
        }

        public string Nombre { get => nombre; set => nombre = value; }
        public int Dinero { get => dinero; set => dinero = value; }
        public int Apuesta { get => apuesta; set => apuesta = value; }
        public eGenero Genero { get => genero; set => genero = value; }
        public bool Jugando { get => jugando; set => jugando = value; }

        internal List<Carta> Mazo { get => mazo; set => mazo = value; }
    }
}
