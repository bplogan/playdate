import { Component } from '@angular/core';
import { Platform } from '@ionic/angular';
import { AuthenticationService } from './services/authentication.service';
import { GlobalService } from './services/global.service';
import { register } from 'swiper/element/bundle';

register();

@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
  styleUrls: ['app.component.scss'],
})
export class AppComponent {
  constructor(
    private platform: Platform, 
    private auth: AuthenticationService,
    private global: GlobalService
  ) {
    this.platform.ready().then(() =>{
      console.log("Platform ready...");
      this.global.compononentInit();
    });
  }
 
}
