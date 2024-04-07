import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IPagination } from '../shared/models/pagination';
import { IProduct } from '../shared/models/product';
import { Brand } from '../shared/models/brand';
import { Type } from '../shared/models/type';
import { ShopParams } from '../shared/models/shopParams';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl: string = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getProducts(shopParams : ShopParams){
    let params = new HttpParams();
    if (shopParams.brandId > 0) params = params.append('brandId', shopParams.brandId);
    if (shopParams.typeId) params = params.append('typeId', shopParams.typeId);
    params = params.append('sort', shopParams.sort);
    params = params.append('pageIndex', shopParams.pageIndex);
    params = params.append('pageSize', shopParams.pageSize);
    if (shopParams.search) params = params.append('search', shopParams.search);

    return this.http.get<IPagination>(`${this.baseUrl}product`, {params});
  }

  getProduct(id : number){
    return this.http.get<IProduct>(`${this.baseUrl}product/${id}`);
  }

  getBrands(){
    return this.http.get<Brand[]>(`${this.baseUrl}product/brands?pageSize=3`);
  }

  getTypes(){
    return this.http.get<Type[]>(`${this.baseUrl}product/types?pageSize=3`);
  }
}
