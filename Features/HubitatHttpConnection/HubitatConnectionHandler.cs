using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Watson.SmartHomeHub.Extensions;

namespace Watson.SmartHomeHub.Features.HubitatHttpConnection
{
    public class HubitatConnectionHandler : IRequestHandler<HubitatHttpQuery, HubitatHttpResponse>
    {
        public async Task<HubitatHttpResponse> Handle(HubitatHttpQuery request, CancellationToken cancellationToken)
        {
            // Connect to hubitat and get devices
            using var client = new HttpClient();
            const string? baseUri = "https://cloud.hubitat.com/api/5ac77235-1449-4060-b7e3-9383701fb52d"; // Need to move this to secure storage
            const string? accessToken = "?access_token=f6bb25f0-65f9-4413-ac03-6ed43628b5b3";

            var requestString = new StringBuilder()
                               .Append(baseUri)
                               .AppendIf($"/{request.Uri}", _ => !string.IsNullOrEmpty(request.Uri))
                               .Append(accessToken).ToString();

            var response = await client.GetAsync(
                requestString,
                cancellationToken);

            return new HubitatHttpResponse { Response = response, };
        }
    }
}