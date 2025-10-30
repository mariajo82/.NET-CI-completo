namespace FullStackCI.Dtos
{
    public class RespuestaHaciendaDto
    {
        public string Nombre { get; set; }
        public string TipoIdentificacion { get; set; }
        public RegimenDto Regimen { get; set; }
        public SituacionDto Situacion { get; set; }
        public List<ActividadesDto> Actividades { get; set; }

    }
}

