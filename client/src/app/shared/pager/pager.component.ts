import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ShopParams } from '../models/shopParams';

@Component({
  selector: 'app-pager',
  templateUrl: './pager.component.html',
  styleUrls: ['./pager.component.scss']
})
export class PagerComponent {
  @Input() shopParams : ShopParams = new ShopParams(); 
  @Input() totalCount : number = 0;
  @Output() pageChanged = new EventEmitter<number>()

  onPageChanged(event: any){
    this.pageChanged.emit(event.page);
  }
}
