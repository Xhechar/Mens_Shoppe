import { Routes } from '@angular/router';
import { FavouritesComponent } from './components/favourites/favourites.component';
import { LandingComponent } from './components/landing/landing.component';
import { ShopComponent } from './components/shop/shop.component';
import { LoginComponent } from './components/login/login.component';
import { SignupComponent } from './components/signup/signup.component';
import { ProductComponent } from './components/product/product.component';
import { CartComponent } from './components/cart/cart.component';
import { HistoryComponent } from './components/history/history.component';
import { AdminframeComponent } from './components/adminframe/adminframe.component';
import { ReviewFormComponent } from './components/review/review.component';

export const routes: Routes = [
  { path: '', component: LandingComponent },
  { path: 'shop', component: ShopComponent },
  { path: 'login', component: LoginComponent },
  { path: 'signup', component: SignupComponent },
  { path: 'product', component: ProductComponent },
  { path: 'cart', component: CartComponent },
  { path: 'history', component: HistoryComponent },
  { path: 'favourites', component: FavouritesComponent },
  {path: 'admin', component: AdminframeComponent},
  { path: 'product/:id', component: ProductComponent },
  {path: 'review', component: ReviewFormComponent},

];
