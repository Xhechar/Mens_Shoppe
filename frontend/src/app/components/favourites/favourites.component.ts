import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { NavbarComponent } from '../navbar/navbar.component';

@Component({
  selector: 'app-favourites',
  standalone: true,
  imports: [CommonModule, NavbarComponent],
  templateUrl: './favourites.component.html',
  styleUrl: './favourites.component.css'
})
export class FavouritesComponent {
  products = [
    {
    id: 1,
    name: "name",
    quantity: 1,
    size: "large",
    image: "/assets/imgs/men-s-casual-t-shirshort-sleeve-o-neck-t-shirt-lestyleparfait-kenya-t-shirts-1.webp"
  },
    {
    id: 1,
    name: "name",
    quantity: 1,
    size: "large",
    image: "/assets/imgs/men-s-casual-t-shirshort-sleeve-o-neck-t-shirt-lestyleparfait-kenya-t-shirts-1.webp"
  },
    {
    id: 1,
    name: "name",
    quantity: 1,
    size: "large",
    image: "/assets/imgs/men-s-casual-t-shirshort-sleeve-o-neck-t-shirt-lestyleparfait-kenya-t-shirts-1.webp"
  },
    {
    id: 1,
    name: "name",
    quantity: 1,
    size: "large",
    image: "/assets/imgs/men-s-casual-t-shirshort-sleeve-o-neck-t-shirt-lestyleparfait-kenya-t-shirts-1.webp"
  },
    {
    id: 1,
    name: "name",
    quantity: 1,
    size: "large",
    image: "/assets/imgs/men-s-casual-t-shirshort-sleeve-o-neck-t-shirt-lestyleparfait-kenya-t-shirts-1.webp"
  },
    {
    id: 1,
    name: "name",
    quantity: 1,
    size: "large",
    image: "/assets/imgs/men-s-casual-t-shirshort-sleeve-o-neck-t-shirt-lestyleparfait-kenya-t-shirts-1.webp"
  },
    {
    id: 1,
    name: "name",
    quantity: 1,
    size: "large",
    image: "/assets/imgs/men-s-casual-t-shirshort-sleeve-o-neck-t-shirt-lestyleparfait-kenya-t-shirts-1.webp"
  },
]
}
