import { Component, OnInit } from '@angular/core';
import { LoadingController, ModalController, NavController } from '@ionic/angular';
import { AuthenticationService } from '../services/authentication.service';
import { Preferences } from '@capacitor/preferences';
import { Router, NavigationExtras } from '@angular/router';
import { pdPlayer } from '../classes/pdPlayer';
import { FriendModalComponent } from '../modals/friend-modal/friend-modal.component';
import { GlobalService } from '../services/global.service';

@Component({
  selector: 'app-tab2',
  templateUrl: 'tab2.page.html',
  styleUrls: ['tab2.page.scss']
})
export class Tab2Page implements OnInit {

  players:pdPlayer[] = [];
  friends:pdPlayer[] = [];
  allFriends:pdPlayer[] = [];
  loading:boolean = false;
  
  constructor(
    private authService: AuthenticationService,
    private router: Router,
    private loadingController: LoadingController,
    private modalCtrl: ModalController,
    private nav: NavController,
    private global: GlobalService
  ) {}

  async ngOnInit() {

  }
  goAddPlayer(){

  }

  goBack(){
    this.nav.back();
  }

  goUser(){
    this.router.navigate(['user']);
  }

  async goAddFriend(){
    const modal = await this.modalCtrl.create({
      component: FriendModalComponent,
    });
    modal.present();

    const { data, role } = await modal.onWillDismiss();

    if (role === 'confirm') {
      alert("confirm");
    }
  }

  goPlayer(playerId: number){
   
    let navigationExtras: NavigationExtras = {
      queryParams: {
        userId: playerId
      }
    };
    this.router.navigate(['player'], navigationExtras);
  }


  async ionViewDidEnter() {
  
    this.loading = true;
    let userId = (await Preferences.get({key:'user-id'})).value || 0;  
    var payload = {userId: +userId , playerId: -1, includeDetails: true};
    this.authService.getPlayers(payload).then(response =>{
      if(response.data['success'] == true){
        this.players = response.data["result"];
        for(let player of this.players){
          if(player['friends'] && player['friends'].length > 0)
          this.allFriends.concat(player['friends'])
        }
        console.log(this.players);
      }else{
        this.global.handleAPIError(response.result);
      }
      this.global.hideLoader();
      this.loading = false;
    }).catch((err) =>{
      this.global.handleAPIError(err);
      this.global.hideLoader();
    });
  }

}
