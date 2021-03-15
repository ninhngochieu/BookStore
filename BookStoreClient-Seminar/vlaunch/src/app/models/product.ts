import { Brand } from './brand';
import { Category } from './category';
import { Image } from './image';
import { Unit } from './unit';
import { Variant } from './variant';

export class Product {
  constructor(name?, price?) {
    this.name = name;
    this.price = price;
  }

  id: number;
  category: Category;
  unit: Unit;
  brand: Brand;
  brand_id: number;
  name: string;
  slug: string;
  code: string;
  default_image: string;
  brief: string;
  price: number;
  price_discount: number;
  rating_avg: number;
  rating_count: number;
  view_count: number;
  bought_count: number;
  images: Image[];
  default_image_md: string;
  default_image_sm: string;
  content: string;
  seo_description: string;
  seo_title: string;
  variants: Variant[];
  product_type: any;
  has_variant: boolean;
  discount_percent: number;
  free_ship: boolean;
}
