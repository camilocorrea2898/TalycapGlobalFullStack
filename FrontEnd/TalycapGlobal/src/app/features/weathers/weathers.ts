import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { WeatherResponse } from '../../core/models/WeatherResponse';
import { WeathersService } from '../../core/services/WeathersService';
import { GeoResponse } from '../../core/models/GeoResponse';
import { MatSelectModule } from '@angular/material/select';
import { MatOptionModule } from '@angular/material/core';

@Component({
  selector: 'app-weathers',
  imports: [
    MatTableModule,
    MatPaginatorModule,
    MatFormFieldModule,
    MatInputModule,
    MatCardModule,
    CommonModule,
    MatSelectModule,
    MatOptionModule,
  ],
  templateUrl: './weathers.html',
  styleUrl: './weathers.css',
})
export class Weathers implements OnInit {
  displayedColumns: string[] = ['city', 'temp', 'humidity', 'description', 'icon'];
    colombianCities = [
    { name: 'Bogotá'},
    { name: 'Medellín'},
    { name: 'Cali'},
    { name: 'Barranquilla'},
    { name: 'Cartagena'}
  ];

  dataSource = new MatTableDataSource<any>();

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(private weatherService: WeathersService) {}

  ngOnInit() {
  }

  loadWeather(lat: string, lon: string) {
    this.weatherService.getWeathers(lat, lon).subscribe((res: WeatherResponse) => {
      console.log('res:', res);
      const row = {
        city: res.name,
        temp: res.main.temp,
        humidity: res.main.humidity,
        description: res.weather[0].description,
        icon: res.weather[0].icon
      };
      this.dataSource.data = [row];
      this.dataSource.paginator = this.paginator;
    });
  }

  onCitySelected(event: any) {
    const city = event.name;
    this.weatherService.getCityCoordinates(city, 'CO').subscribe((res: GeoResponse[]) => {
      const city = res[0];
      this.loadWeather(`${city.lat}`,`${city.lon}`);
    });
  }
}
