import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MoviesService } from '../../core/services/MoviesService';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MoviesResponse } from '../../core/models/MoviesResponse';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';


@Component({
  selector: 'app-movies',
  imports: [
      MatTableModule,
      MatPaginatorModule,
      MatFormFieldModule,
      MatInputModule,
      MatCardModule,
      CommonModule
  ],
  templateUrl: './movies.html',
  styleUrl: './movies.css',
})
export class Movies implements OnInit {
  displayedColumns: string[] = ['title', 'release_date', 'vote_average', 'poster'];
  dataSource = new MatTableDataSource<any>();

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(private moviesService: MoviesService) {}

  ngOnInit() {
    this.loadMovies(1);
  }

  loadMovies(page: number) {
    this.moviesService.getPopularMovies(page).subscribe((res: MoviesResponse) => {
      console.log(res.results);
      this.dataSource.data = res.results;
      this.dataSource.paginator = this.paginator;
    });
  }


  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

}
