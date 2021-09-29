using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Watson.SmartHomeHub.Features.GetDevices
{
    public class GetDevicesHandler : IRequestHandler<GetDevicesQuery, DeviceCollection>
    {
        public async Task<DeviceCollection> Handle(GetDevicesQuery request, CancellationToken cancellationToken)
        {
            // Connect to hubitat and get devices
            using var client = new HttpClient();
            const string? baseUri = "https://cloud.hubitat.com/api/5ac77235-1449-4060-b7e3-9383701fb52d/";
            const string? requestUri = "apps/2/devices";
            const string? accessToken = "?access_token=f6bb25f0-65f9-4413-ac03-6ed43628b5b3";

            var requestString = new StringBuilder()
                               .Append(baseUri)
                               .Append(requestUri)
                               .AppendIf($"/{request.Id}", _ => !string.IsNullOrEmpty(request.Id))
                               .Append(accessToken).ToString();

            var response = await client.GetAsync(
                requestString,
                cancellationToken);

            string parsedResponse;

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return new DeviceCollection { ResultMessage = "No Devices Found", };
            }

            try
            {
                parsedResponse = await response.Content.ReadAsStringAsync(cancellationToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new DeviceCollection { ResultMessage = "No Devices Found", };
            }

            return string.IsNullOrEmpty(request.Id)
                ? DeviceCollection.FromJson(parsedResponse)
                : new DeviceCollection { Devices = new[] { Device.FromJson(parsedResponse), }, };
        }
    }

    public static class StringExtensions
    {
        /// <summary>
        /// Perform an append with the addition value only if the specified predicate passes.
        /// </summary>
        /// <param name="builder">The string builder.</param>
        /// <param name="value">The string to append.</param>
        /// <param name="condition">The predicate that determines whether the addition will be appended.</param>
        /// <returns>Returns the string builder.</returns>
        public static StringBuilder AppendIf(this StringBuilder builder, string value, Predicate<string> condition)
        {
            return condition.Invoke(value) ? builder.Append(value) : builder;
        }

        /// <summary>
        /// Perform an append with the addition value only if the specified value is not null or empty.
        /// </summary>
        /// <param name="builder">The string builder.</param>
        /// <param name="value">The string to append.</param>
        /// <returns>Returns the string builder.</returns>
        public static StringBuilder AppendIfNotNullOrEmpty(this StringBuilder builder, string value)
        {
            return !string.IsNullOrEmpty(value) ? builder.Append(value) : builder;
        }
    }
}