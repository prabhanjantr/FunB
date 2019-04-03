import { Component, OnInit } from '@angular/core';
import { ProductsService } from '../_services/products.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {
  title = 'clientapp';
  constructor(private productService :ProductsService){}
  
  private list = new Set();
  private cart_list:number;
  ngOnInit(){
      this.productService.data.subscribe(
      data => {
        if(data != null){
          this.list.add(data);
          this.cart_list = this.list.size;
        }
      }
      );
    console.log("value is",this.list.values);
  }

  productDetails(){
    console.log("list is",this.list);
  }
}
