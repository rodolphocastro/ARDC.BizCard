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
            return !(
                string.IsNullOrWhiteSpace(NomeCompleto) ||
                string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(TelefonePrincipal) ||
                string.IsNullOrWhiteSpace(TelefoneSecundario) ||
                string.IsNullOrWhiteSpace(Whatsapp) ||
                string.IsNullOrWhiteSpace(LinkedIn) ||
                string.IsNullOrWhiteSpace(Website) ||
                string.IsNullOrWhiteSpace(Empresa) ||
                string.IsNullOrWhiteSpace(Cargo) ||
                string.IsNullOrWhiteSpace(Endereco)
            );
        }
    }
}
