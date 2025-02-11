import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { BlogPost } from '../../blog-post/models/blogpost.model';
import { BlogpostsService } from '../../blog-post/services/blogposts.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  blogposts$?: Observable<BlogPost[]>

  constructor(private blogPostService: BlogpostsService) {
  }

  ngOnInit(): void {
    this.blogposts$ = this.blogPostService.getAllBlogPost();
  }

}
