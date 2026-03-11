namespace Clientes.Dto
{
    public class ClientesDto
    {
        public int id { get; set; }
        public int idTipoDocumento {  get; set; }
        public long numeroDocumento { get; set; }
        public string nombreCompleto { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public string empresa {  get; set; }
        public bool? activo {  get; set; }
        public DateTime fechaCreacion {  get; set; }
    }
}
