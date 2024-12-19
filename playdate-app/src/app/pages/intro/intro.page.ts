import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IonInput, IonicSlides, AlertController, LoadingController } from '@ionic/angular';
import { Router } from '@angular/router';
import { Preferences } from '@capacitor/preferences';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { pdPlayer } from 'src/app/classes/pdPlayer';
import { TimeInterval } from 'rxjs';
import { GlobalService } from 'src/app/services/global.service';

@Component({
  selector: 'app-intro',
  templateUrl: './intro.page.html',
  styleUrls: ['./intro.page.scss'],
})
export class IntroPage implements OnInit {
  private swiperInstance: any;
  swiperModules = [IonicSlides];
  
  @ViewChild('swiper')
  set swiper(swiperRef: ElementRef) {
    /**
     * This setTimeout waits for Ionic's async initialization to complete.
     * Otherwise, an outdated swiper reference will be used.
     */
    setTimeout(() => {
      this.swiperInstance = swiperRef.nativeElement.swiper;
    }, 100);
  }


  @ViewChild('txtPlayerCount') playerCountInput!: IonInput;
  
  playerCount: number = 1;
  loading: boolean = false;
  newPlayers: pdPlayer[] = [];
  updateCount: number = 0;
  playerInterval: any;

  constructor(
    private alertController: AlertController,
    private router: Router,
    private loadingController: LoadingController,
    private authService: AuthenticationService,
    private global: GlobalService
  ) {}

  async ngOnInit() {
    this.loading = true;
    let uid = (await Preferences.get({key: 'user-id'})).value || -1;
    
    this.authService.getPlayers({ userId: +uid, playerId: -1, includeDetails: false}).then(response =>{
      console.log("PLAYERS");
      console.log(response);  
      if(response.data['success'] == true){
        if(response.data["result"].length > 0){
          //alert("going to tabs");
          this.router.navigateByUrl('/tabs');
        }
      }      
      this.loading = false;
    });
  }

  next() {
    if(this.playerCount > 0){
      this.generateForm();    
      this.swiperInstance?.slideNext();    
    }
  }

  generateForm(){
    this.newPlayers = [];
    for(let i = 0; i < this.playerCount; i++){
      let player = new pdPlayer();     
      this.newPlayers.push(player);
    }
  }

  playerCountClicked(nativeEl: any){
    nativeEl.target.autofocus=true;
    nativeEl.target.select();
  }


  startPlayerInterval(){
    console.log("starting interval");
    this.playerInterval = setInterval(() =>{
      this.checkPlayerCounts();
    }, 1000);
  }

  stopPlayerInterval(){
    this.global.hideLoader();
    console.log("stopping interval");
    clearInterval(this.playerInterval);
  }

  checkPlayerCounts(){
    console.log("checking...");
    if(this.updateCount >= this.newPlayers.length){
      this.stopPlayerInterval();
      this.router.navigateByUrl('/tabs');
    }else{
      console.log("not yet");
    }
  }

  async start() {
    if(this.playerCount > 0){
      const canGO = this.newPlayers.find((player) => player.fullName.trim() != '');
      if(canGO){
        this.startPlayerInterval();

        this.global.showLoader();
        //add the players to the user
        for (let player of this.newPlayers) {
          this.authService.createOrUpdatePlayer(player).then((response) =>{           
              this.updateCount += 1;                 
          }).catch(() =>{
            this.updateCount += 1;     
          });
        }//end ff
      }else{
        alert("nar");
      }
    }
  }

  ionViewDidEnter() {
    setTimeout(() =>{
      this.playerCountInput.setFocus();      
    }, 100);
    
  }

  
}
