using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Linq;
using System.Threading.Tasks;
using VotingSystemBigBrotherBrasil.Publisher.Models.Responses;
using VotingSystemBigBrotherBrasil.Publisher.Models.VoteModel;
using VotingSystemBigBrotherBrasil.Publisher.Services;

namespace VotingSystemBigBrotherBrasil.Publisher.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VoteReceiverController : ControllerBase
    {
        private readonly IValidator<Vote> _validator;

        private readonly RabbitMqPublisher _rabbitMqPublisher;
        private readonly ILogger _logger;

        public VoteReceiverController(
            IValidator<Vote> validator,
            RabbitMqPublisher rabbitMqPublisher,
            ILogger logger)
        {
            _validator = validator;
            _rabbitMqPublisher = rabbitMqPublisher;
            _logger = logger;
        }

        [HttpPost("vote")]
        public async Task<IActionResult> VoteReceiverAsync(Vote vote)
        {
            var modelValidationResult = await _validator.ValidateAsync(vote);

            if (!modelValidationResult.IsValid)
            {
                var response = new BaseHttpResponse()
                {
                    Erros = modelValidationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };

                return StatusCode(StatusCodes.Status400BadRequest, response);
            }

            _logger.Information("Teste log");

            _rabbitMqPublisher.PublishVote(vote.Name);

            return StatusCode(
                StatusCodes.Status200OK,
                new BaseHttpResponse<string>()
                {
                    Data = "Voto computado com sucesso!!"
                });
        }
    }
}
