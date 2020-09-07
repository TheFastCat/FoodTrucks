import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { throwError, Observable } from 'rxjs';
import { map } from "rxjs/operators";
import { FoodTruckFeatureCollection, Feature } from '../models/foodTruckFeatureCollection';

@Injectable({
  providedIn: 'root'
})
export class FoodTruckService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {

  }

  getApprovedVendors(): Observable<FoodTruckFeatureCollection> {
    return this.http.get<FoodTruckFeatureCollection>(this.baseUrl + 'foodtruck' + '?status=approved', { responseType: 'json' });
  }

  getExpiredVendors(): Observable<FoodTruckFeatureCollection> {
    return this.http.get<FoodTruckFeatureCollection>(this.baseUrl + 'foodtruck' + '?IsExpired=true', { responseType: 'json' });
  }

  getAllVendors(): Observable<FoodTruckFeatureCollection> {
    return this.http.get<FoodTruckFeatureCollection>(this.baseUrl + 'foodtruck' + '', { responseType: 'json' });
  }

  getApprovedFoodTrucks(): Observable<FoodTruckFeatureCollection> {
    return this.http.get<FoodTruckFeatureCollection>(this.baseUrl + 'foodtruck' + '?status=approved' + '&facilitytype=truck', { responseType: 'json' });
  }

  getAllFoodCarts(): Observable<FoodTruckFeatureCollection> {
    return this.http.get<FoodTruckFeatureCollection>(this.baseUrl + 'foodtruck' + '?facilitytype=cart', { responseType: 'json' });
  }

  getApprovedTacoTrucks(): Observable<FoodTruckFeatureCollection> {
    return this.http.get<FoodTruckFeatureCollection>(this.baseUrl + 'foodtruck' + '?status=approved' + '&facilitytype=truck'+ '&fooditems=taco', { responseType: 'json' });
  }

  getSenorSisigTacoTrucks(): Observable<FoodTruckFeatureCollection> {
    return this.http.get<FoodTruckFeatureCollection>(this.baseUrl + 'foodtruck' + '?status=approved' + '&facilitytype=truck' + '&fooditems=taco' + '&applicant=senor&nbsp;sisig', { responseType: 'json' });
  }

  getFood(foodType : string): Observable<FoodTruckFeatureCollection> {
    return this.http.get<FoodTruckFeatureCollection>(this.baseUrl + 'foodtruck' + '?fooditems=' + foodType, { responseType: 'json' });
  } 
}


