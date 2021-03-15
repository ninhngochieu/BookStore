import { Component, OnDestroy, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { OwlOptions, SlidesOutputData } from 'ngx-owl-carousel-o';
import { Subscription } from 'rxjs';
import { last, take } from 'rxjs/operators';
import { SeoService } from 'src/app/core/services/seo.service';
import { cutStringWithLength, reverseString } from 'src/app/core/utils';
import { Image } from 'src/app/models/image';
import { Product } from 'src/app/models/product';
import { Variant } from 'src/app/models/variant';
import { CartService } from '../../cart/cart.service';
import { ProductService } from '../product.service';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.scss'],
})
export class ProductDetailComponent implements OnInit, OnDestroy {
  constructor(
    public service: ProductService,
    private router: Router,
    private seoService: SeoService,
    private cartService: CartService
  ) {}
  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  customOptions: OwlOptions = {
    loop: false,
    mouseDrag: true,
    touchDrag: true,
    dots: false,
    navSpeed: 700,
    autoWidth: true,
    autoHeight: true,
    margin: 10,
    responsive: {
      0: {
        items: 3,
      },
      320: {
        items: 5,
      },
      1330: {
        items: 5,
      },
    },
    nav: false,
  };

  customOptions2: OwlOptions = {
    loop: false,
    mouseDrag: true,
    touchDrag: true,
    dots: false,
    navSpeed: 700,
    autoWidth: true,
    autoHeight: true,
    responsive: {
      0: {
        items: 1,
      },
    },
    nav: false,
  };

  activeSlides: SlidesOutputData;
  product = new Product();
  mainImage: Image;
  breadcrumbData = [];
  newestProduct$;
  attributes: any[];
  variants: Variant[];
  number = 1;
  selectedVariant;
  tab = 'content';
  ratingData;
  id;
  isEmptyVariants: boolean;

  variantMappingList = [];
  variantAttrs = [];

  subscription = new Subscription();

  objectKeys = Object.keys;

  ngOnInit(): void {    
    this.subscription.add(
      this.router.events.subscribe((event) => {
        if (event instanceof NavigationEnd) {
          this.reloadCurrentRoute();
        }
      })
    );

    let url = this.router.url;
    let id = this.extractUrl(url);
    if (!id) return;
    this.id = id;
    this.service.getProductDetail(id);
    this.service.product$.pipe(take(1)).subscribe((product) => {
      this.product = product;
      if (product.images) {
        this.mainImage =
          this.product.images.length > 0 ? this.product.images[0] : null;
      }

      this.variants = product.variants;
      this.selectedVariant = this.variants[0];

      if (
        this.product.product_type 
        && this.product.product_type.has_variant 
        && this.product.product_type.attributes
      ) {
        this.variantAttrs = this.product.product_type.attributes.map(attr => {
          return {'slug': attr.slug, 'name': attr.name};
        });
        
        this.variantMappingList = this._buildVariantListMapping(this.variants, this.variantAttrs);
        this.isEmptyVariants = this.isEmptyVariant(this.variantMappingList);
      }

      this.service
        .getBreadcrumb(product.category.id)
        .pipe(take(1))
        .subscribe((result) => {
          result.forEach((item) => {
            this.breadcrumbData.push({
              name: item.name,
              link: ['/' + item.slug + '-c' + item.id],
            });
          });

          this.breadcrumbData.push({ name: product.name });
        });

      this.setupSeo(this.product);
    });
    this.service.getProductRating(id).subscribe((res) => {
      this.ratingData = res;
    });
    this.newestProduct$ = this.service.getNewestProduct();
  }

  private isEmptyVariant(variants) {
    let flag = false;
    for (let i in variants) {
      if (variants[i].items.length == 0) {
        flag = true;
      }
    }

    return flag;
  }

  private _buildVariantListMapping(variants, variantAttrs) {
    let variantAttrExists = [];
    let mappingList = [];

    for (let i in variantAttrs) {
      let variantAttr = variantAttrs[i];
      if (mappingList[i] == undefined) {
        mappingList[i] = {
          'attr_slug': variantAttr.slug,
          'attr_name': variantAttr.name,
          'items': []
        };
      }

      for (var j in variants) {
        let variant = variants[j];

        let attrItemKey = variant.attribute_item_keys.length > i ? variant.attribute_item_keys[i] : '';
        if (variantAttrExists.indexOf(attrItemKey) != -1) {
          continue;
        }
        
        let attrItem = variant.attributes.length > i ? variant.attributes[i] : null;
        mappingList[i].items.push({
          'attr_item': attrItem,
          'attr_item_key': attrItemKey,
          'variant': variant,
        });

        variantAttrExists.push(attrItemKey);
      }
    }

    return mappingList;
  }

  private setupSeo(item: Product) {
    let seoData = [];
    this.seoService.updateTitle(
      item.seo_title
        ? item.seo_title + ' - AdivietSports Việt Nam - Mua sắm thể thao'
        : item.name + ' - AdivietSports Việt Nam - Mua sắm thể thao'
    );
    seoData.push({
      property: 'og:title',
      content: item.seo_title
        ? item.seo_title + ' - AdivietSports Việt Nam - Mua sắm thể thao'
        : item.name + ' - AdivietSports Việt Nam - Mua sắm thể thao',
    });

    seoData.push({
      name: 'description',
      content: item.seo_description
        ? cutStringWithLength(item.seo_description, 300)
        : cutStringWithLength(item.brief, 300),
    });
    seoData.push({
      property: 'og:description',
      content: item.seo_description
        ? cutStringWithLength(item.seo_description, 300)
        : cutStringWithLength(item.brief, 300),
    });
    seoData.push({
      property: 'og:image',
      content: item.default_image_md,
    });
    seoData.push({
      property: 'og:url',
      content: window.location.href,
    });

    if (seoData.length > 0) {
      this.seoService.updateMeta(seoData);
    }
  }

  private extractUrl(url) {
    if (reverseString(url).indexOf('?') > 0) {
      let index = reverseString(url).indexOf('?');
      url = reverseString(reverseString(url).slice(index + 1));
    }

    let arr = url.split('-');
    let code = arr[arr.length - 1];
    return this.getID(code);
  }

  private getID(code) {
    let arrCode = code.split('');
    let type = '';
    let id = '';

    for (let i = 0; i < arrCode.length; i++) {
      let value = arrCode[i] * 1;
      if (!Number.isInteger(value)) {
        type += arrCode[i];
      } else {
        id += arrCode[i];
      }
    }

    if (type.length === 0 || id.length === 0) {
      this.router.navigate(['404']);
      return null;
    }

    return id;
  }

  public selectImage(image) {
    this.mainImage = image;
  }

  reloadCurrentRoute() {
    let currentUrl = this.router.url;
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
      this.router.navigate([currentUrl]);
    });
  }

  addProductToCart() {
    const data = {
      product_id: this.product.id,
      quantity: this.number,
      variant_id: this.selectedVariant.id,
    };

    this.cartService.addProductToCart(data);
  }

  public updateNumber(type) {
    if (type === 'add') {
      this.number++;
    } else if (this.number > 1) {
      this.number--;
    }
  }

  public onChangePageRating(page) {
    this.service.getProductRating(this.id, page).subscribe((res) => {
      if (res.results.length == 0) return;
      var oldList = this.ratingData.results;
      var newData = res.results.concat(oldList);
      res.results = newData;
      this.ratingData = res;
    });
  }
}
