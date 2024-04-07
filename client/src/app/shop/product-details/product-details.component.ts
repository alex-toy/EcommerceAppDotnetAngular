import { Component, OnInit } from '@angular/core';
import { IProduct } from 'src/app/shared/models/product';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';
import { BasketService } from 'src/app/basket/basket.service';
import { Observable, take } from 'rxjs';
import { IBasket, IBasketItem } from 'src/app/shared/models/basket';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  product?: IProduct;
  quantity = 1;
  quantityInBasket = 0;
  item : IBasketItem | undefined = undefined;

  constructor(
      private shopService : ShopService, 
      private activatedRoute : ActivatedRoute, 
      private breadcrumbService: BreadcrumbService,
      private basketService: BasketService
    ) {
    this.breadcrumbService.set('@productDetails', ' ');
  }

  ngOnInit(): void {
    this.getProduct();
  }

  getProduct() {
    let id : string = this.activatedRoute.snapshot.paramMap.get('id') ?? "";
    this.shopService.getProduct(+id).subscribe({
      next: response => {
        this.product = response;
        this.breadcrumbService.set('@productDetails', this.product.name);
        this.basketService.basketSource$.pipe(take(1)).subscribe({
          next : basket => {
            this.item = basket?.items.find(x => x.id === +id);
            if (this.item) {
              this.quantity == this.item.quantity;
              this.quantityInBasket = this.item.quantity;
            }
          }
        });
      },
      error: response => console.log(response),
      // complete: () => console.log(this.product)
    });
  }

  addItemToBasket() {
    if (!this.product) return;
    this.basketService.addItemToBasket(this.product, this.quantity);
  }

  incrementQuantity() {
    this.quantity++;
  }

  decrementQuantity() {
    if (this.quantity > 1) this.quantity--;
  }

  updateBasket(){
    if (!this.product) return;
    if (this.quantity > this.quantityInBasket) {
      const itemsToAddCount = this.quantity - this.quantityInBasket;
      this.quantityInBasket += itemsToAddCount;
      this.basketService.addItemToBasket(this.product, itemsToAddCount);
    } else {
      const itemsToRemoveCount = this.quantityInBasket - this.quantity;
      this.quantityInBasket -= itemsToRemoveCount;
      if (this.item === undefined) return;
      this.basketService.removeItemFromBasket(this.item);
    }
  }

  get buttonText(){
    return this.quantityInBasket === 0 ? 'Add to basket' : 'Update basket';
  }
}
