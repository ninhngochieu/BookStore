export interface Province {
  id: number;
  name: string;
  slug: string;
  lat: number;
  lng: number;
}

export interface District {
  id: number;
  province_id: number;
  name: string;
  slug: string;
  lat: number;
  lng: number;
}
