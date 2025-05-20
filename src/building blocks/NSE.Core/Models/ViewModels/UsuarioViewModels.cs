using System.ComponentModel.DataAnnotations;

namespace NSE.Core.Models.ViewModels
{
    public class UsuarioRegistro
    {

        [Required(ErrorMessage = "Digite seu nome")]
        public  string Nome { get; set; }

        [Required(ErrorMessage = "Digite seu CPF")]
        public string Cpf { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "O campo email está em um formato inválido")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Senha { get; set; }

        [Compare("Senha", ErrorMessage = "As senhas não conferem")]
        public string SenhaConfirmacao { get; set; }
    }


    public class UsuarioLogin
    {
        [Required]
        [EmailAddress(ErrorMessage = "O campo email está em um formato inválido")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Senha { get; set; }
    }


    public class UsuarioRespostaLogin
    {
        public UsuarioRespostaLogin()
        {
            UsuarioToken = new UsuarioToken();
            AcessToken = string.Empty;
        }
        public string AcessToken { get; set; }
        public double ExpireIn { get; set; }
        public UsuarioToken UsuarioToken{ get; set; }
    }
    public class UsuarioToken
    {
        public UsuarioToken()
        {
            Id = string.Empty;
            Email = string.Empty;
            Claims = new List<UsuarioClaim>();
        }
        public string Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<UsuarioClaim> Claims { get; set; }

    }

    public class UsuarioClaim
    {
        public UsuarioClaim()
        {
            Value = string.Empty;
            Type = string.Empty;
        }
        public string Value { get; set; }
        public string Type { get; set; }
    }





}
