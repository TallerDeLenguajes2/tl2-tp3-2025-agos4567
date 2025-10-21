namespace EspacioDatos
{
     public enum EstadoPedido
    {
        Pendiente,
        EnProceso,
        Entregado,
        Cancelado
    }
    public class Pedido
    {
        private int nro;
        private string? observacion;

         private Clientes cliente;

          private EstadoPedido estado;
          

             // Agrego una referencia de cadete en Pedido.cs  (TP2)
        public Cadete? CadeteAsignado { get; set; }

        public int Nro { get => nro; set => nro = value; }
        public string? Observacion { get => observacion; set => observacion = value; }
        public Clientes Cliente { get => cliente; set => cliente = value; }
        public EstadoPedido Estado { get => estado; set => estado = value; }


            public Pedido(int nro, string observacion, Clientes cliente, EstadoPedido estado)
        {
            this.Nro = nro;
            this.Observacion = observacion;
            this.Cliente = cliente;
            this.Estado = estado;
            this.CadeteAsignado = null; // al inicio no tiene cadete
        }
        
  
                    public EstadoPedido CambiarEstado(EstadoPedido nuevoEstado)
            {
                Estado = nuevoEstado;
                return Estado; // devuelve el nuevo estado
            }

    }
}