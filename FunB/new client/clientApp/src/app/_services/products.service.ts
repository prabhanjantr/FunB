import { Injectable } from '@angular/core';
import { of, BehaviorSubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HttpHeaders, HttpClient } from '@angular/common/http';
const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':  'application/json',
  })
};

@Injectable({
  providedIn: 'root'
})
export class ProductsService {

  constructor(private http: HttpClient) { }
  private _producturl = environment.api + environment.products;
  private dataSource = new BehaviorSubject<number>(null);
  data = this.dataSource.asObservable();
  cartDetails(data){
      this.dataSource.next(data);
  }
  getProducts(){
    return this.http.get(this._producturl) ; 
  }
}
