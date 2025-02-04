import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { UsersService } from '../../services/users.service';

interface Client {
  profileUrl: string;
  name: string;
  email: string;
  phoneNumber: string;
  numberOfOrders: number;
  registrationDate: string;
}

@Component({
  selector: 'app-clients',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './clients.component.html',
  styleUrl: './clients.component.css'
})
export class ClientsComponent implements OnInit {
  selectedClient: Client | null = null;
  clients: Client[] = [];

  constructor(private userService : UsersService) {}

  ngOnInit() {
    this.loadClients();
  }

  loadClients() {
    this.userService.getUsers().subscribe({
      next: (clients) => {
        // this.clients = clients;
      },
      error: (error) => {
        console.error('Error fetching clients:', error);
      }
    });
  }

  showPopup(client: Client) {
    this.selectedClient = client;
  }
}
