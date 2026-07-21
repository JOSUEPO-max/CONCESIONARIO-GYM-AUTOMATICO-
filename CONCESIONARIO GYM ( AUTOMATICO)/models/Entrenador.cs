using System;
using System.Collections.Generic;
using System.Text;

namespace CONCESIONARIO_GYM___AUTOMATICO_.models
{
    public class Entrenador
    {
        private int id;
        private string nombre;
        private string especialidad;

        public int Id { get => id; set => id = value; }
        public string Nombre
        {
            get => nombre;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("El nombre del entrenador no puede estar vacío.");
                }
                nombre = value;
            }
        }
        public string Especialidad { get => especialidad; set => especialidad = value; }

        public Entrenador(int id, string nombre, string especialidad)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.Especialidad = especialidad;
        }

        public void PresentarEntrenador()
        {
            Console.WriteLine($"[Entrenador #{this.Id}] {this.Nombre} - Especialidad: {this.Especialidad}");
        }
    }
}