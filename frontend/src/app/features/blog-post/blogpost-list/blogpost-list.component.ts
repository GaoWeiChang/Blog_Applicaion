import { afterNextRender, Component, OnDestroy, OnInit } from '@angular/core';
import { Observable, Subscribable, Subscription } from 'rxjs';
import { BlogPost } from '../models/blogpost.model';
import { BlogpostsService } from '../services/blogposts.service';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from '../../confirm-dialog/confirm-dialog.component';


@Component({
  selector: 'app-blogpost-list',
  templateUrl: './blogpost-list.component.html',
  styleUrls: ['./blogpost-list.component.css']
})
export class BlogpostListComponent implements OnInit, OnDestroy {
  blogPosts$? : Observable<BlogPost[]>
  
  deleteBlogPostSubscription?: Subscription;

  constructor(private blogPostService : BlogpostsService,
              private dialog: MatDialog
  ){}

  ngOnInit(): void {
    this.blogPosts$ = this.blogPostService.getAllBlogPost();
  }

  onClickDelete(id: string): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      data: { message: 'Are you sure you want to delete this post?' }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result){
        this.deleteBlogPostSubscription = this.blogPostService.deleteBlogPost(id)
                                              .subscribe({
                                                next: (response) => {
                                                  window.location.reload();
                                                }
                                              });
      }
    });
  }

  ngOnDestroy(): void {
    this.deleteBlogPostSubscription?.unsubscribe()
  }
}
