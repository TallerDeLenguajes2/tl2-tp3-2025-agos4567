using System;
using System.Collections.Generic;

namespace EspacioDatos
{
    public class Clientes
    {
        private string? nombre;

        private string? direccion;

        private int telefono;

        private string? datosReferenciaDireccion;

        public string? Nombre { get => nombre; set => nombre = value; }
        public string? Direccion { get => direccion; set => direccion = value; }
        public int Telefono { get => telefono; set => telefono = value; }
        public string? DatosReferenciaDireccion { get => datosReferenciaDireccion; set => datosReferenciaDireccion = value; }



        public Clientes(string nombre, string direccion, int telefono, string datosReferenciaDireccion)
            {
                this.Nombre = nombre;
                this.Direccion = direccion;
                this.Telefono = telefono;
                this.DatosReferenciaDireccion = datosReferenciaDireccion;
            }
   
                
    }
}
