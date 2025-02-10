import { Component, OnDestroy, OnInit } from '@angular/core';
import { BlogPost, UpdateBlogPost } from '../models/blogpost.model';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { BlogpostsService } from '../services/blogposts.service';
import { Observable, Subscription } from 'rxjs';
import { Category } from '../../category/models/category.model';
import { CategoryService } from '../../category/services/category.service';
import { ImageService } from 'src/app/shared/components/image-selector/services/image.service';
import { ImageSelectorComponent } from 'src/app/shared/components/image-selector/image-selector.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-edit-blogpost',
  templateUrl: './edit-blogpost.component.html',
  styleUrls: ['./edit-blogpost.component.css']
})
export class EditBlogpostComponent implements OnInit, OnDestroy{

  id: string | null = null;
  model?: BlogPost;
  categories$?: Observable<Category[]>
  selectedCategories?: string[]

  routeSubscription?: Subscription;
  updateBlogPostSubscription?: Subscription;
  getBlogPostSubscription?: Subscription;
  deleteBlogPostSubscription?: Subscription;
  imageSelectSubscription?: Subscription;

  constructor(private route: ActivatedRoute,
    private router: Router,
    private dialog: MatDialog,
    private blogPostService: BlogpostsService,
    private categoryService: CategoryService,
    private imageService: ImageService) {

  }

  ngOnInit(): void {
    this.categories$ = this.categoryService.getAllCategories();

    this.routeSubscription = this.route.paramMap.subscribe({
      next: (params) => {
        this.id = params.get('id');
        
        // Get blogpost from API
        if (this.id) {
          this.getBlogPostSubscription = this.blogPostService.getBlogPostById(this.id)
            .subscribe({
              next: (response) => {
                this.model = response;
                this.selectedCategories = response.categories.map(x => x.id)
              }
            });
        }

        this.imageSelectSubscription = this.imageService.onSelectImage()
        .subscribe({
          next: (response) => {
            if(this.model){
              this.model.featuredImageUrl = response.url;
            }
          }
        });

      }
    });
  }

  onFormSubmit(): void {
    // convert model to request object
    if (this.model && this.id) {
      var updateBlogPost: UpdateBlogPost = {
        title: this.model.title,
        shortDescription: this.model.shortDescription,
        content: this.model.content,
        featuredImageUrl: this.model.featuredImageUrl,
        urlHandle: this.model.urlHandle,
        author: this.model.author,
        publishDate: this.model.publishedDate,
        isVisible: this.model.isVisible,
        categories: this.selectedCategories ?? [] // if empty return []
      };

      this.updateBlogPostSubscription = this.blogPostService.updateBlogPostbyId(this.id, updateBlogPost)
        .subscribe({
          next: (response) => {
            this.router.navigateByUrl('/admin/blogposts');
          }
        })
    }
  }

  onDelete(): void {
    if (this.id) {
      // call service for delete blog Post
      this.blogPostService.deleteBlogPost(this.id)
        .subscribe({
          next: (response) => {
            this.router.navigateByUrl('/admin/blogposts');
          }
        });
    }
  }

  openImageSelector(): void {
    const dialogRef = this.dialog.open(ImageSelectorComponent, {
        width: '60%', // or 'px'
        height: '55%',
        maxWidth: '1200px',
        maxHeight: '900px'
      });
  }

  ngOnDestroy(): void {
    this.routeSubscription?.unsubscribe();
    this.updateBlogPostSubscription?.unsubscribe();
    this.getBlogPostSubscription?.unsubscribe();
    this.deleteBlogPostSubscription?.unsubscribe();
    this.imageSelectSubscription?.unsubscribe();
  }
}
