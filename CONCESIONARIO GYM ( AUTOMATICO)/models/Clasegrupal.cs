using System;
using System.Collections.Generic;
using System.Text;

namespace CONCESIONARIO_GYM___AUTOMATICO_.models
{
public class ClaseGrupal
{
    private int id;
    private string nombreDisciplina;
    private string horario;
    private int cuposDisponibles;

    public int Id { get => id; set => id = value; }
    public string NombreDisciplina
    {
        get => nombreDisciplina;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new Exception("El nombre de la disciplina no puede estar vacío.");
            }
            nombreDisciplina = value;
        }
    }
    public string Horario { get => horario; set => horario = value; }
    public int CuposDisponibles
    {
        get => cuposDisponibles;
        set
        {
            if (value < 0 || value > 50)
            {
                throw new Exception("Los cupos disponibles deben estar entre 0 y 50.");
            }
            cuposDisponibles = value;
        }
    }

    public ClaseGrupal(int id, string nombreDisciplina, string horario, int cuposDisponibles)
    {
        this.Id = id;
        this.NombreDisciplina = nombreDisciplina;
        this.Horario = horario;
        this.CuposDisponibles = cuposDisponibles;
    }

    public bool ReservarCupo()
    {
        if (this.CuposDisponibles > 0)
        {
            this.CuposDisponibles--;
            Console.WriteLine($"Cupo reservado con éxito en {this.NombreDisciplina}.");
            return true;
        }
        else
        {
            Console.WriteLine($"No hay cupos disponibles para {this.NombreDisciplina}.");
            return false;
        }
    }

    public void MostrarDetalle()
    {
        Console.WriteLine($"[ID: {this.Id}] {this.NombreDisciplina} - Horario: {this.Horario} | Cupos: {this.CuposDisponibles}");
    }
}
}