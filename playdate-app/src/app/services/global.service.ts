import { Injectable } from '@angular/core';
import { TimeInterval } from 'rxjs';
import { AuthenticationService } from './authentication.service';
import { GetResult, Preferences } from '@capacitor/preferences';
import { AlertController, LoadingController, ToastController } from '@ionic/angular';
import { Router } from '@angular/router';

const TOKEN_KEY = 'auth-token';
const USER_KEY = 'user';
const CREDS = 'user-creds';
const USER_ID = "user-id";
const TOKEN_KEY_DATE = 'auth-token-date';

@Injectable({
  providedIn: 'root'
})

export class GlobalService {
  tokenRefresh: any;
  loader:any = null;

  constructor(
    private auth: AuthenticationService,
    private alertController: AlertController,
    private loadingController: LoadingController,
    private router: Router,
    private toastController: ToastController
  ) { }

  async compononentInit(){
   
    
  } 

  async checkAndCloseLoader() {
    // Use getTop function to find the loader and dismiss only if loader is present.
    const loader = await this.loadingController.getTop();
    // if loader present then dismiss
     if(loader !== undefined) { 
       await this.loadingController.dismiss();
     }
   }

  async hideLoader(){
     // Instead of directly closing the loader like below line
    // return await this.loadingController.dismiss();
	
    this.checkAndCloseLoader();
	
	// sometimes there's delay in finding the loader. so check if the loader is closed after one second. if not closed proceed to close again
    setTimeout(() => this.checkAndCloseLoader(), 1000);
    
  }

  async handleAPIError(error: {}){
    this.router.navigateByUrl('/login', { replaceUrl: true });
  }

  async showLoader(){    
   
    const loader = await this.loadingController.create();
    await this.loader?.present();
  }

  async getToken() : Promise<string>{
    console.log("--getting token");
    var tokenKey = (await Preferences.get({ key: TOKEN_KEY })).value || "";
    if(tokenKey == "" || tokenKey == undefined){
      console.log("--no token found, trying to refresh")
      tokenKey = await this.auth.refreshToken().then(result =>{
        console.log("--refreshed with new token");
        return result;
      });
    }
    var tokenDateString = (await Preferences.get({key: TOKEN_KEY_DATE})).value || "0" ;
    var dateNow = new Date().getTime();
    var dateToken = +tokenDateString;
    var secs = (dateNow - dateToken);
    if(secs > 600000){
      console.log("--token is expired, returning empty string")
      return "";
    }else{
      console.log("--token is good, returning token.")
      this.auth._token = tokenKey;
      return tokenKey;
    }
    
  }

  async getTokenDate() : Promise<number>{
    var tokenKey = await Preferences.get({ key: TOKEN_KEY_DATE });
    var seconds = tokenKey.value || "0";
    return +seconds;
  }
  
  async getUserId() : Promise<number>{
    var tokenKey = await Preferences.get({ key: USER_ID });
    var idString = tokenKey?.value || "0";
    return +idString || 0;
  }

  startTokenRefresh(){
    //console.log("starting token refresh ...");
    this.stopTokenRefresh();
    this.tokenRefresh = setInterval(() => {
      this.auth.refreshToken();
    }, 300000);
  }

  stopTokenRefresh(){
    if(this.tokenRefresh != null){
      clearInterval(this.tokenRefresh);
    } 
    this.tokenRefresh = null;
  }

  

  async showPop(head: string, msg: string){
    if(msg == "" || msg == null || msg == undefined || JSON.stringify(msg) == "{}"){
      msg = "An unknown error occured.";
    }
    const alert = await this.alertController.create({
      header: head,
      message: JSON.stringify(msg),
      buttons: ['OK'],
    });
    await alert.present();
  }

  async presentToast(msg: string, position: 'bottom' | 'middle' | 'top') {
    const toast = await this.toastController.create({
      message: msg,
      duration: 1500,
      position: position,
    });
    await toast.present();
  }
}
