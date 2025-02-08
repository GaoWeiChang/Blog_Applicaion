import { Component, NgModule, OnDestroy, OnInit } from '@angular/core';
import { MarkdownModule } from 'ngx-markdown';
import { AddBlogPost } from '../models/blogpost.model';
import { BlogpostsService } from '../services/blogposts.service';
import { Observable, Subscribable, Subscription } from 'rxjs';
import { Category } from '../../category/models/category.model';
import { Router } from '@angular/router';
import { CategoryService } from '../../category/services/category.service';

@Component({
  selector: 'app-add-blogpost',
  templateUrl: './add-blogpost.component.html',
  styleUrls: ['./add-blogpost.component.css']
})

export class AddBlogpostComponent implements OnInit, OnDestroy{
  model: AddBlogPost;
  categories$?: Observable<Category[]>;

  private addBlogPostSubscription?: Subscription;

  constructor(private blogPostService: BlogpostsService,
              private categoryService: CategoryService,
              private router: Router) {
    this.model = {
      title: '',
      shortDescription: '',
      content: '',
      featuredImageUrl: '',
      urlHandle: '',
      author: '',
      publishedDate: new Date(),
      isVisible: true,
      categories: []
    }
  }

  ngOnInit(): void {
    this.categories$ = this.categoryService.getAllCategories();
  }

  onSubmit(): void {
    console.log(this.model);
    this.addBlogPostSubscription = this.blogPostService.addBlogPost(this.model)
                                    .subscribe({
                                      next: (response) => {
                                        this.router.navigateByUrl('/admin/blogposts');
                                      }
                                    })
  }

  ngOnDestroy(): void {
    this.addBlogPostSubscription?.unsubscribe();
  }
}

