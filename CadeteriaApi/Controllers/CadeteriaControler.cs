using Microsoft.AspNetCore.Mvc;
using EspacioDatos;
using System.Collections.Generic;

namespace TP4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CadeteriaController : ControllerBase
    {
        // Simulación de la base de datos
        private static Cadeteria cadeteria = new Cadeteria("Cadetería Central", "381-5555555");

        // Constructor estático para precargar datos de ejemplo
        static CadeteriaController()
        {
            cadeteria.AgregarCadete(new Cadete(1, "Carlos Pérez", "Calle Falsa 123", "381123456"));
            cadeteria.AgregarCadete(new Cadete(2, "María López", "San Martín 789", "381654321"));

            var cliente1 = new Clientes("Juan Gómez", "Rivadavia 100", 381555000, "Depto A");
            cadeteria.AgregarPedido(new Pedido(1, "Pizza grande", cliente1, EstadoPedido.Pendiente));
        }

        //  [GET] /api/cadeteria/pedidos
        [HttpGet("pedidos")]
        public ActionResult<List<Pedido>> GetPedidos()
        {
            return Ok(cadeteria.ListadoPedidos);
        }

        //  [GET] /api/cadeteria/cadetes
        [HttpGet("cadetes")]
        public ActionResult<List<Cadete>> GetCadetes()
        {
            return Ok(cadeteria.ListadoCadetes);
        }

        // [GET] /api/cadeteria/informe
        [HttpGet("informe")]
        public ActionResult<List<string>> GetInforme()
        {
            var informe = cadeteria.ObtenerInforme();
            return Ok(informe);
        }

        //  [POST] /api/cadeteria/agregarPedido
        [HttpPost("agregarPedido")]
        public ActionResult AgregarPedido([FromBody] Pedido pedido)
        {
            if (pedido == null)
                return BadRequest("El pedido no puede ser nulo.");

            var resultado = cadeteria.AgregarPedido(pedido);
            return Ok(resultado);
        }

        //  [PUT] /api/cadeteria/asignarPedido?idPedido=1&idCadete=2
        [HttpPut("asignarPedido")]
        public ActionResult AsignarPedido(int idPedido, int idCadete)
        {
            var resultado = cadeteria.AsignarPedidoACadete(idCadete, idPedido);
            return Ok(resultado);
        }

        //  [PUT] /api/cadeteria/cambiarCadetePedido?idPedido=1&idNuevoCadete=2
        [HttpPut("cambiarCadetePedido")]
        public ActionResult CambiarCadetePedido(int idPedido, int idNuevoCadete)
        {
            var resultado = cadeteria.ReasignarPedido(idPedido, idNuevoCadete);
            return Ok(resultado);
        }

        // [PUT] /api/cadeteria/cambiarEstadoPedido?idPedido=1&nuevoEstado=Entregado
        [HttpPut("cambiarEstadoPedido")]
        public ActionResult CambiarEstadoPedido(int idPedido, EstadoPedido nuevoEstado)
        {
            var pedido = cadeteria.BuscarPedidoPorId(idPedido);
            if (pedido == null)
                return NotFound("Pedido no encontrado.");

            pedido.CambiarEstado(nuevoEstado);
            return Ok($"Estado del pedido {idPedido} cambiado a {nuevoEstado}.");
        }
    }
}