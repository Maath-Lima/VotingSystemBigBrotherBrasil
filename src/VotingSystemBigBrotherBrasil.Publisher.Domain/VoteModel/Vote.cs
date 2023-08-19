using Newtonsoft.Json;

namespace VotingSystemBigBrotherBrasil.Publisher.Models.VoteModel
{
    public class Vote
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
