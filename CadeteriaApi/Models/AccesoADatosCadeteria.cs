using System.Text.Json;

namespace EspacioDatos
{
    public class AccesoADatosCadeteria
    {
        private readonly string rutaCadeteria = Path.Combine(Directory.GetCurrentDirectory(), "Datos", "Cadeteria.json");

        public Cadeteria Obtener()
        {
            if (!File.Exists(rutaCadeteria))
                throw new FileNotFoundException("No se encontró el archivo Cadeteria.json");

            string json = File.ReadAllText(rutaCadeteria);
            var cadeteria = JsonSerializer.Deserialize<Cadeteria>(json);
            return cadeteria ?? new Cadeteria("SinNombre", "0000");
        }
    }
}
