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

        // 🔴 1. NUEVOS CAMPOS PRIVADOS
        private string correo;
        private string telefono;

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

        // 🔴 2. NUEVAS PROPIEDADES CON VALIDACIÓN
        public string Correo
        {
            get => correo;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || !value.Contains("@"))
                {
                    throw new Exception("Debe ingresar un correo electrónico válido.");
                }
                correo = value;
            }
        }

        public string Telefono
        {
            get => telefono;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("El teléfono no puede estar vacío.");
                }
                telefono = value;
            }
        }

        // Constructor vacío (necesario para JSON)
        public Socio()
        {
        }

        // Constructor original (mantenido para no romper código existente)
        public Socio(int id, string nombre, string cedula, int edad, bool estadoMembresia, string tipoMembresia)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.Cedula = cedula;
            this.Edad = edad;
            this.EstadoMembresia = estadoMembresia;
            this.TipoMembresia = tipoMembresia;
        }

        // 🔴 3. NUEVO CONSTRUCTOR COMPLETO (INCLUYE CORREO Y TELÉFONO)
        public Socio(int id, string nombre, string cedula, int edad, bool estadoMembresia, string tipoMembresia, string correo, string telefono)
            : this(id, nombre, cedula, edad, estadoMembresia, tipoMembresia)
        {
            this.Correo = correo;
            this.Telefono = telefono;
        }

        // Métodos auxiliares de validación
        public Boolean EsMayorEdad(int edad)
        {
            return edad >= 18;
        }

        public void Presentar()
        {
            string estado = this.EstadoMembresia ? "ACTIVA" : "VENCIDA";
            Console.WriteLine($"[ID: {this.Id}] {this.Nombre} - Cédula: {this.Cedula} | Correo: {this.Correo} | Tipo: {this.TipoMembresia.ToUpper()} | Estado: {estado}");
        }

        public string ObtenerFichaTecnica()
        {
            return $"[{TipoMembresia.ToUpper()}] #{Id} - {Nombre} ({Edad} años) | {Correo} | Tel: {Telefono}";
        }
    }
}