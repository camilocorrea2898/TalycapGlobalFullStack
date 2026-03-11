🌦️🎬 Angular -  Movies & Weather App

Este proyecto es una aplicación desarrollada en **Angular** que integra dos funcionalidades principales: un listado de películas populares usando la API de TMDB y la visualización del clima en ciudades de Colombia usando la API de OpenWeather. La interfaz está construida con **Angular Material** para un diseño moderno y responsivo.

## Tecnologías utilizadas
Angular 17+, Angular Material, RxJS, TMDB API, OpenWeather API.

     _                      _                 ____ _     ___
    / \   _ __   __ _ _   _| | __ _ _ __     / ___| |   |_ _|
   / △ \ | '_ \ / _` | | | | |/ _` | '__|   | |   | |    | |
  / ___ \| | | | (_| | |_| | | (_| | |      | |___| |___ | |
 /_/   \_\_| |_|\__, |\__,_|_|\__,_|_|       \____|_____|___|
                |___/


Angular CLI       : 21.2.1
Angular           : 21.2.2
Node.js           : 24.13.0
Package Manager   : npm 11.6.2
Operating System  : win32 x64

## Instalación
Clona el repositorio y entra en la carpeta del proyecto:
git clone https://github.com/tu-usuario/tu-repo.git
cd tu-repo

Instala las dependencias:
npm install

## Ejecución
Para correr el proyecto en modo desarrollo:
ng serve

## Luego abre en tu navegador:
http://localhost:4200/

## Configuración de APIs
- TMDB API: Regístrate en TMDB y obtén tu API Key. Configura la key en tu servicio MoviesService.
- OpenWeather API: Regístrate en OpenWeather y obtén tu API Key. Configura la key en tu servicio WeatherService.

## Estructura del proyecto
src/
 ├── app/
 │   ├── core/
 │   │   ├── services/   # Servicios para TMDB y OpenWeather
 │   │   └── models/     # Interfaces (MoviesResponse, WeatherResponse, GeoResponse)
 │   ├── components/
 │   │   ├── movies/     # Componente de películas
 │   │   └── weathers/   # Componente de clima
 │   └── app.component.ts
 ├── assets/
 └── index.html


## Funcionalidades
- Películas: listado con título, fecha, rating y póster.
- Clima: tabla con ciudad, temperatura, humedad, descripción e ícono.
- Selector de ciudades: para elegir ciudades de Colombia.
- Responsive design: tablas con scroll en móvil y tarjetas alternativas.

👨‍💻 Autor
Desarrollado por Cristian en Bogotá, Colombia.
