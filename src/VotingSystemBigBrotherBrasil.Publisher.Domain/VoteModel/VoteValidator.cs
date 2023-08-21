using FluentValidation;
using Microsoft.Extensions.Options;
using System.Linq;
using VotingSystemBigBrotherBrasil.Publisher.Models.Settings;

namespace VotingSystemBigBrotherBrasil.Publisher.Models.VoteModel
{
    public class VoteValidator : AbstractValidator<Vote>
    {
        private const string NAME_MESSAGE = "É necessário o nome do participante emparedado!";
        private const string PAREDAO_MESSAGE = "Participante não emparedado!";

        public VoteValidator(IOptions<ParedaoSettings> options)
        {
            var emparedados = options.Value.Emparedados;

            RuleFor(v => v.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(NAME_MESSAGE)
                .Must(name => emparedados.Contains(name)).WithMessage(PAREDAO_MESSAGE);
        }
    }
}
