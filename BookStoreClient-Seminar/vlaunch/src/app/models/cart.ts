export class Cart {
  discount_amount: number;
  lines: [];
  note: string;
  quantity: number;
  shipping_address: any;
  token: string;
  user: any;
}

export interface CartSummary {
  discount_amount: number;
  quantity: number;
  shipping_fee: number;
  total: number;
  subtotal: number;
}
