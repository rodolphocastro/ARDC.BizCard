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
        }
    }
}
