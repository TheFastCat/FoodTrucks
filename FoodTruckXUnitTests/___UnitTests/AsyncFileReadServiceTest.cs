using FoodTrucks;
using FoodTrucks.Controllers;
using FoodTrucks.Services;
using GeoJSON.Net.Feature;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Xunit;

namespace FoodTruckXUnitTests
{
    public class AsyncFileReadServiceTest
    {
        IAsyncFileReadService _asyncFileReadService;
        readonly string _fileName = "TestFiles/TextFile1.txt";

        [Fact]
        public void FileExists()
        {
            Assert.True(File.Exists(_fileName));
        }

        [Fact]
        public async void ReadAllText()
        {
            _asyncFileReadService = new AsyncFileReadService();
            string allReadText = await _asyncFileReadService.ReadAllText(_fileName);
            Assert.Equal("Hello World!", allReadText.TrimEnd());
        }
    }
}
