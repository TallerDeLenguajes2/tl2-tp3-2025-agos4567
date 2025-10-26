using System;
using System.Collections.Generic;
using System.Linq;

namespace EspacioDatos
{
    public class Cadeteria
    {
  
        private string? nombre;
        private string telefono;
        private List<Cadete> listadoCadetes = new List<Cadete>();
        private List<Pedido> listadoPedidos = new List<Pedido>();

        public string? Nombre { get => nombre; set => nombre = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public List<Cadete> ListadoCadetes { get => listadoCadetes; set => listadoCadetes = value; }
        public List<Pedido> ListadoPedidos { get => listadoPedidos; set => listadoPedidos = value; }

        public Cadeteria(string nombre, string telefono)
        {
            this.Nombre = nombre;
            this.Telefono = telefono;
        }

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

        public Pedido? BuscarPedidoPorId(int pedidoId)
        {
            return listadoPedidos.FirstOrDefault(p => p.Nro == pedidoId);
        }

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

        // nuevo
        private int ultimoNroPedido = 0;

        public int TomarPedido(string nombreCliente, string direccion, string telefono, string referenciaDireccion, string observacion)
        {
            // 1 crear cliente
            Clientes cliente = new Clientes(nombreCliente, direccion, telefono, referenciaDireccion);

            // 2. calcular nro de pedido
            int nroPedido = listadoPedidos.Count + 1;

            // 3 crear pedido usando el constructor correcto
            Pedido pedido = new Pedido(
                nroPedido,
                direccion,    // o direccion del cliente
                cliente,
                EstadoPedido.Pendiente
            );

            // 4 asignar obs si la propiedad existe
            pedido.Observacion = observacion;

            // 5 aregar a la lista de pedidos
            listadoPedidos.Add(pedido);

            // devolver nro de pedido para la respuesta
            return nroPedido;
        }

        // ===================================================

        // metodo de informe y jornal (de antes)
        public const double PrecioPorViaje = 50;


        public double JornalACobrar(Cadete cadete)
        {
            int realizados = PedidosRealizados(cadete);
            return realizados * PrecioPorViaje;
        }
                    public int PedidosRealizados(Cadete cadete)
            {
                int contador = 0;
                foreach (var pedido in listadoPedidos)
                {
                    if (pedido.CadeteAsignado != null &&
                        pedido.CadeteAsignado.Id == cadete.Id &&
                        pedido.Estado == EstadoPedido.Entregado)
                    {
                        contador++;
                    }
                }
                return contador;
            }


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
