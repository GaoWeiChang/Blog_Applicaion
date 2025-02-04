import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Category } from '../models/category.model';
import { CategoryService } from '../services/category.service';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.css']
})
export class CategoryListComponent implements OnInit {
  categories$?: Observable<Category[]>;

  constructor(private categoryService: CategoryService) {
    
  }
  
  ngOnInit(): void {
    this.categories$ = this.categoryService.getAllCategories(undefined);
  }

  onSearch(query: string): void {
    this.categories$ = this.categoryService.getAllCategories(query);
  }

  onDelete(id: string): void{
    if (id) {
      this.categoryService.deleteCategory(id)
          .subscribe({
            next: (response) => {
              window.location.reload();
            }
          });
    }
  }
}
