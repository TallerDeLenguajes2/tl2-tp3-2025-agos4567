using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using EspacioDatos;

public class AccesoADatosJSON : IAccesoADatos
{
    public Cadeteria CargarCadeteria(string nombreArchivo)
    {
        if (File.Exists(nombreArchivo))
        {
            var json = File.ReadAllText(nombreArchivo);
            return JsonSerializer.Deserialize<Cadeteria>(json);
        }
        else
        {
            return new Cadeteria("Cadetería Central", "381-5555555");
        }
    }

    public List<Cadete> CargarCadetes(string nombreArchivo)
    {
        if (File.Exists(nombreArchivo))
        {
            var json = File.ReadAllText(nombreArchivo);
            return JsonSerializer.Deserialize<List<Cadete>>(json);
        }
        return new List<Cadete>();
    }

    public List<Pedido> CargarPedidos(string nombreArchivo)
    {
        if (File.Exists(nombreArchivo))
        {
            var json = File.ReadAllText(nombreArchivo);
            return JsonSerializer.Deserialize<List<Pedido>>(json);
        }
        return new List<Pedido>();
    }

    public void GuardarCadeteria(string nombreArchivo, Cadeteria cadeteria)
    {
        var opciones = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(cadeteria, opciones);
        File.WriteAllText(nombreArchivo, json);
    }

    public void GuardarPedidos(string nombreArchivo, List<Pedido> pedidos)
    {
        var opciones = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(pedidos, opciones);
        File.WriteAllText(nombreArchivo, json);
    }
}
