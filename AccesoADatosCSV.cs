using System;
using System.Collections.Generic;
using System.IO;
using EspacioDatos;
public class AccesoADatosCSV : IAccesoADatos
{
    public Cadeteria CargarCadeteria(string nombreArchivo)
    {
        Cadeteria cadeteria = null;

        if (File.Exists(nombreArchivo))
        {
            var lineas = File.ReadAllLines(nombreArchivo);
            // formato: Nombre;Telefono
            if (lineas.Length > 0)
            {
                var datos = lineas[0].Split(',');
                cadeteria = new Cadeteria(datos[0], datos[1]);
            }
        }

        return cadeteria;
    }

    public List<Cadete> CargarCadetes(string nombreArchivo)
    {
        List<Cadete> cadetes = new List<Cadete>();

        if (File.Exists(nombreArchivo))
        {
            var lineas = File.ReadAllLines(nombreArchivo);
            foreach (var linea in lineas)
            {
                // formato: Id;Nombre;Direccion;Telefono
                var datos = linea.Split(',');
                var cadete = new Cadete(
                    int.Parse(datos[0]),
                    datos[1],
                    datos[2],
                    datos[3]
                );
                cadetes.Add(cadete);
            }
        }

        return cadetes;
    }
}
