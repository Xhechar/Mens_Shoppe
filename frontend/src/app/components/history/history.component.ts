import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { NavbarComponent } from '../navbar/navbar.component';

@Component({
  selector: 'app-history',
  standalone: true,
  imports: [CommonModule, RouterLink, NavbarComponent],
  templateUrl: './history.component.html',
  styleUrl: './history.component.css'
})
export class HistoryComponent {
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
