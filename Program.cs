using EspacioDatos;
using System;
using System.Collections.Generic;

namespace CadeteriaApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1 se elige el tipo de archivo
            Console.WriteLine("Seleccione fuente de datos: 1) CSV 2) JSON");
            string? opcion = Console.ReadLine();

            IAccesoADatos acceso;
            if (opcion == "2")
                acceso = new AccesoADatosJSON();
            else
                acceso = new AccesoADatosCSV();

            // 2 cargo datos usando la interfaz
            List<Cadete> cadetes = acceso.CargarCadetes(opcion == "2" ? "Cadete.json" : "Cadete.csv");
            Cadeteria cadeteria = acceso.CargarCadeteria(opcion == "2" ? "Cadeteria.json" : "Cadeteria.csv");

            // 3 asigno los cadetes a la cadeteria
            foreach (var cadete in cadetes)
            {
                cadeteria.AgregarCadete(cadete);
            }

           
 
            // 4 muestro menu de gestion de pedidos
            MostrarMenu(cadeteria);
        }

        // --- MENU ---
        static void MostrarMenu(Cadeteria cadeteria)
        {
            int opcion = 0;
            while (opcion != 6)
            {
                Console.WriteLine("\n--- MENÚ DE OPCIONES ---");
                Console.WriteLine("1. Dar de alta un pedido");
                Console.WriteLine("2. Asignar pedido a cadete");
                Console.WriteLine("3. Reasignar pedido");
                Console.WriteLine("4. Mostrar informe de cadetes");
                Console.WriteLine("5. Cambiar estado pedido");
                Console.WriteLine("6. Salir");
                Console.Write("Elija una opción: ");
                int.TryParse(Console.ReadLine(), out opcion);

                switch (opcion)
                {
                    case 1: DarDeAltaPedido(cadeteria); break;
                    case 2: AsignarPedidoACadete(cadeteria); break;
                    case 3: ReasignarPedido(cadeteria); break;
                    case 4: MostrarInforme(cadeteria); break;
                    case 5: CambiarEstadoPedido(cadeteria); break;
                    case 6: Console.WriteLine("Saliendo..."); break;
                    default: Console.WriteLine("Opción no válida"); break;
                }
            }
        }

        // --- OTROS metodos ---
        static void DarDeAltaPedido(Cadeteria cadeteria)
        {
            Console.Write("Ingrese número de pedido: ");
            int.TryParse(Console.ReadLine(), out int nroPedido);

            Console.Write("Observación: "); string? obs = Console.ReadLine();
            Console.Write("Nombre cliente: "); string? nombre = Console.ReadLine();
            Console.Write("Dirección: "); string? direccion = Console.ReadLine();
            Console.Write("Teléfono: "); int.TryParse(Console.ReadLine(), out int telefono);
            Console.Write("Datos de referencia: "); string? refDatos = Console.ReadLine();

            Clientes cliente = new Clientes(nombre, direccion, telefono, refDatos);
            Pedido pedido = new Pedido(nroPedido, obs, cliente, EstadoPedido.Pendiente);

            cadeteria.ListadoPedidos.Add(pedido);

            Console.WriteLine("Pedido dado de alta.");
        }
        static void AsignarPedidoACadete(Cadeteria cadeteria)
        {
            Console.Write("ID cadete: "); int.TryParse(Console.ReadLine(), out int cadId);
            Console.Write("ID pedido: "); int.TryParse(Console.ReadLine(), out int pedId);

            string asignado = cadeteria.AsignarPedidoACadete(cadId, pedId);

   
              Console.WriteLine(asignado);
        }
        static void ReasignarPedido(Cadeteria cadeteria)
        {
            Console.Write("ID pedido: "); int.TryParse(Console.ReadLine(), out int pedId);
            Console.Write("Nuevo ID cadete: "); int.TryParse(Console.ReadLine(), out int cadId);

            string reasignado = cadeteria.ReasignarPedido(pedId, cadId);

    
            Console.WriteLine(reasignado);
        }

        static void CambiarEstadoPedido(Cadeteria cadeteria)
        {
            Console.Write("Número de pedido: "); int.TryParse(Console.ReadLine(), out int pedId);
            var pedido = cadeteria.BuscarPedidoPorId(pedId);

            if (pedido == null)
            {
                Console.WriteLine(" Pedido no encontrado.");
                return;
            }

            Console.WriteLine("Seleccione nuevo estado: ");
            Console.WriteLine("1) Pendiente  2) EnProceso  3) Entregado  4) Cancelado");
            int.TryParse(Console.ReadLine(), out int estado);

            pedido.CambiarEstado((EstadoPedido)(estado - 1));
            Console.WriteLine(" Estado cambiado.");
        }
        
          static void MostrarInforme(Cadeteria cadeteria)
        {
            var informe = cadeteria.ObtenerInforme();

            foreach (var linea in informe)
            {
                Console.WriteLine(linea);
            }
        }



    }
}
