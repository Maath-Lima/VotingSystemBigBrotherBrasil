namespace VotingSystemBigBrotherBrasil.Publisher.Models.Responses
{
    public class BaseHttpResponse<T>
    {
        public Guid TraceId { get; set; }

        public int Status { get; set; }

        public T Data { get; set; }

        public IEnumerable<string> Erros { get; set; }

        public BaseHttpResponse()
        {
            TraceId = Guid.NewGuid();
            Erros = new List<string>();
        }
    }

    public class BaseHttpResponse
    {
        public Guid TraceId { get; set; }

        public int Status { get; set; }

        public object Data { get; set; }

        public IEnumerable<string> Erros { get; set; }

        public BaseHttpResponse()
        {
            TraceId = Guid.NewGuid();
            Erros = new List<string>();
        }
    }
}
