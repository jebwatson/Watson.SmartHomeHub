using System.Net.Http;

namespace Watson.SmartHomeHub.Features.HubitatHttpConnection
{
    public record HubitatHttpResponse
    {
        public HttpResponseMessage? Response { get; init; }
    }
}