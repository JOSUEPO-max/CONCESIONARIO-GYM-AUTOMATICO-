using System;
using System.Collections.Generic;
using System.Text;

namespace CONCESIONARIO_GYM___AUTOMATICO_.models
{
    public class Gimnasio
    {
        private string nombre;
        private string direccion;
        private List<Socio> socios;
        private string recepcionista;
        private int capacidadMaxima;

        public string Nombre { get => nombre; set => nombre = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public List<Socio> Socios { get => socios; set => socios = value; }
        public string Recepcionista
        {
            get => recepcionista;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("El nombre del recepcionista no puede estar vacío.");
                }
                recepcionista = value;
            }
        }
        public int CapacidadMaxima
        {
            get => capacidadMaxima;
            set
            {
                if (value <= 0 || value > 500)
                {
                    throw new Exception("La capacidad máxima debe estar entre 1 y 500 personas.");
                }
                capacidadMaxima = value;
            }
        }

        public Gimnasio(string nombre, string direccion, string recepcionista, int capacidadMaxima)
        {
            this.Nombre = nombre;
            this.Direccion = direccion;
            this.Socios = new List<Socio>();
            this.Recepcionista = recepcionista;
            this.CapacidadMaxima = capacidadMaxima;
        }

        public void AgregarSocio(Socio objSocio)
        {
            if (objSocio == null)
            {
                Console.WriteLine("Error: No se puede agregar un socio nulo al gimnasio.");
                return;
            }
            this.Socios.Add(objSocio);
            Console.WriteLine($"Socio {objSocio.Nombre} registrado correctamente.");
        }

        public void ListarSocios()
        {
            Console.WriteLine($"La lista de socios del gimnasio {this.Nombre} ubicado en {this.Direccion} es:");
            foreach (Socio objSocio in Socios)
            {
                objSocio.Presentar();
            }
        }

        public bool TieneAforoDisponible()
        {
            return this.Socios.Count < this.CapacidadMaxima;
        }
    }
}
