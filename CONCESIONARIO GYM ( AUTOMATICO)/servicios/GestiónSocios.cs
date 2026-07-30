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

        public void ConsultarSocioPorCedula(string cedula, List<ClaseGrupal> listaClases = null)
        {
            foreach (Socio objSocio in this.Gimnasio.Socios)
            {
                if (objSocio.Cedula == cedula)
                {
                    // 1. Muestra los datos básicos del socio
                    Console.WriteLine("\n==================================================");
                    Console.WriteLine("                FICHA DEL SOCIO");
                    Console.WriteLine("==================================================");
                    objSocio.Presentar();

                    // 2. Si se pasó la lista de clases, busca sus reservas
                    if (listaClases != null)
                    {
                        Console.WriteLine("\n--- CLASES GRUPALES RESERVADAS ---");

                        // Buscamos las clases donde esté anotado el socio (por cédula o por ID)
                        var clasesReservadas = listaClases.Where(c => c.SociosInscritos != null &&
                                                                      c.SociosInscritos.Any(s => s.Cedula == cedula)).ToList();

                        if (clasesReservadas.Count == 0)
                        {
                            Console.WriteLine(" (El socio no tiene clases reservadas)");
                        }
                        else
                        {
                            foreach (var clase in clasesReservadas)
                            {
                                Console.WriteLine($" • [ID: {clase.Id}] {clase.NombreDisciplina} - Horario: {clase.Horario}");
                            }
                        }
                    }

                    Console.WriteLine("==================================================\n");
                    return;
                }
            }

            Console.WriteLine("Socio no encontrado.");
        }
    }
}
        