import { Component } from '@angular/core';
import { BasketService } from './basket.service';
import { Observable } from 'rxjs';
import { IBasket, IBasketItem, IBasketTotals } from '../shared/models/basket';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.scss']
})
export class BasketComponent {
  basket$: Observable<IBasket | null> = new Observable<IBasket | null>();
  basketTotals$: Observable<IBasketTotals | null> = new Observable<IBasketTotals | null>();

  constructor(private basketService: BasketService) { }

  ngOnInit() {
    this.basket$ = this.basketService.basketSource$;
    this.basketTotals$ = this.basketService.basketTotalSource$;
  }

  removeBasketItem(item: IBasketItem) {
    this.basketService.removeItemFromBasket(item);
  }

  incrementItemQuantity(item: IBasketItem) {
    this.basketService.incrementItemQuantity(item);
  }

  decrementItemQuantity(item: IBasketItem) {
    this.basketService.decrementItemQuantity(item);
  }
}
