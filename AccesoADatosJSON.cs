using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using EspacioDatos;
public class AccesoADatosJSON : IAccesoADatos
{
    public Cadeteria CargarCadeteria(string nombreArchivo)
    {
        Cadeteria cadeteria = null;

        if (File.Exists(nombreArchivo))
        {
            var json = File.ReadAllText(nombreArchivo);
            cadeteria = JsonSerializer.Deserialize<Cadeteria>(json);
        }

        return cadeteria;
    }

    public List<Cadete> CargarCadetes(string nombreArchivo)
    {
        List<Cadete> cadetes = new List<Cadete>();

        if (File.Exists(nombreArchivo))
        {
            var json = File.ReadAllText(nombreArchivo);
            cadetes = JsonSerializer.Deserialize<List<Cadete>>(json);
        }

        return cadetes;
    }
}
