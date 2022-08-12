namespace TrainCarAPI.Model.DTO
{
    public class MiddlewareReturnDTO
    {
        public string statusCode { get; set; }
        public object content { get; set; }
        public string identity { get; set; }
        public MiddlewareReturnDTO(string statusCode, object content, string identity)
        {
            this.statusCode = statusCode;
            this.content = content;
            this.identity = identity;
        }
    }
}
