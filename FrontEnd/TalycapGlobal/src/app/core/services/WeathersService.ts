import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { WeatherResponse } from '../models/WeatherResponse';
import { GeoResponse } from '../models/GeoResponse';

@Injectable({ providedIn: 'root' })
export class WeathersService {
  private apiUrlWeather = 'https://api.openweathermap.org/data/2.5/weather';
  private apiUrlcity = ' http://api.openweathermap.org/geo/1.0/direct';
  private apiKey = 'a4352a7068260658d656b0923e6f1327';

  constructor(private http: HttpClient) {}

  getWeathers(lat: string,lon: string): Observable<WeatherResponse> {
    return this.http.get<WeatherResponse>(
      `${this.apiUrlWeather}?lat=${lat}&lon=${lon}&appid=${this.apiKey}&units=metric`
    );
  }

  getCityCoordinates(city: string, country: string): Observable<GeoResponse[]> {
    return this.http.get<GeoResponse[]>(
      `${this.apiUrlcity}?q=${city},${country}&limit=1&appid=${this.apiKey}`
    );
  }

}
