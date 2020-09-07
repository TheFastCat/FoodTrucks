import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { FoodTruckService } from '../services/food-truck.service';
declare let L;

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  map;
  customPopupOptions;
  layerGroup;

  constructor(private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
    private foodTruckService: FoodTruckService) {

    this.customPopupOptions =
    {
      'maxWidth': '500',
      'height':    '500',
      'className': 'custom'
    }
  }

  ngOnInit() {
    this.map = L.map('map').setView([37.7660327, -122.4409502], 12.65);
    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
      attribution: '© <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
    }).addTo(this.map);
    this.layerGroup = L.layerGroup().addTo(this.map);
  }

  clearMap() {
    this.layerGroup.clearLayers();
  }

  onEachFeature(feature, layer) {
    var popup = '';

    if (feature.properties.applicant) {
      popup += `<b>${feature.properties.applicant}</b><br/>`;
    }

    if (feature.properties.facilitytype) {
      popup += `<span style='font-size:12px'>${feature.properties.facilitytype}</span><br/>`;
    }

    if (feature.properties.fooditems) {
      popup += `<span style='font-size:12px'>${feature.properties.fooditems}<br/>`;
    }

    if (feature.properties.schedule) {
      popup += `<a style='color:#0366d6; font-size:14px;' href='${feature.properties.schedule}' target="_blank" rel="noopener noreferrer">Schedule</a><br/>`;
    }

    if (feature.properties.dayshours) {
      popup += `<span>${feature.properties.dayshours}</span><br/>`;
    }

    if (feature.properties.expirationdate) {
      var d1 = Date.parse("2020-09-07");
      var d2 = Date.parse(feature.properties.expirationdate);
      if (d1 > d2) {
        popup += `<b><span style='color:red; font-size:14px;'>Expired Permit</span></b><br/>`;
      }
    }

    layer.bindPopup(popup, this.customPopupOptions);
  }

  getApprovedVendors() {
    this.foodTruckService.getApprovedVendors().subscribe(featureCollection => {
      L.geoJSON(featureCollection, {
        onEachFeature: this.onEachFeature
      }).addTo(this.layerGroup);
    });
  }

  getExpiredVendors() {
    this.foodTruckService.getExpiredVendors().subscribe(featureCollection => {
      L.geoJSON(featureCollection, {
        onEachFeature: this.onEachFeature
      }).addTo(this.layerGroup);
    });
  }

  getAllVendors() {
    this.foodTruckService.getAllVendors().subscribe(featureCollection => {
      L.geoJSON(featureCollection, {
        onEachFeature: this.onEachFeature
      }).addTo(this.layerGroup);
    });
  }

  getApprovedFoodTrucks() {
    this.foodTruckService.getApprovedFoodTrucks().subscribe(featureCollection => {
      L.geoJSON(featureCollection, {
        onEachFeature: this.onEachFeature
      }).addTo(this.layerGroup);
    });
  }

  getAllFoodCarts() {
    this.foodTruckService.getAllFoodCarts().subscribe(featureCollection => {
      L.geoJSON(featureCollection, {
        onEachFeature: this.onEachFeature
      }).addTo(this.layerGroup);
    });
  }

  getApprovedTacoTrucks() {
    this.foodTruckService.getApprovedTacoTrucks().subscribe(featureCollection => {
      L.geoJSON(featureCollection, {
        onEachFeature: this.onEachFeature
      }).addTo(this.layerGroup);
    });
  }

  getSenorSisigTacoTrucks() {
    this.foodTruckService.getSenorSisigTacoTrucks().subscribe(featureCollection => {
      L.geoJSON(featureCollection, {
        onEachFeature: this.onEachFeature
      }).addTo(this.layerGroup);
    });
  }

  getFood(foodType : string) {
    this.foodTruckService.getFood(foodType).subscribe(featureCollection => {
      L.geoJSON(featureCollection, {
        onEachFeature: this.onEachFeature
      }).addTo(this.layerGroup);
    });
  }

}
