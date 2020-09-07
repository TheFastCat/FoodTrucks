using FoodTrucks;
using FoodTrucks.Controllers;
using FoodTrucks.Services;
using GeoJSON.Net.Feature;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Xunit;

namespace FoodTruckXUnitTests
{
    public class FoodTruckFileDataServiceTests
    {
        readonly IFoodTruckDataService _foodTruckDataService;
        readonly string _fileName = "foodTruckGeoJsonFormatted.json";
        FoodTruckFilter _filter = new FoodTruckFilter();
        private readonly DateTime _today;
        readonly string _todayString = "9/07/2020";
        public FoodTruckFileDataServiceTests()
        {
            var mockLogger = new Mock<ILogger<FoodTruckFileDataService>>();
            IAsyncFileReadService asyncFileReadService = new AsyncFileReadService();
            var appSettings = Options.Create(new AppSettings() { FoodTruckFeatureCollectionFilePath = _fileName, Today = _todayString });
            _foodTruckDataService = new FoodTruckFileDataService(mockLogger.Object, asyncFileReadService, appSettings);
            _today = DateTime.Parse(_todayString);
        }

        [Fact]
        public void Initialize()
        {
            Assert.True(File.Exists(_fileName));
        }

        [Fact]
        public async void GetAllVendors()
        {
            FeatureCollection featureCollection = await _foodTruckDataService.GetFoodTruckFeatures(_filter);
            Assert.NotNull(featureCollection);
            Assert.NotEmpty(featureCollection.Features);
        }

        [Fact]
        public async void GetExpiredVendors()
        {
            _filter.IsExpired = true;
            DateTime expirationDate;

            FeatureCollection featureCollection = await _foodTruckDataService.GetFoodTruckFeatures(_filter);
            foreach (Feature feature in featureCollection.Features)
            {
                Assert.NotNull(feature.Properties["expirationdate"]);
                Assert.True(DateTime.TryParse(feature.Properties["expirationdate"].ToString(), out expirationDate));
                Assert.True(_today > expirationDate);
            }
        }
       
        [Fact]
        public async void GetAllFoodTrucks()
        {
            _filter.FacilityType = new List<string>() { "truck" };
            _filter.IsNotExpired = true;
            DateTime expirationDate;

            FeatureCollection featureCollection = await _foodTruckDataService.GetFoodTruckFeatures(_filter);
            foreach (Feature feature in featureCollection.Features)
            {
                bool hasAFilteredFacilityType = _filter.FacilityType.Any(s => feature.Properties["facilitytype"].ToString().ToLower().Contains(s));

                Assert.True(hasAFilteredFacilityType);

                Assert.NotNull(feature.Properties["expirationdate"]);
                Assert.True(DateTime.TryParse(feature.Properties["expirationdate"].ToString(), out expirationDate));
                Assert.True(_today < expirationDate);
            }
        }

        [Fact]
        public async void GetAllApprovedFoodTrucks()
        {
            _filter.FacilityType = new List<string>() { "truck" };
            _filter.Status       = new List<string>() { "approved" };
            _filter.IsNotExpired = true;
            DateTime expirationDate;

            FeatureCollection featureCollection = await _foodTruckDataService.GetFoodTruckFeatures(_filter);
            foreach (Feature feature in featureCollection.Features)
            {
                bool hasAFilteredFacilityType = _filter.FacilityType.Any(s => feature.Properties["facilitytype"].ToString().ToLower().Contains(s));
                bool hasAFilteredStatus       = _filter.Status.Any(s => feature.Properties["status"].ToString().ToLower().Contains(s));

                Assert.True(hasAFilteredFacilityType);
                Assert.True(hasAFilteredStatus);

                Assert.NotNull(feature.Properties["expirationdate"]);
                Assert.True(DateTime.TryParse(feature.Properties["expirationdate"].ToString(), out expirationDate));
                Assert.True(_today < expirationDate);
            }
        }

        [Fact]
        public async void GetAllSenorHotTrucks()
        {
            _filter.FacilityType = new List<string>() { "truck" };
            _filter.Applicant    = new List<string>() { "senor", "hot" };
            _filter.IsNotExpired = true;
            DateTime expirationDate;

            FeatureCollection featureCollection = await _foodTruckDataService.GetFoodTruckFeatures(_filter);
            foreach (Feature feature in featureCollection.Features)
            {
                bool hasAFilteredFacilityType = _filter.FacilityType.Any(s => feature.Properties["facilitytype"].ToString().ToLower().Contains(s));
                bool hasAFilteredApplicant    = _filter.Applicant.Any(s => feature.Properties["applicant"].ToString().ToLower().Contains(s));

                Assert.True(hasAFilteredFacilityType);
                Assert.True(hasAFilteredApplicant);

                Assert.NotNull(feature.Properties["expirationdate"]);
                Assert.True(DateTime.TryParse(feature.Properties["expirationdate"].ToString(), out expirationDate));
                Assert.True(_today < expirationDate);
            }
        }

        [Fact]
        public async void GetAllApprovedTacoTrucksDeSenor()
        {
            _filter.FacilityType = new List<string>() { "truck" };
            _filter.Status       = new List<string>() { "approved" };
            _filter.FoodItems    = new List<string>() { "taco" };
            _filter.Applicant    = new List<string>() { "senor" };
            _filter.IsNotExpired = true;
            DateTime expirationDate;

            FeatureCollection featureCollection = await _foodTruckDataService.GetFoodTruckFeatures(_filter);
            foreach (Feature feature in featureCollection.Features)
            {
                bool hasAFilteredFacilityType = _filter.FacilityType.Any(s => feature.Properties["facilitytype"].ToString().ToLower().Contains(s));
                bool hasAFilteredStatus       = _filter.Status.Any(s => feature.Properties["status"].ToString().ToLower().Contains(s));
                bool hasAFilteredFoodItem     = _filter.FoodItems.Any(s => feature.Properties["fooditems"].ToString().ToLower().Contains(s));
                bool hasAFilteredApplicant    = _filter.Applicant.Any(s => feature.Properties["applicant"].ToString().ToLower().Contains(s));

                Assert.True(hasAFilteredFacilityType);
                Assert.True(hasAFilteredStatus);
                Assert.True(hasAFilteredFoodItem);
                Assert.True(hasAFilteredApplicant);

                Assert.NotNull(feature.Properties["expirationdate"]);
                Assert.True(DateTime.TryParse(feature.Properties["expirationdate"].ToString(), out expirationDate));
                Assert.True(_today < expirationDate);
            }
        }

        [Fact]
        public async void GetAllApprovedTacoSandwichPizzaTrucks()
        {
            _filter.FacilityType = new List<string>() { "truck" };
            _filter.Status       = new List<string>() { "approved" };
            _filter.FoodItems    = new List<string>() { "taco", "sandwich", "pizza" };
            _filter.IsNotExpired = true;
            DateTime expirationDate;

            FeatureCollection featureCollection = await _foodTruckDataService.GetFoodTruckFeatures(_filter);
            foreach (Feature feature in featureCollection.Features)
            {
                bool hasAFilteredFacilityType = _filter.FacilityType.Any(s => feature.Properties["facilitytype"].ToString().ToLower().Contains(s));
                bool hasAFilteredStatus       = _filter.Status.Any(s => feature.Properties["status"].ToString().ToLower().Contains(s));
                bool hasAFilteredFoodItem     = _filter.FoodItems.Any(s => feature.Properties["fooditems"].ToString().ToLower().Contains(s));

                Assert.True(hasAFilteredFacilityType);
                Assert.True(hasAFilteredStatus);
                Assert.True(hasAFilteredFoodItem);

                Assert.NotNull(feature.Properties["expirationdate"]);
                Assert.True(DateTime.TryParse(feature.Properties["expirationdate"].ToString(), out expirationDate));
                Assert.True(_today < expirationDate);
            }
        }
    }
}
