using Microsoft.AspNetCore.Mvc;
using EspacioDatos;
using System.Collections.Generic;

namespace TP5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CadeteriaController : ControllerBase
    {
        private Cadeteria cadeteria;
        private AccesoADatosCadeteria ADCadeteria;
        private AccesoADatosCadetes ADCadetes;
        private AccesoADatosPedidos ADPedidos;

        public CadeteriaController()
        {
            ADCadeteria = new AccesoADatosCadeteria();
            ADCadetes = new AccesoADatosCadetes();
            ADPedidos = new AccesoADatosPedidos();

            // Cargar datos desde los JSON
            cadeteria = ADCadeteria.Obtener();
            cadeteria.ListadoCadetes = ADCadetes.Obtener();
            cadeteria.ListadoPedidos = ADPedidos.Obtener();
        }

        // GET cadetes
        [HttpGet("cadetes")]
        public ActionResult<List<Cadete>> GetCadetes()
        {
            return Ok(cadeteria.ListadoCadetes);
        }

        // GET pedidos
        [HttpGet("pedidos")]
        public ActionResult<List<Pedido>> GetPedidos()
        {
            return Ok(cadeteria.ListadoPedidos);
        }

        // POST nuevo pedido
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

            // 🔹 Guardar SOLO pedidos
            ADPedidos.Guardar(cadeteria.ListadoPedidos);

            return Ok($"Pedido Nº {nro} agregado exitosamente");
        }

        // PUT asignar pedido
        [HttpPut("asignarPedido")]
        public ActionResult AsignarPedido(int idPedido, int idCadete)
        {
            var resultado = cadeteria.AsignarPedidoACadete(idCadete, idPedido);
            ADPedidos.Guardar(cadeteria.ListadoPedidos);
            return Ok(resultado);
        }

        // PUT cambiar estado
        [HttpPut("cambiarEstadoPedido")]
        public ActionResult CambiarEstadoPedido(int idPedido, EstadoPedido nuevoEstado)
        {
            var pedido = cadeteria.BuscarPedidoPorId(idPedido);
            if (pedido == null)
                return NotFound("Pedido no encontrado.");

            pedido.CambiarEstado(nuevoEstado);
            ADPedidos.Guardar(cadeteria.ListadoPedidos);
            return Ok($"Estado del pedido {idPedido} cambiado a {nuevoEstado}.");
        }

        // GET informe
        [HttpGet("informe")]
        public ActionResult<List<string>> GetInforme()
        {
            return Ok(cadeteria.ObtenerInforme());
        }
    }
}
