import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '../services/authentication.service';
import { Preferences } from '@capacitor/preferences';
import { AlertController, LoadingController, NavController, ModalController } from '@ionic/angular';
import { GlobalService } from '../services/global.service';
import { FriendModalComponent } from '../modals/friend-modal/friend-modal.component';

@Component({
  selector: 'app-tab1',
  templateUrl: 'tab1.page.html',
  styleUrls: ['tab1.page.scss'],
})
export class Tab1Page implements OnInit {
  players:[] = [];
  events:[] = [];
  friends:[] = [];
  loading:boolean = false;

  constructor(
    private authService: AuthenticationService,
    private router: Router,
    private loadingController: LoadingController,
    private global: GlobalService,
    private nav: NavController,
    private modalCtrl: ModalController
  ) {}

  
  async logout() {
    await Preferences.clear();
    this.router.navigateByUrl('login', { replaceUrl: true });
  }
  
  goAddPlayer(){

  }

  async goAddFriend(){
    const friendModal = await this.modalCtrl.create({
      component: FriendModalComponent,
    });
    friendModal.present();

    const { data, role } = await friendModal.onWillDismiss();

    if (role === 'confirm') {
      alert("confirm");
    }
  }

  async ngOnInit() {    
   
  }

  goBack(){
    this.nav.back();
  }
 
  goUser(){
    this.router.navigate(['user']);
  }

  async ionViewDidEnter(){
    await this.global.showLoader();
    var payload = {userId: -1 , playerId: -1, includeDetails: false};
    return this.authService.getPlayers(payload).then(response =>{  
    
     if(response.data['success'] == true){
        this.players = response.data["result"];
        
      }else{
        //this.global.handleAPIError(response.result);
      }
      this.global.hideLoader();
    }).catch((err) =>{
      //this.global.handleAPIError(err);
      this.global.hideLoader();
    });
  }
  
}
