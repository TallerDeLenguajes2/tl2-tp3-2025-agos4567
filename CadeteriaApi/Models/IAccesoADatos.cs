namespace EspacioDatos
{
    public interface IAccesoADatos
    {
        Cadeteria CargarCadeteria(string archivo);
        List<Cadete> CargarCadetes(string archivo);
        void GuardarCadeteria(string archivo, Cadeteria cadeteria);
    }
}
