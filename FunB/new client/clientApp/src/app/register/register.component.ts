import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators ,ReactiveFormsModule } from '@angular/forms';
import {LoginService} from '../_services/login.service'
import {Message, MessageService} from 'primeng/components/common/api';


// import custom validator to validate that password and confirm password fields match
import { MustMatch } from '../_helpers/MustMatch';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
  providers: [MessageService]
})
export class RegisterComponent implements OnInit {
    registerForm: FormGroup;
    submitted = false;
    msg : string ='' ;
    msgs: Message[] = [];
    constructor(private formBuilder: FormBuilder,private _loginService: LoginService,private messageService: MessageService) { }

    ngOnInit() {
        this.registerForm = this.formBuilder.group({
            email: ['', [Validators.required, Validators.email]],
            password: ['', [Validators.required, Validators.minLength(6)]],
            confirmPassword: ['', Validators.required]
        }, {
            validator: MustMatch('password', 'confirmPassword')
        });
    }

    // convenience getter for easy access to form fields
    get f() { return this.registerForm.controls; }
    data:any ='';

    onSubmit() {
        this.submitted = true;

        // stop here if form is invalid
        if (this.registerForm.invalid) {
            return;
        }
        let formData = JSON.stringify(this.registerForm.value);
        // this._loginService.RegisterUser(formData);
        this._loginService.RegisterUser(formData).subscribe(
            res=>
            {
                // this.msgs = [];
                // this.msgs.push({severity:'success',summary:'Success',detail:'Added successfully'})
                this.messageService.add({key:'br',severity:'success',detail:'Added successfully'})
            },
            err=> {this.msg = err.error.Message;
            this.msgs = [];
            this.messageService.add({key:'br',severity:'error',summary:'Error',detail:err.error.Message})

                console.log(err.error.Message);
            }
            )
            ;
            // this.data =   res.message + '\n' +res.stack });
         alert(JSON.stringify(this.registerForm.value) +'\n\n' );
    }
}
