using System.Text.Json;

namespace EspacioDatos
{
    public class AccesoADatosPedidos
    {
        private readonly string rutaPedidos = Path.Combine(Directory.GetCurrentDirectory(), "Datos", "Pedidos.json");

        public List<Pedido> Obtener()
        {
            if (!File.Exists(rutaPedidos))
                return new List<Pedido>();

            string json = File.ReadAllText(rutaPedidos);
            var lista = JsonSerializer.Deserialize<List<Pedido>>(json);
            return lista ?? new List<Pedido>();
        }

        public void Guardar(List<Pedido> pedidos)
        {
            string json = JsonSerializer.Serialize(pedidos, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(rutaPedidos, json);
        }
    }
}
