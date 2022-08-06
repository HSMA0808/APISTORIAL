namespace WebAPI.RequestObjects
{
    public class Request_Patient
    {
        public int Idpatient { get; set; }
        public string? MedicalCenter_Token { get; set; }
        public string? PrimerNombre { get; set; }
        public string? SegundoNombre { get; set; }
        public string? Apellidos { get; set; }
        public string? Sexo { get; set; }
        public string? Codigo_TipoIdentificacion { get; set; }
        public string? NumeroIdentificacion { get; set; }
        public string? Direccion1 { get; set; }
        public string? Direccion2 { get; set; }
        public string? Codigo_TipoSangre { get; set; }
        public string? Telefono1 { get; set; }
        public string? Telefono2 { get; set; }
        public string? Email { get; set; }
    }
}
