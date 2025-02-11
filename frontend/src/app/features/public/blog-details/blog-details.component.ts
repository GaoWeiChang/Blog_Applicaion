import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BlogpostsService } from '../../blog-post/services/blogposts.service';
import { BlogPost } from '../../blog-post/models/blogpost.model';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-blog-details',
  templateUrl: './blog-details.component.html',
  styleUrls: ['./blog-details.component.css']
})

export class BlogDetailsComponent implements OnInit , OnDestroy{
  url: string | null = null;
  blogpost?: BlogPost;

  GetBlogPostSubscription?: Subscription;

  constructor(private route: ActivatedRoute,
    private blogPostService: BlogpostsService
  ) { }

  ngOnInit(): void {
    this.route.paramMap
      .subscribe({
        next: (param) => {
          this.url = param.get('url');
        }
      });

    if (this.url) {
      this.GetBlogPostSubscription = this.blogPostService.getBlogPostByUrlHandle(this.url)
        .subscribe({
          next: (post) => {
            this.blogpost = post;
          }
        });
    }
  }

  ngOnDestroy(): void {
    this.GetBlogPostSubscription?.unsubscribe();
  }
}
