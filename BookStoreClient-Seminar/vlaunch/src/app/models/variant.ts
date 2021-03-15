import { ProductAttribute } from './attribute';

export interface Variant {
  attributes: ProductAttribute[];
  id: number;
  images: string;
  name: string;
  price_override: number;
  product_id: number;
}
