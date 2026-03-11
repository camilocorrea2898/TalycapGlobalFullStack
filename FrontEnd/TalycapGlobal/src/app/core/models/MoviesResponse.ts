export interface Movie {
  id: number;
  title: string;
  overview: string;
  release_date: Date;
  vote_average: number;
  poster_path: string;
  backdrop_path: string;
  popularity: number;
}

export interface MoviesResponse {
  page: number;
  results: Movie[];
  total_pages: number;
  total_results: number;
}
