using GeoJSON.Net.Feature;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTrucks.Services
{
    public class FoodTruckFileDataService : IFoodTruckDataService
    {
        readonly ILogger<FoodTruckFileDataService> _logger;
        readonly IAsyncFileReadService             _asyncFileReadService;
        FeatureCollection                          _featureCollection;
        readonly string                            _filePath;
        readonly string                            _todayString;
        readonly DateTime                          _todayDate;
        public FoodTruckFileDataService(ILogger<FoodTruckFileDataService> logger,
                                        IAsyncFileReadService asyncFileReadService,
                                        IOptions<AppSettings> appSettings)
        {
            _asyncFileReadService = asyncFileReadService;
            _filePath             = appSettings.Value.FoodTruckFeatureCollectionFilePath;
            _todayString          = appSettings.Value.Today;
            _todayDate            = DateTime.Parse(_todayString).Date;
            _logger               = logger;
        }

        public async Task<FeatureCollection> GetFeatureCollectionFromFile(string filePath)
        {
            if (_featureCollection == null)
            {
                string allJsonData = await _asyncFileReadService.ReadAllText(filePath);
                _featureCollection = JsonConvert.DeserializeObject<FeatureCollection>(allJsonData);
            } 

            return _featureCollection;
        }

        async Task<FeatureCollection> IFoodTruckDataService.GetFoodTruckFeatures(FoodTruckFilter filter)
        {
            FeatureCollection featureCollection = await GetFeatureCollectionFromFile(_filePath);

            IEnumerable<Feature> workingSet = featureCollection.Features;

            if (filter.Applicant.Any())
            {
                workingSet = SelectWhere("applicant", isFilteredOn: filter.Applicant, from: workingSet);
            }

            if (filter.FoodItems.Any())
            {
                workingSet = SelectWhere("fooditems", isFilteredOn: filter.FoodItems, from: workingSet);
            }

            if (filter.Status.Any())
            {
                workingSet = SelectWhere("status", isFilteredOn: filter.Status, from: workingSet);
            }

            if (filter.FacilityType.Any())
            {
                workingSet = SelectWhere("facilitytype", isFilteredOn: filter.FacilityType, from: workingSet);
            }

            if (filter.IsNotExpired.HasValue && filter.IsNotExpired == true)
            {
                workingSet = from feature in workingSet
                             where feature.Properties["expirationdate"] != null
                             && _todayDate < DateTime.Parse(feature.Properties["expirationdate"].ToString()).Date
                             select feature;
            }
            else if (filter.IsExpired.HasValue && filter.IsExpired == true)
            {
                workingSet = from feature in workingSet
                             where feature.Properties["expirationdate"] != null
                             && _todayDate > DateTime.Parse(feature.Properties["expirationdate"].ToString()).Date
                             select feature;
            }

            return new FeatureCollection(workingSet.ToList()); ;
        }

        public IEnumerable<Feature> SelectWhere(string propertyName, IEnumerable<string> isFilteredOn, IEnumerable<Feature> from)
        {
            IEnumerable<Feature> results = from feature in @from
                                           where feature.Properties[propertyName] != null
                                           && isFilteredOn.Any(s => feature.Properties[propertyName].ToString().ToLower().Contains(s))
                                           select feature;
            return results;
        }

        /* A future implementation of IFoodTruckDataService could be driven by more generic, dynamic filtering using reflection.
         * This could remove If/then filtering logic; instead just iterate through populated filters and apply the same SelectWhere logic.
         * In this way additional property filter capabilities could be added _by only_ extending the argument FoodTruckFilter class with 
         * more properties.
        private static List<KeyValuePair<string, string>> GetNormalizedFilterValues(FoodTruckFilter filter)
        {
            // derive populated, normalized key/value FoodTruck filter arguments
            List<KeyValuePair<string, string>> normalizedFilterValues = filter.GetType().GetProperties()
                                                                       .Select(prop => new KeyValuePair<string, string>(prop.Name, prop.GetValue(filter, null)?.ToString().ToLower()))
                                                                       .Where(pair => !string.IsNullOrWhiteSpace(pair.Value != null ? pair.Value.ToString() : string.Empty))
                                                                       .ToList();
            return normalizedFilterValues;
        }
        */

    }
}
