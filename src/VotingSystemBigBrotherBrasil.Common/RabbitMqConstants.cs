namespace VotingSystemBigBrotherBrasil.Common
{
    public static class RabbitMqConstants
    {
        public const string QUEUE_NAME_PREFIX = "bbb-votes-queue";
        public const string EXCHANGE_NAME = "votes";

        public const bool QUEUE_DURABLE = true;
        public const bool QUEUE_EXCLUSIVE = false;
        public const bool QUEUE_AUTO_DELETE = false;
    }
}