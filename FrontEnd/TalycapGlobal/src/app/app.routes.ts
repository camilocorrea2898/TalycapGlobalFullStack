import { Routes } from '@angular/router';
import { Movies } from './features/movies/movies';
import { Weathers } from './features/weathers/weathers';

export const routes: Routes = [
    { path: 'movies', component: Movies },
    { path: 'weathers', component: Weathers }
];
