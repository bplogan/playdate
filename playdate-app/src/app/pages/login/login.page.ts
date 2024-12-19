import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule, FormsModule  } from '@angular/forms';
import { Router } from '@angular/router';
import { Preferences } from '@capacitor/preferences';
import { AlertController, LoadingController } from '@ionic/angular';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { GlobalService } from 'src/app/services/global.service';

const TOKEN_KEY = 'auth-token';
const USER_KEY = 'user';
const CREDS = 'user-creds';
const USER_ID = "user-id";

@Component({
  selector: 'app-login',
  templateUrl: './login.page.html',
  styleUrls: ['./login.page.scss'],
})
export class LoginPage implements OnInit {
  credentials!: FormGroup;
  showPassword = false;
  loader:any = null;
  
  constructor(
    private fb: FormBuilder,
    private authService: AuthenticationService,
    private alertController: AlertController,
    private router: Router,
    private loadingController: LoadingController,
    private global: GlobalService
  ) {}

  ngOnInit() {
    this.credentials = this.fb.group({
      userNameOrEmailAddress: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      rememberClient: [true]
    });
  }

  async showLoader(){    
    await this.hideLoader();
    this.loader = await this.loadingController.create();
    await this.loader?.present();
  }

  async hideLoader(){
    await this.loader?.dismiss();
  }

  async login() {    
    this.showLoader();
    this.authService.login(this.credentials.value).then((result) => {
      this.authService.getSessionInfo();   
      
      this.global.startTokenRefresh();
      console.log("-go to tabs");
      this.router.navigateByUrl('/tabs');
    }).catch(err =>{
      this.hideLoader();
      console.log(err);
      this.global.showPop("Woops!", err);
    });   
  }

  // Getter for easy access to form fields
  get userNameOrEmailAddress() {
    return this.credentials.get('email');
  }

  get password() {
    return this.credentials.get('password');
  }

  // Method to navigate to the register page
  navigateToSignUp() {
    this.router.navigate(['/signup']);
  }

  ionViewDidEnter(){
    this.global.stopTokenRefresh();
    this.getDetails();
  }

  async getDetails(){
    var userId = await Preferences.get({ key: USER_ID });
    var creds =  await Preferences.get({ key: CREDS });
    var token =  await Preferences.get({ key: TOKEN_KEY });
    var user =  await Preferences.get({ key: USER_KEY });

    console.log("userId");
    console.log(userId);

    console.log("creds");
    console.log(creds);

    console.log("token");
    console.log(token);

    console.log("user");
    console.log(user);

  }

}
