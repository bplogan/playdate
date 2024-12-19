import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { GlobalService } from 'src/app/services/global.service';
import { ModalController } from '@ionic/angular';
import { Preferences } from '@capacitor/preferences';
import { pdPlayer } from 'src/app/classes/pdPlayer';


@Component({
  selector: 'app-friend-modal',
  templateUrl: './friend-modal.component.html',
  styleUrls: ['./friend-modal.component.scss'],
})
export class FriendModalComponent  implements OnInit {

   
  modalTitle:string = "";
  players: pdPlayer[] = [];
  selectedFriends:[] = [];
  lst: number[] = [];
  newFriendInput = {
    id: "",
    playerIds: this.lst,
    playerCode: "",
    statusId: 1
  }
  friendCode: string = "";
  constructor(
    private auth: AuthenticationService,
    private global: GlobalService,
    private modalControl: ModalController
  ) { }

  ngOnInit() {
    
  }
  confirmFriendModal(){
    if(this.friendCode != ""){
      this.auth.createOrUpdatePlayerFriend(this.newFriendInput).then((result) =>{
        this.global.presentToast("Friend request was sent successfully!", "bottom");
        return this.modalControl.dismiss(null, 'confirm');
      });
    }  
  }


  cancel(){
    return this.modalControl.dismiss(null, 'cancel');
  }

  async ionViewWillEnter(){
    this.global.showLoader();
    let userId = (await Preferences.get({key:'user-id'})).value || 0;  
    if(userId != null && userId != undefined && userId != "0" && userId != "-1"){
      this.auth.getPlayers({userId: +userId , playerId: -1, includeDetails: false}).then(response =>{
        this.players = response?.data?.result;
        console.log(this.players);
      }).finally(() =>{
        this.global.hideLoader();
      });
    }
  }

}
