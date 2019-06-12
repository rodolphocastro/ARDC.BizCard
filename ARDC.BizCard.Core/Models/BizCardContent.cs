using System.Security.Cryptography;
using System.Text;

namespace ARDC.BizCard.Core.Models
{
    /// <summary>
    /// Model para o Cartao de Visitas do App.
    /// </summary>
    public class BizCardContent
    {
        /// <summary>
        /// Nome completo da pessoa.
        /// </summary>
        public string NomeCompleto { get; set; }

        /// <summary>
        /// E-Mail para contato.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Telefone primário.
        /// </summary>
        public string TelefonePrincipal { get; set; }

        /// <summary>
        /// Telefone secundário.
        /// </summary>
        public string TelefoneSecundario { get; set; }

        /// <summary>
        /// Whatsapp para contato.
        /// </summary>
        public string Whatsapp { get; set; }

        /// <summary>
        /// Perfil do LinkedIn.
        /// </summary>
        public string LinkedIn { get; set; }

        /// <summary>
        /// Website para contato.
        /// </summary>
        public string Website { get; set; }

        /// <summary>
        /// Empresa em que trabalha.
        /// </summary>
        public string Empresa { get; set; }

        /// <summary>
        /// Cargo que ocupa.
        /// </summary>
        public string Cargo { get; set; }

        /// <summary>
        /// Endereço comercial.
        /// </summary>
        public string Endereco { get; set; }

        /// <summary>
        /// Indica se o cartão possui algum dado preenchido.
        /// </summary>
        /// <returns>TRUE caso o cartão possua algum dado preenchido, FALSE caso contrário</returns>
        public bool HasData()
        {
            return (
                !string.IsNullOrWhiteSpace(NomeCompleto) ||
                !string.IsNullOrWhiteSpace(Email) ||
                !string.IsNullOrWhiteSpace(TelefonePrincipal) ||
                !string.IsNullOrWhiteSpace(TelefoneSecundario) ||
                !string.IsNullOrWhiteSpace(Whatsapp) ||
                !string.IsNullOrWhiteSpace(LinkedIn) ||
                !string.IsNullOrWhiteSpace(Website) ||
                !string.IsNullOrWhiteSpace(Empresa) ||
                !string.IsNullOrWhiteSpace(Cargo) ||
                !string.IsNullOrWhiteSpace(Endereco)
            );
        }

        /// <summary>
        /// Obtem a URL do Gravatar do cartão.
        /// </summary>
        /// <returns>Uma URL para o Gravatar</returns>
        /// <remarks>Caso o Cartão não possua e-mail, será retornada uma URL padrão do Gravatar</remarks>
        public string ToGravatarURI()
        {
            string baseAddr = @"https://www.gravatar.com/avatar/";
            StringBuilder sb = new StringBuilder(baseAddr);

            if (string.IsNullOrWhiteSpace(Email))
            {
                sb.Append(@".jpg?s=250&d=identicon");
                return sb.ToString();
            }

            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(Email));

            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString("x2"));
            }

            sb.Append(@".jpg?s=250&d=identicon");

            return sb.ToString();
        }
    }
}
