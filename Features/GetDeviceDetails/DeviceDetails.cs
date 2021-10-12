using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Watson.SmartHomeHub.Features.GetDeviceDetails
{
  public record DeviceDetails
  {
#region Properties

    public int Id { get; init; }
    public string Name { get; init; } = "";
    public string Label { get; init; } = "";
    public string Type { get; init; } = "";
    public IEnumerable<DeviceAttribute> Attributes { get; init; } = Enumerable.Empty<DeviceAttribute>();
    public IEnumerable<string> Commands { get; init; } = Enumerable.Empty<string>();

#endregion

#region Public Methods

    public static DeviceDetails FromJson(string json)
    {
      DeviceDetails result = new();

      try
      {
        result = JsonConvert.DeserializeObject<DeviceDetails>(json) ?? new DeviceDetails();
      }
      catch (JsonSerializationException e)
      {
        Console.WriteLine(e);
      }

      return result;
    }

#endregion
  }

  public record DeviceAttribute
  {
#region Properties

    public string Name { get; init; } = "";
    public string CurrentValue { get; init; } = "";
    public string DataType { get; init; } = "";
    public IEnumerable<string> Values { get; init; } = Enumerable.Empty<string>();

#endregion
  }

  /// <summary>
  /// Encapsulates device capabilities.
  /// </summary>
  /// <para>Need to figure out how to parse capabilities into an object.</para>
  public record DeviceCapabilities { }

  public record DeviceCommand
  {
#region Properties

    public string Command { get; init; } = "";
    public IEnumerable<string> Type { get; init; } = Enumerable.Empty<string>();

#endregion
  }
}