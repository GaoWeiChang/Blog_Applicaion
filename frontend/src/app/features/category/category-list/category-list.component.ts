import { Component, OnInit } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { Category } from '../models/category.model';
import { CategoryService } from '../services/category.service';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from 'src/app/shared/components/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.css']
})
export class CategoryListComponent implements OnInit {
  categories$?: Observable<Category[]>;

  deleteBlogPostSubscription?: Subscription;

  constructor(private categoryService: CategoryService,
    private dialog: MatDialog
  ) { }

  ngOnInit(): void {
    this.categories$ = this.categoryService.getAllCategories(undefined);
  }

  onSearch(query: string): void {
    this.categories$ = this.categoryService.getAllCategories(query);
  }

  onDelete(id: string): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      data: { message: 'Are you sure you want to delete this post?' }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.deleteBlogPostSubscription = this.categoryService.deleteCategory(id)
          .subscribe({
            next: (response) => {
              window.location.reload();
            }
          });
      }
    });
  }
}
