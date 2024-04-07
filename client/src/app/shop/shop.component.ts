import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ShopService } from './shop.service';
import { IProduct } from '../shared/models/product';
import { Type } from '../shared/models/type';
import { Brand } from '../shared/models/brand';
import { ShopParams } from '../shared/models/shopParams';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  @ViewChild('search') searchTerm? : ElementRef;
  products : IProduct[] = [];
  brands : Brand[] = [];
  types : Type[] = [];
  shopParams : ShopParams = new ShopParams(); 
  sortOptions = [
    {name:'Alphabetical', value:'name'},
    {name:'Price asc', value:'priceAsc'},
    {name:'Price desc', value:'priceDesc'},
  ];
  totalCount : number = 0;

  constructor(private shopService : ShopService) {}

  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getTypes();
  }

  getProducts(){
    this.shopService.getProducts(this.shopParams).subscribe({
      next: response => {
        this.products = response.items;
        this.shopParams.pageIndex = response.pageIndex;
        this.shopParams.pageSize = response.pageSize;
        this.totalCount = response.count;
      },
      error: response => console.log(response),
      // complete: () => console.log(this.products)
    });
  }

  getBrands(){
    this.shopService.getBrands().subscribe({
      next: response => this.brands = [{id:0, name:'All'}, ...response],
      error: response => console.log(response),
      // complete: () => console.log(this.brands)
    });
  }

  getTypes(){
    this.shopService.getTypes().subscribe({
      next: response => this.types = [{id:0, name:'All'}, ...response],
      error: response => console.log(response),
      // complete: () => console.log(this.types)
    });
  }

  onBrandSelected(brandId : number){
    this.shopParams.brandId = brandId;
    this.shopParams.pageIndex = 1;
    this.getProducts();
  }

  onTypeSelected(typeId : number){
    this.shopParams.typeId = typeId;
    this.shopParams.pageIndex = 1;
    this.getProducts();
  }

  onSortSelected(event: any){
    this.shopParams.sort = event.target.value;
    this.getProducts();
  }

  onPageChanged(pageIndex: number){
    if (this.shopParams.pageIndex === pageIndex) return;
    this.shopParams.pageIndex = pageIndex
    this.getProducts();
  }

  onSearch(){
    this.shopParams.search = this.searchTerm?.nativeElement.value;
    this.shopParams.pageIndex = 1;
    this.getProducts();
  }

  onReset(){
    this.shopParams = new ShopParams();
    this.getProducts();
  }
}
