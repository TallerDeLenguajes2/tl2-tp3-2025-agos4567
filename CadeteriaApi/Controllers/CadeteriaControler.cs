using Microsoft.AspNetCore.Mvc;
using EspacioDatos;
using System.Collections.Generic;
using System.IO;

namespace TP4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CadeteriaController : ControllerBase
    {
        private readonly string rutaCadeteria = Path.Combine(Directory.GetCurrentDirectory(), "Datos", "Cadeteria.json");
        private readonly string rutaCadetes = Path.Combine(Directory.GetCurrentDirectory(), "Datos", "Cadetes.json");
        private readonly string rutaPedidos = Path.Combine(Directory.GetCurrentDirectory(), "Datos", "Pedidos.json");

        private readonly AccesoADatosJSON acceso = new AccesoADatosJSON();
        private Cadeteria cadeteria;

        public CadeteriaController()
        {
            // Cargar datos de la cadetería
            cadeteria = acceso.CargarCadeteria(rutaCadeteria);

            // Cargar y asignar lista de cadetes
            var cadetes = acceso.CargarCadetes(rutaCadetes);
            cadeteria.ListadoCadetes = cadetes;

            // Cargar pedidos si existen
            var pedidos = acceso.CargarPedidos(rutaPedidos);
            cadeteria.ListadoPedidos = pedidos;
        }

        [HttpGet("cadetes")]
        public ActionResult<List<Cadete>> ListadoCadetes()
        {
            return Ok(cadeteria.ListadoCadetes);
        }

        [HttpGet("pedidos")]
        public ActionResult<List<Pedido>> ListadoPedidos()
        {
            return Ok(cadeteria.ListadoPedidos);
        }

        [HttpGet("informe")]
        public ActionResult<List<string>> GetInforme()
        {
            return Ok(cadeteria.ObtenerInforme());
        }

        [HttpPost("agregarPedido")]
        public IActionResult AgregarPedido([FromBody] PedidoRequest request)
        {
            if (request == null)
                return BadRequest("La solicitud no puede estar vacía.");

            int nro = cadeteria.TomarPedido(
                request.Nombre,
                request.Direccion,
                request.Telefono,
                request.ReferenciaDireccion,
                request.Observacion
            );

            // Guardar SOLO en Pedidos.json
            acceso.GuardarPedidos(rutaPedidos, cadeteria.ListadoPedidos);

            return Ok($"Pedido Nº {nro} agregado exitosamente");
        }

        [HttpPut("asignarPedido")]
        public ActionResult AsignarPedido(int idPedido, int idCadete)
        {
            var resultado = cadeteria.AsignarPedidoACadete(idCadete, idPedido);
            // Guardar solo pedidos para no tocar Cadeteria.json
            acceso.GuardarPedidos(rutaPedidos, cadeteria.ListadoPedidos);
            return Ok(resultado);
        }

        [HttpPut("cambiarCadetePedido")]
        public ActionResult CambiarCadetePedido(int idPedido, int idNuevoCadete)
        {
            var resultado = cadeteria.ReasignarPedido(idPedido, idNuevoCadete);
            acceso.GuardarPedidos(rutaPedidos, cadeteria.ListadoPedidos);
            return Ok(resultado);
        }

        [HttpPut("cambiarEstadoPedido")]
        public ActionResult CambiarEstadoPedido(int idPedido, EstadoPedido nuevoEstado)
        {
            var pedido = cadeteria.BuscarPedidoPorId(idPedido);
            if (pedido == null)
                return NotFound("Pedido no encontrado.");

            pedido.CambiarEstado(nuevoEstado);
            acceso.GuardarPedidos(rutaPedidos, cadeteria.ListadoPedidos);

            return Ok($"Estado del pedido {idPedido} cambiado a {nuevoEstado}.");
        }
    }
}
