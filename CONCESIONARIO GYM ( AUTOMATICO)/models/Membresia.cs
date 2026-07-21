using System;
using System.Collections.Generic;
using System.Text;

namespace CONCESIONARIO_GYM___AUTOMATICO_.models
{
    public class Membresia
    {
        private string nombrePlan;
        private double precioBase;
        private int duracionDias;

        public string NombrePlan
        {
            get => nombrePlan;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("El nombre del plan no puede estar vacío.");
                }
                nombrePlan = value;
            }
        }
        public double PrecioBase
        {
            get => precioBase;
            set
            {
                if (value <= 0)
                {
                    throw new Exception("El precio base debe ser mayor a 0.");
                }
                precioBase = value;
            }
        }
        public int DuracionDias
        {
            get => duracionDias;
            set
            {
                if (value <= 0)
                {
                    throw new Exception("La duración en días debe ser mayor a 0.");
                }
                duracionDias = value;
            }
        }

        public Membresia(string nombrePlan, double precioBase, int duracionDias)
        {
            this.NombrePlan = nombrePlan;
            this.PrecioBase = precioBase;
            this.DuracionDias = duracionDias;
        }

        public void MostrarInformacion()
        {
            Console.WriteLine($"Plan: {this.NombrePlan} | Precio: ${this.PrecioBase} | Duración: {this.DuracionDias} días");
        }
    }
}
