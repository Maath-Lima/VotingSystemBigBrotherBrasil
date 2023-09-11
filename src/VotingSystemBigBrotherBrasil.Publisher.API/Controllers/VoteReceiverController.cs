using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
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

        private readonly RabbitMqPublisherService _rabbitMqPublisher;

        public VoteReceiverController(
            IValidator<Vote> validator,
            RabbitMqPublisherService rabbitMqPublisher)
        {
            _validator = validator;
            _rabbitMqPublisher = rabbitMqPublisher;
        }

        [HttpPost("vote")]
        public IActionResult VoteReceiverAsync(Vote vote)
        {
            var modelValidationResult = _validator.Validate(vote);

            if (!modelValidationResult.IsValid)
            {
                var response = new BaseHttpResponse()
                {
                    Erros = modelValidationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };

                return StatusCode(StatusCodes.Status400BadRequest, response);
            }

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
