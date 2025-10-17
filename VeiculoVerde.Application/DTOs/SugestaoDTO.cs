namespace VeiculoVerde.Application.DTOs
{
    public class SugestaoIncentivoDTO
    {
        public string TipoIncentivo { get; set; }
        public string Descricao { get; set; }
        public string LinkMaisInformacoes { get; set; }
        public string Elegibilidade { get; set; }
        public decimal ValorEstimadoBeneficio { get; set; }
    }
}
