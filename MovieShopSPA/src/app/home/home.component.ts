import { Component, OnInit } from '@angular/core';
import { MovieService } from '../core/services/movie.service';
import { MovieCard } from '../shared/models/movieCard';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  //data that I need to expose to the template/view
  movieCards!: MovieCard[];
  constructor(private movieService: MovieService) { }

  ngOnInit(): void {
    console.log('inside Home Component Init Method')
    //one of the most important life cycle hooks method in angular
    //we use this method to make any API calls and initialize the data objects
    this.movieService.getTopGrossingMovies().subscribe(m=>{
      this.movieCards=m;
      console.log('inside the subscribtion');
      console.log(this.movieCards);
    });
  }

}
