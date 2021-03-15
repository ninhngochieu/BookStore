import { UrlSegment } from '@angular/router';

export function formatPrice(value) {
  value = value.toString().replace(/[\,]+/g, '');
  value = value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
  return value;
}

export function removeAccents(str) {
  str = str.toLowerCase();
  str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, 'a');
  str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, 'e');
  str = str.replace(/ì|í|ị|ỉ|ĩ/g, 'i');
  str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, 'o');
  str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, 'u');
  str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, 'y');
  str = str.replace(/đ/g, 'd');
  // str = str.replace(/!|@|%|\^|\*|\(|\)|\+|\=|\<|\>|\?|\/|,|\.|\:|\;|\'|\"|\&|\#|\[|\]|~|\$|_|`|-|{|}|\||\\/g," ");
  str = str.replace(/ + /g, ' ');
  str = str.trim();
  return str;
}

export class UploadService {
  public imagePath;
  imgURL: any;

  preview(files) {
    if (files.length === 0) {
      this.imgURL = null;
      return;
    }

    var mimeType = files[0].type;

    if (mimeType.match(/image\/*/) == null) {
      console.error('Only images are supported.');
      return;
    }

    var reader = new FileReader();
    this.imagePath = files[0];
    reader.readAsDataURL(files[0]);
    reader.onload = (_event) => {
      this.imgURL = reader.result;
    };
  }
}

export function cutStringWithLength(text: string ,length: number) {
  if(!text) return '';
  if(text.length <= length) return text;

  return text.slice(0, length);
}

export function cleanObj(obj) {
  var propNames = Object.getOwnPropertyNames(obj);
  for (var i = 0; i < propNames.length; i++) {
    var propName = propNames[i];
    if (obj[propName] === null || obj[propName] === undefined) {
      delete obj[propName];
    }
  }
}

export function customMatcher(url: UrlSegment[], type: string[]) {
  if (url.length === 0 || type.length === 0) return null;
  if (url[0].path.indexOf('-') < 0) return null;
  let arr = url[0].path.split('-');
  let match = false;
  type.forEach((i) => {
    if (arr[arr.length - 1].indexOf(i) > -1) {
      match = true;
    }
  });

  return match ? { consumed: url } : null;
}

export function generateUrl(item): any {
  if (!item) {
    return ['/'];
  }

  if (item.url) {
    var url = new URL(item.url);
    return url.pathname;
  }

  if (item.product) {
    return ['/' + item.product.slug + '-p' + item.product.id];
  }

  if (item.category) {
    return ['/' + item.category.slug + '-c' + item.category.id];
  }

  if (item.collection) {
    return ['/' + item.collection.slug + '-co' + item.collection.id];
  }

  if (item.post) {
    return ['/' + 'blog/' + item.post.slug + '-' + item.post.id];
  }

  if (item.tag) {
    return ['/' + 'blog/' + '?tag_id=' + item.tag.id];
  }

  return ['/'];
}

export function reverseString(str) {
  return str === '' ? '' : reverseString(str.substr(1)) + str.charAt(0);
}
