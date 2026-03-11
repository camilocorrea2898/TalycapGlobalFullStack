import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MatButtonModule, MatIconButton } from '@angular/material/button';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatTabsModule } from '@angular/material/tabs';
import { Movies } from './features/movies/movies';
import { MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { Weathers } from './features/weathers/weathers';

@Component({
  selector: 'app-root',
  imports: [
    Movies, 
    Weathers,
    RouterOutlet,
    MatButtonModule,
    MatPaginatorModule,
    MatTabsModule, 
    MatTableModule,
    MatCardModule,
    MatIconModule
  ],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('TalycapGlobal');
}
