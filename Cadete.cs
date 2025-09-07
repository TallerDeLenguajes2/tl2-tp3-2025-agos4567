using System;
using System.Collections.Generic;


namespace EspacioDatos
{
    public class Cadete
    {
        private int id;

        private string? nombre;

        private string? direccion;

        private string telefono;

    //Quito ListadoPedidos de TP2 

    //  private List<Pedido> listadoPedidos = new List<Pedido>(); 
    //    public List<Pedido> ListadoPedidos { get => listadoPedidos; set => listadoPedidos = value; }
        public int Id { get => id; set => id = value; }
        public string? Nombre { get => nombre; set => nombre = value; }
        public string? Direccion { get => direccion; set => direccion = value; }
        public string Telefono { get => telefono; set => telefono = value; }



        //constructor


        public Cadete(int id, string nombre, string direccion, string telefono)
        {
         
            this.Id = id;
            this.Nombre = nombre;
            this.Direccion = direccion;
            this.Telefono = telefono;     
}


    

  }
  
  
  }