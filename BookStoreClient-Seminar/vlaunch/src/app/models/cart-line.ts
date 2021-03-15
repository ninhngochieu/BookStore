import { Product } from './product';
import { Variant } from './variant';

export interface CartLine {
  id: number;
  note: string;
  product: Product;
  quantity: number;
  total: number;
  variant: Variant;
}
