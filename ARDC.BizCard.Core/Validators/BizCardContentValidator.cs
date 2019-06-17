using ARDC.BizCard.Core.Models;
using FluentValidation;

namespace ARDC.BizCard.Core.Validators
{
    public class BizCardContentValidator : AbstractValidator<BizCardContent>
    {
        public BizCardContentValidator()
        {
            // Nome Completo: Deve estar preenchido, entre 5 e 255 caracteres
            RuleFor(c => c.NomeCompleto).NotEmpty().MinimumLength(5).MaximumLength(255);

            // E-Mail: Caso informado, deve seguir o padrão de e-mail, máximo de 255 caracteres
            RuleFor(c => c.Email)
                .MaximumLength(255).Unless(c => string.IsNullOrWhiteSpace(c.Email))
                .EmailAddress().Unless(c => string.IsNullOrWhiteSpace(c.Email));

            // Empresa: Caso informado, deve ter no mínimo 5 e no máximo 255 caracteres
            RuleFor(c => c.Empresa)
                .MinimumLength(5).Unless(c => string.IsNullOrWhiteSpace(c.Empresa))
                .MaximumLength(255).Unless(c => string.IsNullOrWhiteSpace(c.Empresa));

            // TODO: Validar apenas caso a Empresa esteja informada?
            // Cargo: Caso informado, deve ter mínimo 5 e no máximo 255 caracteres
            RuleFor(c => c.Cargo)
                .MinimumLength(5).Unless(c => string.IsNullOrWhiteSpace(c.Cargo))
                .MaximumLength(255).Unless(c => string.IsNullOrWhiteSpace(c.Cargo));

            // TODO: Melhorar validação de telefones
            // Telefone Primário: Caso informado, deve ter um número válido
            RuleFor(c => c.TelefonePrincipal)
                .Matches(@"^([\d\ \(\)\-\+])+$").Unless(c => string.IsNullOrWhiteSpace(c.TelefonePrincipal));

            // Telefone Secundário: Caso informado, deve ter um número válido
            RuleFor(c => c.TelefoneSecundario)
                .Matches(@"^([\d\ \(\)\-\+])+$").Unless(c => string.IsNullOrWhiteSpace(c.TelefoneSecundario));

            // Website: Caso informado, deve ser um endereço válido
            RuleFor(c => c.Website)
                .Matches(@"^(https?:\/\/)?(www\.)?([a-zA-Z0-9]+(-?[a-zA-Z0-9])*\.)+[\w]{2,}(\/\S*)?$").Unless(c => string.IsNullOrWhiteSpace(c.Website));
        }
    }
}
