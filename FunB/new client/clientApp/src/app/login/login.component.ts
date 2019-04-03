import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators ,ReactiveFormsModule } from '@angular/forms';
import {LoginService} from '../_services/login.service'

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  submitted = false;
    msg : string ='' ;
  constructor(private formBuilder:FormBuilder,private _loginService : LoginService) { }

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]]});
    }

// convenience getter for easy access to form fields
get f() { return this.loginForm.controls; }
data:any ='';

onSubmit() {
  this.submitted = true;

  // stop here if form is invalid
  if (this.loginForm.invalid) {
      return;
  }
  let formData = JSON.stringify(this.loginForm.value);
  // this._loginService.RegisterUser(formData);
   this._loginService.LoginUser(formData)
   .subscribe(
      res=>
      {this.msg = res;
      console.log(res);
      console.log(res.headers.get('X-Token'));
      },
      err=> {this.msg = err.error;
          console.log(err.error);
      });
   alert(JSON.stringify(this.loginForm.value) +'\n\n' );
}

}