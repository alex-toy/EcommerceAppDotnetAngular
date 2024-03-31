import { Component, Input } from '@angular/core';
import { ShopParams } from '../models/shopParams';

@Component({
  selector: 'app-paging-header',
  templateUrl: './paging-header.component.html',
  styleUrls: ['./paging-header.component.scss']
})
export class PagingHeaderComponent {
  @Input() shopParams : ShopParams = new ShopParams(); 
  @Input() totalCount : number = 0;

  getPaginationString(){
    let startIndex = (this.shopParams.pageIndex - 1) * this.shopParams.pageSize + 1;
    let endIndex : number = this.shopParams.pageIndex * this.shopParams.pageSize > this.totalCount 
      ? this.totalCount 
      : this.shopParams.pageIndex * this.shopParams.pageSize;
    return `${startIndex} - ${endIndex}`;
  }
}
