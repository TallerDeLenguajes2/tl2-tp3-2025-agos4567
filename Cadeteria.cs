
namespace EspacioDatos
{
    public class Cadeteria
    {
        private string? nombre;

        private string telefono;
        //    private List<Cadete> listadoCadetes;
     private List<Cadete> listadoCadetes = new List<Cadete>();
     
        // en TP2 ahora es listado pedios, ya no se llama pedidos disponibles
        private List<Pedido> listadoPedidos = new List<Pedido>();
        

        public string? Nombre { get => nombre; set => nombre = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        // public List<Cadete> ListadoCadetes { get => listadoCadetes; set => listadoCadetes = value; }
         public List<Cadete> ListadoCadetes { get => listadoCadetes; set => listadoCadetes = value; }
         //en tp2 ahora se llama listado pedidos
        public List<Pedido> ListadoPedidos { get => listadoPedidos; set => listadoPedidos = value; }

        // public List<Pedido> PedidosDisponibles { get => pedidosDisponibles; set => pedidosDisponibles = value; }


        public Cadeteria(string nombre, string telefono)
        {
            this.Nombre = nombre;
            this.Telefono = telefono;
    }

    
        //   agrego un pedido al listado tp2
        public string AgregarPedido(Pedido pedido)
        {
            if (pedido == null)
                return "El pedido no puede ser nulo.";

            listadoPedidos.Add(pedido);
            return $"Pedido {pedido.Nro} agregado correctamente.";
        }


        public string AgregarCadete(Cadete nuevoCadete)
        {
            if (nuevoCadete == null)
                return "El cadete no puede ser nulo.";

            listadoCadetes.Add(nuevoCadete);
            return $"Cadete {nuevoCadete.Nombre} agregado correctamente.";
        }

        //   // metodo para buscar un pedido por su id
        // public Pedido? BuscarPedidoPorId(int pedidoId)
        // {
        //     foreach (var pedido in listadoPedidos)
        //     {
        //         if (pedido.Nro == pedidoId)
        //         {
        //             return pedido;
        //         }
        //     }
        //     return null; // Retorna null si no se encuentra el pedido
        // }



           public Pedido? BuscarPedidoPorId(int pedidoId)
        {
            return listadoPedidos.FirstOrDefault(p => p.Nro == pedidoId);
        }




        // Método para asignar un pedido a un cadete tp2
 public string AsignarPedidoACadete(int cadeteId, int pedidoId)
        {
            Pedido? pedido = listadoPedidos.FirstOrDefault(p => p.Nro == pedidoId);
            if (pedido == null)
                return $"El pedido con ID {pedidoId} no ha sido dado de alta.";

            Cadete? cadete = listadoCadetes.FirstOrDefault(c => c.Id == cadeteId);
            if (cadete == null)
                return $"Cadete con ID {cadeteId} no encontrado.";

            pedido.CadeteAsignado = cadete;
            return $"El pedido {pedidoId} ha sido asignado al cadete {cadeteId}.";
        }

       public string ReasignarPedido(int pedidoId, int nuevoCadeteId)
        {
            Pedido? pedido = BuscarPedidoPorId(pedidoId);
            if (pedido == null)
                return $"El pedido con ID {pedidoId} no ha sido dado de alta.";

            if (pedido.CadeteAsignado == null)
                return $"El pedido {pedidoId} no está asignado a ningún cadete.";

            Cadete? nuevoCadete = listadoCadetes.FirstOrDefault(c => c.Id == nuevoCadeteId);
            if (nuevoCadete == null)
                return $"El nuevo cadete con ID {nuevoCadeteId} no existe.";

            pedido.CadeteAsignado = nuevoCadete;
            return $"El pedido {pedidoId} ha sido reasignado al cadete {nuevoCadeteId}.";
        }


/// ////////////////////////// ////////////////////////// ////////////////////////// ////////////////////////// ////////////////////////// ////////////////////////// ///////////////////////


     public const double PrecioPorViaje = 50;

        // Contar pedidos entregados de un cadete
        public int PedidosRealizados(Cadete cadete)
        {
            return listadoPedidos.Count(p => p.CadeteAsignado == cadete && p.Estado == EstadoPedido.Entregado);
        }

        // Calcular jornal a cobrar
        public double JornalACobrar(Cadete cadete)
        {
            int realizados = PedidosRealizados(cadete);
            return realizados * PrecioPorViaje;
        }

        // Obtener informe de todos los cadetes
        public List<string> ObtenerInforme()
        {
            var informe = new List<string>();
            if (!ListadoCadetes.Any()) return informe;

            double totalJornal = 0;
            int totalEnvios = 0;

            foreach (var cadete in ListadoCadetes)
            {
                int realizados = PedidosRealizados(cadete);
                double jornal = JornalACobrar(cadete);

                totalEnvios += realizados;
                totalJornal += jornal;

                informe.Add($"Cadete: {cadete.Nombre}, Pedidos Entregados: {realizados}, Jornal: {jornal}");
            }

            double promedioEnvios = ListadoCadetes.Count > 0 ? (double)totalEnvios / ListadoCadetes.Count : 0;
            informe.Add($"Total Jornal: {totalJornal}, Total Envíos: {totalEnvios}, Promedio Envíos por Cadete: {promedioEnvios:F2}");

            return informe;
        }











        

            
    }
}