import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IonInput, AlertController, LoadingController, NavController, ModalController } from '@ionic/angular';
import { Router, ActivatedRoute } from '@angular/router';
import { Preferences } from '@capacitor/preferences';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { pdPlayer } from 'src/app/classes/pdPlayer';


@Component({
  selector: 'app-player',
  templateUrl: './player.page.html',
  styleUrls: ['./player.page.scss'],
})
export class PlayerPage implements OnInit {

  player: pdPlayer = new pdPlayer();
  data: any;
  playerId: number = this.activatedRoute.snapshot.queryParams['userId'];
  loading: boolean = false;
  segment:string = "info";
  isPlayerModalOpen = false;
  modalTitle:string = "";
  constructor(    
    private alertController: AlertController,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private loadingController: LoadingController,
    private authService: AuthenticationService,
    private nav: NavController,
    private modalCtrl: ModalController) {
    
     
     
    }

  ngOnInit() {

  }

  setPlayerOpen(isOpen: boolean) {
    this.isPlayerModalOpen = isOpen;
  }

  confirmPlayerModal(){
    this.setPlayerOpen(false);
  }

  goAddFriend(){

  }

  async goAddAllergy(){
    this.setPlayerOpen(true);
  }

  goFriend(id:number){

  }

  savePlayer(){
   this.authService.createOrUpdatePlayer(this.player).then((result) =>{
    //
   });
  }

  goBack(){
    this.nav.back();
  }

  goUser(){
    this.router.navigate(['user']);
  }

  segmentChanged(event:any){
    //alert(event.detail.value);
  }

  async ionViewWillEnter(){
    if(this.playerId > 0){     
      var input = {id: this.playerId};
      this.authService.getPlayerDetails(input).then((result) =>{
       
        this.player = result.data['result']; 
        console.log(this.player);            
      });    
    }
  }
}
