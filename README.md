# Food Trucks
This is a simple map application for locating food trucks (and push carts) in San Francisco. Users can filter geo-located food vendors on criteria including cuisine, name by clicking. Vendor metadata is displayed as popup tooltips when location markers on the map are clicked.

## Running the application
For best play-along experience:
 1. open 'FoodTrucks.sln' in Visual Studio 2019
 2. `cd` a command prompt into  'FoodTrucks/ClientApp'
 3. type: `npm install` and wait until completed
 4. Run `ng serve` for a dev server. Navigate to [http://localhost:4200/](http://localhost:4200/). The app *should* automatically reload if you change any of the source files.
 5. Within Visual Studio, right-click the `FoodTrucks.sln` file and select `Build Solution`. 
 6. After build is completed, verify that `FoodTrucks.csproj` is set as the startup project and press f5 to launch the application.

## Using the application

 - Use Buttons at the top of the page to control loading map
   information.
 - You can press the space bar key on your keyboard to
   clear the map, or use the yellow 'clear' button
 - Left-clicking a map marker will display information associated with a food vendor including:
	 - **Name** (aka 'applicant')
	 - **Type of vendor** (aka 'facilitytype' -- ie truck, push cart)
	 - **Cuisine** (aka 'fooditems' -- eg pizza, hot dogs)
	 - A link to generate an online **schedule** .pdf (this is loaded in a separate tab as it can take minutes to be generated due to external dependency);
	 - **Hours of operation** (aka 'dayshours' -- this field is deprecated and only visible on expired vendors)
	 - If the vendor location's city permit is **expired**, it will also appear
	 - 
### Configuration
 Because there does not exist a real-time data API to pull San Francisco's food truck data, In order to maintain a consistent UX for the remainder of eternity (and keep presumptive testing unaffected in the future), the FoodTrucks application uses static "today" date used for comparison of food truck data (such as license expiration date).
 
 On account of this, `appsettings.json` contains two variables:
 
      "AppSettings": {
        "FoodTruckFeatureCollectionFilePath": "foodTruckGeoJsonFormatted.json",
        "Today": "9/07/2020"
      }
 - **FoodTruckFeatureCollectionFilePath** : the file containing a valid GeoJSON `FeatureCollection` representing San Francisco's food truck data set.
 - **Today** : Date text string to use for comparisons against expiration dates of data set ie: "9/07/2020"

## Technical Overview
This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 6.0.0 and .NET Core 3.1

[Leaflet.js](https://leafletjs.com/) is used for map rendering.

[xUnit.net](https://xunit.net/) is used for automated, back-end testing of the API and component services.

Scaffolding for both [Karma](https://karma-runner.github.io) and [Protractor](http://www.protractortest.org/) is present for front-end testing, though minimally implemented.

The Angular UI retrieves food vendor location information from the API which in turn reads it from a static GeoJSON file downloaded from [here](https://data.sfgov.org/Economy-and-Community/Mobile-Food-Facility-Permit/rqzj-sfat/data).

Effort was made for separation of concerns, declarative intent and extensibility.

## Automated Testing
### Back-end
 Examples of technical unit, system and integration tests in C# are provided within 'FoodTruckXUnitTests.csproj' of the FoodTrucks.sln. To execute these:
 
 1. Within Visual Studio, open the 'Test Explorer' view
 2. Right-click 'FoodTruckXUnitTests' and select ''Run"

### Front-end
Automated front-end testing is supported but minimally implemented:

 - Type `ng e2e` from the command line within 'FoodTrucks/ClientApp' to execute 

## Opportunities for extension
 - [Custom map marker colors](https://github.com/pointhi/leaflet-color-markers) for filter type. This would allow "pizza" vendors to be visually discrete from "taco" vendors at a glance.
 - Custom user text search 
 - New implementation to `IFoodTruckDataService` to provide "real-time-ish" data query. While no public API apparently exists for San Francisco's food truck open dataset is [located here](https://data.sfgov.org/Economy-and-Community/Mobile-Food-Facility-Permit/rqzj-sfat/data), there does exist a .csv report download that could be used pulled at intervals for a more valid representation of current data.

