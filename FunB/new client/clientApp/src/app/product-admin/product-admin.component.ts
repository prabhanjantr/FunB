import { Component, OnInit, ElementRef } from '@angular/core';
import { ProductsService } from '../_services/products.service';

@Component({
  selector: 'app-product-admin',
  templateUrl: './product-admin.component.html',
  styleUrls: ['./product-admin.component.scss']
})
export class ProductAdminComponent implements OnInit {
  prd_obj:any={};
  prd_list:any=[];
  constructor(private productService:ProductsService) {
   }

  ngOnInit() {

    this.productService.getProducts().subscribe(
      data=>{
        this.prd_list = data;
        console.log(data)
      }, 
      err=> {
        console.log(err.error); 
      })
    //service call 
    // this.prd_obj = {"prd_list":[{"prd_name":"MacBook","prd_id":11,"price":602,"prd_desc":"Intel Core 2 Duo processor Powered by an Intel Core 2 Duo processor at speeds up to 2.1"},{"prd_name":"iPhone","prd_id":22,"price":603,"prd_desc":"iPhone is a revolutionary new mobile phone that allows you to make a call by simply tapping a nam.."},{"prd_name":"Apple Cinema 30","prd_id":33,"price":110,"prd_desc":"The 30-inch Apple Cinema HD Display delivers an amazing 2560 x 1600 pixel resolution. Designed sp"}]};
    // this.prd_list= this.prd_obj['prd_list'];
  }
  addToCart(prd_id:number){
    console.log(prd_id);
    this.productService.cartDetails(prd_id);
  }
}
