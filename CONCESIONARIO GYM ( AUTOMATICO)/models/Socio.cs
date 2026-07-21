using System;
using System.Collections.Generic;
using System.Text;

namespace CONCESIONARIO_GYM___AUTOMATICO_.models
{
    public class Socio
    {
        private int id;
        private string nombre;
        private string cedula;
        private int edad;
        private bool estadoMembresia;
        private string tipoMembresia;

        public int Id { get => id; set => id = value; }
        public string Nombre
        {
            get => nombre;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("El nombre del socio no puede estar vacío.");
                }
                nombre = value;
            }
        }
        public string Cedula { get => cedula; set => cedula = value; }
        public int Edad
        {
            get => edad;
            set
            {
                if (!EsMayorEdad(value))
                {
                    throw new Exception("El socio debe ser mayor de edad.");
                }
                edad = value;
            }
        }
        public bool EstadoMembresia { get => estadoMembresia; set => estadoMembresia = value; }
        public string TipoMembresia
        {
            get => tipoMembresia;
            set
            {
                string valorLimpio = value.Trim().ToLower();
                if (valorLimpio != "estandar" && valorLimpio != "vip")
                {
                    throw new Exception("El tipo de membresía debe ser 'estandar' o 'vip'.");
                }
                tipoMembresia = valorLimpio;
            }
        }

        // Constructor
        public Socio(int id, string nombre, string cedula, int edad, bool estadoMembresia, string tipoMembresia)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.Cedula = cedula;
            this.Edad = edad;
            this.EstadoMembresia = estadoMembresia;
            this.TipoMembresia = tipoMembresia;
        }

        // Métodos auxiliares de validación
        public Boolean EsMayorEdad(int edad)
        {
            return edad >= 18;
        }

        public void Presentar()
        {
            string estado = this.EstadoMembresia ? "ACTIVA" : "VENCIDA";
            Console.WriteLine($"[ID: {this.Id}] {this.Nombre} - Cédula: {this.Cedula} | Tipo: {this.TipoMembresia.ToUpper()} | Estado: {estado}");
        }

        public string ObtenerFichaTecnica()
        {
            return $"[{TipoMembresia.ToUpper()}] #{Id} - {Nombre} ({Edad} años)";
        }
    }
}
