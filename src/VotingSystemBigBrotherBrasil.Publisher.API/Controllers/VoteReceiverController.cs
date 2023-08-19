using FluentValidation;
using Microsoft.AspNetCore.Mvc;
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

        public VoteReceiverController(
            IValidator<Vote> validator, 
            RabbitMqPublisher rabbitMqPublisher)
        {
            _validator = validator;
            _rabbitMqPublisher = rabbitMqPublisher;
        }

        [HttpPost("vote")]
        public async Task<IActionResult> VoteReceiverAsync(Vote vote)
        {
            var modelValidationResult = await _validator.ValidateAsync(vote);

            if (!modelValidationResult.IsValid)
            {
                var response = new BaseHttpResponse()
                {
                    Status = StatusCodes.Status400BadRequest,
                    Erros = modelValidationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };

                return StatusCode(response.Status, response);
            }

            _rabbitMqPublisher.PublishVote(vote.Name);

            return StatusCode(
                StatusCodes.Status200OK,
                new BaseHttpResponse<string>()
                {
                    Status = StatusCodes.Status200OK,
                    Data = "Voto computado com sucesso!!"
                });
        }
    }
}
