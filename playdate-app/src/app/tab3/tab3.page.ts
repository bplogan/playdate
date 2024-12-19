import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { NavController } from '@ionic/angular';

@Component({
  selector: 'app-tab3',
  templateUrl: 'tab3.page.html',
  styleUrls: ['tab3.page.scss']
})
export class Tab3Page {

  constructor(
    private nav : NavController,
    private router: Router
  ) {}

  goBack(){
    this.nav.back();
  }

  goUser(){
    this.router.navigate(['user']);
  }

}
