using System;
using System.Security.Cryptography;
using System.Text;

namespace ARDC.BizCard.Core.Models
{
    public class BizCardContent
    {
        public string NomeCompleto { get; set; }

        public string Email { get; set; }

        public string TelefonePrincipal { get; set; }

        public string TelefoneSecundario { get; set; }

        public string Whatsapp { get; set; }

        public string LinkedIn { get; set; }

        public string Website { get; set; }

        public string Empresa { get; set; }

        public string Cargo { get; set; }

        public string Endereco { get; set; }

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
