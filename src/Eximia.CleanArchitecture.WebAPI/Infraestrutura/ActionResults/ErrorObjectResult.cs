namespace Eximia.CleanArchitecture.WebAPI.Infraestrutura.ActionResults
{
    public class ErrorObjectResult
    {
        public ErrorObjectResult(string erro, string[] motivos)
        {
            Erro = erro;
            Motivos = motivos;
        }
        public string Erro { get; set; }
        public string[] Motivos { get; set; }

        public static ErrorObjectResult Criar(string erro, string motivos)
            => new ErrorObjectResult(erro, motivos.Split(','));
    }
}