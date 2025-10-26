using System.Text.Json;

namespace EspacioDatos
{
    public class AccesoADatosCadetes
    {
        private readonly string rutaCadetes = Path.Combine(Directory.GetCurrentDirectory(), "Datos", "Cadetes.json");

        public List<Cadete> Obtener()
        {
            if (!File.Exists(rutaCadetes))
                return new List<Cadete>();

            string json = File.ReadAllText(rutaCadetes);
            var lista = JsonSerializer.Deserialize<List<Cadete>>(json);
            return lista ?? new List<Cadete>();
        }
    }
}
