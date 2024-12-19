import { Injectable } from '@angular/core';
import { CapacitorHttp } from '@capacitor/core';
import { Preferences } from '@capacitor/preferences';
import { BehaviorSubject, from, map, Observable, switchMap, tap } from 'rxjs';
import { pdPlayer } from '../classes/pdPlayer';
import { HttpResponse } from '@angular/common/http';


const TOKEN_KEY = 'auth-token';
const USER_KEY = 'user';
const CREDS = 'user-creds';
const USER_ID = "user-id";
const TOKEN_KEY_DATE = 'auth-token-date';
const GET = "GET";
const POST = "POST";

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  // Init with null to filter out the first value in a guard!
  

  _apiUrl = 'https://playdate-api.azurewebsites.net/api/';
  public _token:string = "";

  constructor(
    //
  ) {
    
  }
  

  getHeaders() : any{ 
   
    let httpHeader = {
      'Content-Type': 'application/json', 
      'Authorization': 'Bearer ' + this._token,
      'Abp.TenantId' : '2',
      'X-XSRF-TOKEN' : 'CfDJ8MI-uK__o0RIqheI-_zAqeKvjZQI7_H8i2GJQPGbb2PXuOLn3tVn-TKEqfAVHOtiaWuY4lBAWr7tfmiskDS4HD1c4Nc9Uv39pVk7ZOmhiv0FeebA-71SsQkcDf1yKXlqfpL2dor1p4nVz_BmfvK2dhfz2mRfq0Y6JJdJFc5zPjRrHkGSP2A3iq7fNhtQQ2pJSA'
    };
    return httpHeader;   
  }

  getHeadersAuth(){
    let httpHeader = {      
      'Content-Type': 'application/json', 
      'Abp.TenantId' : '2',
      'X-XSRF-TOKEN' : 'CfDJ8CKx_EPa4vtLgYGM9_I55f3Ltl-B4M6aOxipw1p3wM4BL-HlQ5auGP59E9y-HsA99kptd0zzA7v58XVsKJgxkSfLNG7d890b4cYiFPltuOlY5v-1LlkT8sIgrfroz9Ba3--Wiz9WTcdDfhW8_aVymXg'    
    }
    return httpHeader;
  }

  getHttpOptions(path : string, data: {}, postType: string){
    if(postType == "GET"){
      return {
        url: this._apiUrl + path,        
        headers: this.getHeaders()
      }
    }else{
      return {
        url: this._apiUrl + path,
        data: data,
        headers: this.getHeaders()
      }
    }

  }

  getHttpOptionsAuth(data: {}){
    return {
      url: this._apiUrl + 'TokenAuth/Authenticate',
      data: data,
      headers: this.getHeadersAuth()
    }
  }
 
  async login(credentials: { userNameOrEmailAddress: any; password: any }): Promise<any> {   
    console.log("-logging in");
    return CapacitorHttp.post(this.getHttpOptionsAuth(credentials)).then((result) =>{
      let now = new Date().getTime().toString();
      this._token = result.data['result']['accessToken']
      Preferences.set({key:USER_ID, value: result.data['result']['userId']});  
      Preferences.set({key:CREDS, value: JSON.stringify(credentials)});    
      Preferences.set({ key: TOKEN_KEY, value: result.data['result']['accessToken'] });
      Preferences.set({ key: TOKEN_KEY_DATE, value: now });
      console.log("logged in");
      console.log(result);
      return result;
    });
    
  }

  async refreshToken(): Promise<string>{
    console.log("-refreshing token...");
    let creds = (await Preferences.get({key: CREDS})).value;
    let data = JSON.parse(creds || "");
    //console.log(data);
    return CapacitorHttp.post(this.getHttpOptionsAuth(data)).then((result) =>{
      //console.log("--token refreshed.");
      let now = new Date().getTime().toString();
      this._token = result.data['result']['accessToken'];
      Preferences.set({ key: TOKEN_KEY, value: result.data['result']['accessToken'] });
      Preferences.set({ key: TOKEN_KEY_DATE, value: now });
      return this._token;
    }); 
  }

  async getString(k: string): Promise<string> {
    const ret = await Preferences.get({ key: k });
    return (ret.value || '');
  }

  async getSessionInfo(){
    return CapacitorHttp.get(this.getHttpOptions('services/app/Session/GetCurrentLoginInformations',{}, GET))
    .then((data) =>{
      Preferences.set({ key: USER_KEY, value: JSON.stringify(data) })
      return data;
    });
  }

  async logout(): Promise<void> { 
    await Preferences.remove({ key: TOKEN_KEY });
    
  }
  //-----------------------------------------------------------------------------
  async register(email: string, password: string ) : Promise<any> {
    var input = {
      name: email.split('@')[0],
      surname: ".",
      userName: email,
      emailAddress: email,
      password: password,
      captchaResponse: ""
    };
    console.log(input);
    return CapacitorHttp.post(this.getHttpOptions("services/app/account/register", input, POST)).catch(err =>{
      console.log("ERRRRR");
      console.log(err);
      alert(JSON.stringify(err));
    });

  }

  //get the current users' players
  async getPlayers(input: {userId: number, playerId: number, includeDetails: boolean}): Promise<any> {
    console.log("INPUT");
    console.log(input);
    return CapacitorHttp.get(this.getHttpOptions("services/app/player/getPlayers?playerId=" + input.playerId + "&userId=" + input.userId, {}, GET));  
  }

  //get a single player's details
  async getPlayerDetails(input: {id: number}) : Promise<any> {
    return CapacitorHttp.get(this.getHttpOptions("services/app/player/getPlayerDetails?id=" + input.id, input, GET));
  }

  async createOrUpdatePlayer(player: pdPlayer) {   
    return CapacitorHttp.post(this.getHttpOptions("services/app/player/CreateOrUpdatePlayer", player, POST));    
  }

  async createOrUpdatePlayerFriend(input: {id:string, playerIds: number[], playerCode: string, statusId: number}){
    return CapacitorHttp.post(this.getHttpOptions("services/app/player/createOrUpdatePlayerFriend", input, POST));
  }
}
