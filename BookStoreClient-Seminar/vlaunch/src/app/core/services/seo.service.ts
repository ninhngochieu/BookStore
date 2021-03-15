import { Content } from '@angular/compiler/src/render3/r3_ast';
import { Injectable } from '@angular/core';
import { Meta, Title } from '@angular/platform-browser';

@Injectable({
  providedIn: 'root',
})
export class SeoService {
  constructor(private metaTagService: Meta, private titleService: Title) { }

  init() {
    this.titleService.setTitle('AdivietSports Việt Nam - Mua sắm thể thao');
    this.metaTagService.addTags([
      { name: 'generator', content: 'Vlaunch' },
      { name: 'robots', content: 'index, follow' },
      { name: 'author', content: 'Vlaunch' },
      { name: 'apple-mobile-web-app-capable', content: 'yes' },
      { name: 'apple-mobile-web-app-status-bar-style', content: 'black' },
      {
        name: 'apple-mobile-web-app-title',
        content: 'AdivietSports Việt Nam - Mua sắm thể thao',
      },
      { name: 'application-name', content: 'AdivietSports Việt Nam - Mua sắm thể thao' },
      {
        name: 'description',
        content:
          'Mua sắm thể thao trực tuyến với nhiều sản phẩm bóng đá, cầu lông, gym, boxing, giày, trang phục thể thao...Giá tốt & nhiều ưu đãi. Mua hàng online Uy tín chất lượng và có trả góp, thanh toán khi nhận hàng. Adiviet đảm bảo hàng chính hãng, được kiểm tra khi nhận hàng và được đổi nếu sản phẩm bị lỗi và không đúng size.',
      },

      { property: 'og:title', content: 'AdivietSports Việt Nam - Mua sắm thể thao' },
      {
        property: 'og:description',
        content:
          'Mua sắm thể thao trực tuyến với nhiều sản phẩm bóng đá, cầu lông, gym, boxing, giày, trang phục thể thao...Giá tốt & nhiều ưu đãi. Mua hàng online Uy tín chất lượng và có trả góp, thanh toán khi nhận hàng. Adiviet đảm bảo hàng chính hãng, được kiểm tra khi nhận hàng và được đổi nếu sản phẩm bị lỗi và không đúng size.',
      },
      { property: 'og:type', content: 'website' },
      { property: 'og:image', content: '/assets/images/favicon.svg' },
      { property: 'og:url', content: window.location.href },
    ]);
  }

  updateTitle(content: string) {
    this.titleService.setTitle(content);
  }

  updateMeta(metaTags: any[]) {
    metaTags.forEach((m) => this.metaTagService.updateTag(m));
  }
}
