import { Product } from './models/product';

export const DEFAULT_PRODUCT = new Product('Sản phẩm mẫu', 999999);
export const DEFAULT_PRODUCT_LIST = [
  DEFAULT_PRODUCT,
  DEFAULT_PRODUCT,
  DEFAULT_PRODUCT,
  DEFAULT_PRODUCT,
  DEFAULT_PRODUCT,
];

export const PRODUCT_PARAM_CODE = {
  c: 'category_ids',
  co: 'collection_id',
};

export const PRICE_FILTER_VALUE = {
  1: {
    max_price: 100000,
  },
  2: {
    min_price: 100000,
    max_price: 200000,
  },
  3: {
    min_price: 200000,
    max_price: 300000,
  },
  4: {
    min_price: 300000,
    max_price: 500000,
  },
  5: {
    min_price: 500000,
    max_price: 1000000,
  },
  6: {
    min_price: 1000000,
  },
};
