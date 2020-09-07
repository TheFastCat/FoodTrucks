export interface FoodTruckFeatureCollection {
  type: string;
  features: Feature[];
}
export interface Feature {
  type: string;
  properties: Properties[];
  geometry: Geometry;
}
interface Geometry {
  type: string,
  coordinates: number[]
}
interface Properties {
  location_state: string;
  x: string;
  location_zip: string;
  applicant: string;
  locationdescription: string;
  dayshours: string;
  latitude: string;
  y: string;
  blocklot: string;
  location_address: string;
  noisent: string;
  location_city: string;
  cnn: string;
  objectid: string;
  longitude: string;
  block: string;
  permit: string;
  status: string;
  facilitytype: string;
  schedule: string;
  lot: string;
  address: string;
  approved: string;
  fooditems: string;
  received: string;
  expirationdate: string;
  priorpermit: string;
}
