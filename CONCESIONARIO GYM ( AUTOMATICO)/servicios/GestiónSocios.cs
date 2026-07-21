using CONCESIONARIO_GYM___AUTOMATICO_.models;
using System;
using System.Collections.Generic;
using System.Text;
namespace CONCESIONARIO_GYM___AUTOMATICO_.servicios
{
    public class GestionSocios
    {
        private Gimnasio gimnasio;

        public Gimnasio Gimnasio { get => gimnasio; set => gimnasio = value; }

        public GestionSocios(Gimnasio gimnasio)
        {
            if (gimnasio == null)
            {
                throw new Exception("El gestor de socios requiere una instancia de Gimnasio.");
            }
            this.Gimnasio = gimnasio;
        }

        public void RegistrarNuevoSocio(Socio objSocio)
        {
            if (!this.Gimnasio.TieneAforoDisponible())
            {
                Console.WriteLine("Error: El gimnasio ha alcanzado su capacidad máxima.");
                return;
            }

            this.Gimnasio.AgregarSocio(objSocio);
        }

        public void RenovarMembresia(string cedula)
        {
            foreach (Socio objSocio in this.Gimnasio.Socios)
            {
                if (objSocio.Cedula == cedula)
                {
                    objSocio.EstadoMembresia = true;
                    Console.WriteLine($"Membresía renovada con éxito para el socio {objSocio.Nombre}.");
                    return;
                }
            }
            Console.WriteLine("No se pudo renovar: Socio no encontrado.");
        }

        public void ConsultarSocioPorCedula(string cedula)
        {
            foreach (Socio objSocio in this.Gimnasio.Socios)
            {
                if (objSocio.Cedula == cedula)
                {
                    objSocio.Presentar();
                    return;
                }
            }
            Console.WriteLine("Socio no encontrado.");
        }
    }
}