using System;
using NUnit.Framework;
using Watson.SmartHomeHub.Features.GetDevices;

namespace Tests.Features.GetDevicesTests
{
    [TestFixture]
    public class DeviceCollectionTests
    {
        [Test]
        public void EqualsReturnsTrue_WithEqualDeviceCollections()
        {
            var devices1 = new Device[]
            {
                new() { Id = "1", Name = "hueBridge", Label = "Hue Bridge (6A7E0F)", Type = "hueBridge", },
                new() { Id = "2", Name = "hueBridge", Label = "Hue Bridge (6A7E0F)", Type = "hueBridge", },
            };

            var collection1 = new DeviceCollection { Devices = devices1, };
            var collection2 = new DeviceCollection { Devices = devices1, };

            var result = collection1.Equals(collection2);
            
            Assert.True(result);
        }

        [Test]
        public void EqualsReturnsFalse_WithUnEvenDeviceCollections()
        {
            var devices1 = new Device[]
            {
                new() { Id = "1", Name = "hueBridge", Label = "Hue Bridge (6A7E0F)", Type = "hueBridge", },
                new() { Id = "2", Name = "hueBridge", Label = "Hue Bridge (6A7E0F)", Type = "hueBridge", },
            };
            var devices2 = new Device[]
            {
                new() { Id = "1", Name = "hueBridge", Label = "Hue Bridge (6A7E0F)", Type = "hueBridge", },
            };

            var collection1 = new DeviceCollection { Devices = devices1, };
            var collection2 = new DeviceCollection { Devices = devices2, };

            var result = collection1.Equals(collection2);
            
            Assert.False(result);
        }
        
        [Test]
        public void EqualsReturnsFalse_WithUnEqualDeviceCollections()
        {
            var devices1 = new Device[]
            {
                new() { Id = "1", Name = "hueBridge", Label = "Hue Bridge (6A7E0F)", Type = "hueBridge", },
                new() { Id = "2", Name = "hueBridge", Label = "Hue Bridge (6A7E0F)", Type = "hueBridge", },
            };
            var devices2 = new Device[]
            {
                new() { Id = "1", Name = "hueBridge", Label = "Hue Bridge (6A7E0F)", Type = "hueBridge", },
                new() { Id = "3", Name = "hueBridge", Label = "Hue Bridge (6A7E0F)", Type = "hueBridge", },
            };

            var collection1 = new DeviceCollection { Devices = devices1, };
            var collection2 = new DeviceCollection { Devices = devices2, };

            var result = collection1.Equals(collection2);
            
            Assert.False(result);
        }

        [Test]
        public void EqualsReturnsFalse_WithNullCollection()
        {
            var devices1 = new Device[]
            {
                new() { Id = "1", Name = "hueBridge", Label = "Hue Bridge (6A7E0F)", Type = "hueBridge", },
                new() { Id = "2", Name = "hueBridge", Label = "Hue Bridge (6A7E0F)", Type = "hueBridge", },
            };

            var collection1 = new DeviceCollection { Devices = devices1, };

            var result = collection1.Equals(null);
            
            Assert.False(result);
        }
        
        [Test]
        public void EqualsReturnsTrue_WithSameCollection()
        {
            var devices1 = new Device[]
            {
                new() { Id = "1", Name = "hueBridge", Label = "Hue Bridge (6A7E0F)", Type = "hueBridge", },
                new() { Id = "2", Name = "hueBridge", Label = "Hue Bridge (6A7E0F)", Type = "hueBridge", },
            };

            var collection1 = new DeviceCollection { Devices = devices1, };

            var result = collection1.Equals(collection1);
            
            Assert.True(result);
        }

        [Test]
        public void GetHashCodeReturnsCombinationOfAllProperties()
        {
            var devices1 = new Device[]
            {
                new() { Id = "1", Name = "hueBridge", Label = "Hue Bridge (6A7E0F)", Type = "hueBridge", },
                new() { Id = "2", Name = "hueBridge", Label = "Hue Bridge (6A7E0F)", Type = "hueBridge", },
            };

            var collection1 = new DeviceCollection { Devices = devices1, };

            var result = collection1.GetHashCode();
            
            Assert.AreEqual(result, HashCode.Combine(collection1.Devices, collection1.ResultMessage));
        }
    }
}