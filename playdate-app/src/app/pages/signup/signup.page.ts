import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule, FormsModule  } from '@angular/forms';
import { Router } from '@angular/router';
import { AlertController, LoadingController } from '@ionic/angular';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { GlobalService } from 'src/app/services/global.service';
import { Preferences } from '@capacitor/preferences';
@Component({
  selector: 'app-signup',
  templateUrl: './signup.page.html',
  styleUrls: ['./signup.page.scss'],
})
export class SignupPage implements OnInit {
  credentials!: FormGroup;
  showPassword = false;

  constructor(
    private fb: FormBuilder,
    private authService: AuthenticationService,
    private alertController: AlertController,
    private router: Router,
    private loadingController: LoadingController,
    private global: GlobalService,
   
  ) {}

  ngOnInit() {
    this.credentials = this.fb.group(
      {
        email: ['', [Validators.required, Validators.email]],
        password: ['', [Validators.required, Validators.minLength(6)]],
        confirmPassword: ['', [Validators.required, Validators.minLength(6)]],
      },
      {
        validators: this.matchingPasswords('password', 'confirmPassword'),
      }
    );
  }

  async onSubmit() {

    await Preferences.clear();

    this.global.showLoader();   
    this.authService.register(this.email?.value, this.password?.value).then((response) =>{
      console.log("REGISTER");
      console.log(response);
     
      if(response?.data?.result?.canLogin == true){      
        //login
        let creds = {userNameOrEmailAddress: this.email?.value, password: this.password?.value};
        this.authService.login(creds).then((loginResponse) =>{
          this.global.hideLoader();
          if(loginResponse?.data?.success == true){   
            this.router.navigateByUrl('/intro', { replaceUrl: true });
          }else{
            this.global.showPop("Woops!", loginResponse?.data?.result?.error?.message);
          }
        }); 
        
      }else{
        this.global.hideLoader();
        this.global.showPop("Woops!", "A user with this email already exists.");
      }
    }).catch(err =>{
   
      this.global.showPop("Woops!", err);
      this.global.hideLoader();
    });
  }

  // Getter for easy access to form fields
  get email() {
    return this.credentials.get('email');
  }

  get password() {
    return this.credentials.get('password');
  }

  matchingPasswords(passwordKey: string, confirmPasswordKey: string) {
    return (group: FormGroup) => {
      const password = group.controls[passwordKey];
      const confirmPassword = group.controls[confirmPasswordKey];
      if (password.value !== confirmPassword.value) {
        return confirmPassword.setErrors({ mismatchedPasswords: true });
      }
    };
  }
}
