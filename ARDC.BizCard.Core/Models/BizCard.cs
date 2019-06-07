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

        public Uri ToGravatarURI()
        {
            string baseAddr = @"https://www.gravatar.com/avatar/";
            StringBuilder sb = new StringBuilder(baseAddr);
            using (MD5 md5 = MD5.Create())
            {
                byte[] retVal = md5.ComputeHash(Encoding.Unicode.GetBytes("TextToHash"));
                for (int i = 0; i < retVal.Length; i++)
                    sb.Append(retVal[i].ToString("x2"));
            }

            sb.Append(".jpg?s=150");

            return new Uri(sb.ToString());
        }
    }
}
