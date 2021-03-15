// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  CONFIG_CHECK: 'DEBUG_MOD',
  API_DOMAIN : 'https://localhost:5001/api/',
  IMG_ROOT: 'https://localhost:5001/images/',

  // limit
  HOME_CATEGORIES_LIMIT: 6,
  HOME_CATEGORY_ROOT_LIMIT: 3,
  PRODUCT_LIST_LIMIT: 12,

  // config
  BANNER_1X4: 'banner-1x4',
  BANNER_1X2: 'banner-1x2',
  DISCOUNT_BANNER: 'discount-banner',
  CATEGORY_BANNER: 'category-banner',
  SLIDER: 'slider',
  MENU_NAVBAR: 'navbar',
  ARTICLE_MOST_VIEW: 'xem-nhieu',
  ARTICLE_DISCOUNT: 'khuyen-mai',
  PRODUCT_LIST_BANNER: 'product-list-banner',
  BREADCRUMB_BANNER: 'breadcrum-banner',
};
