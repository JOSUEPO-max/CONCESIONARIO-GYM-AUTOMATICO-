using CONCESIONARIO_GYM___AUTOMATICO_.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CONCESIONARIO_GYM___AUTOMATICO_.servicios
{
    public class GestionClases
    {
        private List<ClaseGrupal> listaClases;

        public List<ClaseGrupal> ListaClases { get => listaClases; set => listaClases = value; }

        public GestionClases()
        {
            this.ListaClases = new List<ClaseGrupal>();
        }

        public void AgregarClase(ClaseGrupal objClase)
        {
            if (objClase == null)
            {
                throw new Exception("No se puede agregar una clase nula.");
            }
            this.ListaClases.Add(objClase);
            Console.WriteLine($"Clase {objClase.NombreDisciplina} registrada exitosamente.");
        }

        public void MostrarClasesDisponibles()
        {
            Console.WriteLine("--- CLASES GRUPALES DISPONIBLES ---");
            foreach (ClaseGrupal objClase in this.ListaClases)
            {
                objClase.MostrarDetalle();
            }
        }

        public bool AgendarCupoEnClase(int idClase, Socio socio)
        {
            foreach (ClaseGrupal clase in this.ListaClases)
            {
                if (clase.Id == idClase)
                {
                    return clase.ReservarCupo(socio);
                }
            }
            Console.WriteLine("Clase no encontrada.");
            return false;
        }
    }
}
