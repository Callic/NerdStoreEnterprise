namespace NSE.Core.Models.ViewModels
{
    public class ErrorViewModel
    {
        public int ErrorCode { get; set; }
        public string Titulo { get; set; }
        public string Mensagem { get; set; }
    }

    public class Errors
    {
        public List<string> Mensagens { get; set; } = new List<string>();
    }

    public class ResponseResult
    {
        public string Title { get; set; } = string.Empty;
        public int Status { get; set; }
        public Errors Errors { get; set; } = new Errors();
    }

}
