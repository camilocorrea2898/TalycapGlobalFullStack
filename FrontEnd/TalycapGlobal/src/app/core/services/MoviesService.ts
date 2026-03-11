import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { MoviesResponse } from '../models/MoviesResponse';

@Injectable({ providedIn: 'root' })
export class MoviesService {
  private apiUrl = 'https://api.themoviedb.org/3/movie/popular';
  private apiKey = '4568ff2438c17b540460400d9a663ecc';

  constructor(private http: HttpClient) {}

  getPopularMovies(page: number): Observable<MoviesResponse> {
    return this.http.get<MoviesResponse>(
      `${this.apiUrl}?api_key=${this.apiKey}&page=${page}`
    );
  }
}
