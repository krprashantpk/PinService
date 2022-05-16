namespace PinService.Domain.Core.Logging
{
    public class Request
    {
        public Request(string httpMethod, string resource, string query, string body, Dictionary<string, string> headers)
        {
            HttpMethod = httpMethod;
            Resource = resource;
            Query = query;
            Body = body;
            Headers = headers;
        }

        public string HttpMethod { get; private set; }
        public string Resource { get; private set; }
        public string Query { get; private set; }
        public string Body { get; private set; }
        public Dictionary<string, string> Headers { get; private set; }
    }
}
