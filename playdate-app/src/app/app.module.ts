import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouteReuseStrategy } from '@angular/router';
import { IonicModule, IonicRouteStrategy } from '@ionic/angular';
import { importProvidersFrom } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { PlayerModalComponent } from './modals/player-modal/player-modal.component';
import { CommonModule } from '@angular/common';
import { FriendModalComponent } from './modals/friend-modal/friend-modal.component';
import { FormsModule } from '@angular/forms';

@NgModule({ 
  declarations: [AppComponent, PlayerModalComponent, FriendModalComponent],
  imports: [BrowserModule, IonicModule.forRoot(), AppRoutingModule, FormsModule],
  providers: [{ provide: RouteReuseStrategy, useClass: IonicRouteStrategy }],
  bootstrap: [AppComponent]

})
export class AppModule {}
