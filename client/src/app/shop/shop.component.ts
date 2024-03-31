import { Component, OnInit } from '@angular/core';
import { ShopService } from './shop.service';
import { Product } from '../shared/models/product';
import { Type } from '../shared/models/type';
import { Brand } from '../shared/models/brand';
import { ShopParams } from '../shared/models/shopParams';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  products : Product[] = [];
  brands : Brand[] = [];
  types : Type[] = [];
  shopParams : ShopParams = new ShopParams(); 
  sortOptions = [
    {name:'Alphabetical', value:'name'},
    {name:'Price asc', value:'priceAsc'},
    {name:'Price desc', value:'priceDesc'},
  ];

  constructor(private shopService : ShopService) {}

  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getTypes();
  }

  getProducts(){
    this.shopService.getProducts(this.shopParams).subscribe({
      next: response => this.products = response.items,
      error: response => console.log(response),
      complete: () => console.log(this.products)
    });
  }

  getBrands(){
    this.shopService.getBrands().subscribe({
      next: response => this.brands = [{id:0, name:'All'}, ...response],
      error: response => console.log(response),
      complete: () => console.log(this.brands)
    });
  }

  getTypes(){
    this.shopService.getTypes().subscribe({
      next: response => this.types = [{id:0, name:'All'}, ...response],
      error: response => console.log(response),
      complete: () => console.log(this.types)
    });
  }

  onBrandSelected(brandId : number){
    this.shopParams.brandId = brandId;
    this.getProducts();
  }

  onTypeSelected(typeId : number){
    this.shopParams.typeId = typeId;
    this.getProducts();
  }

  onSortSelected(event: any){
    this.shopParams.sort = event.target.value;
    this.getProducts();
  }
}
