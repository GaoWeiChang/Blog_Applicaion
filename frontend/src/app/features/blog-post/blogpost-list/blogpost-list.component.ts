import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { BlogPost } from '../models/blogpost.model';
import { BlogpostsService } from '../services/blogposts.service';

@Component({
  selector: 'app-blogpost-list',
  templateUrl: './blogpost-list.component.html',
  styleUrls: ['./blogpost-list.component.css']
})
export class BlogpostListComponent implements OnInit {
  blogPosts$? : Observable<BlogPost[]>
  
  constructor(private blogPostService : BlogpostsService){
  }

  ngOnInit(): void {
    this.blogPosts$ = this.blogPostService.getAllBlogPost();
  }

}
