import { inject, Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, CanActivateFn, Router, RouterStateSnapshot } from '@angular/router';
import { AuthenticationService } from '../services/authentication.service';
import { NavController } from '@ionic/angular';
import { GlobalService } from '../services/global.service';

export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthenticationService);
  const router = inject(Router);
  const global = inject(GlobalService);


  var token = global.getToken().then(token =>{  
    if(token != null && token != "" ){    
      return true;
    }else{     
      return router.createUrlTree(['/login']);
    }
  });
  return token;
 
};

/* @Injectable({
  providedIn: 'root',
})

export class AuthGuard implements CanActivate {
  constructor(
    private authService: AuthenticationService,
    private navCtrl: NavController,
    private global: GlobalService
  ) {}

  async canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Promise<boolean> {
    return await this.checkAuth();
  }

  private async checkAuth() {
    const authed = await this.global.currentTokenValid();
    return authed || this.routeToLogin();
  }

  private routeToLogin(): boolean {
    this.navCtrl.navigateRoot('/login');
    return false;
  }
} */
