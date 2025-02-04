import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { CategoryService } from '../services/category.service';
import { Category, EditCategoryRequest } from '../models/category.model';

@Component({
  selector: 'app-edit-category',
  templateUrl: './edit-category.component.html',
  styleUrls: ['./edit-category.component.css']
})
export class EditCategoryComponent implements OnInit, OnDestroy {
  id : string | null = null;
  category?: Category;

  paramsSubcription?: Subscription;
  editSubscription?: Subscription;

  constructor(private route: ActivatedRoute,
              private router: Router,
              private categoryService: CategoryService
  ) {
  }

  ngOnInit(): void {
    this.paramsSubcription = this.route.paramMap.subscribe({
      next: (params) => {
        this.id = params.get('id');

        if(this.id){
          // get category by id
          this.categoryService.getCategoryById(this.id)
              .subscribe({
                next: (response) => {
                  this.category = response;
                }
              });
        }
      }
    });
  }

  onFormSubmit(): void {
    const editCategoryRequestModel: EditCategoryRequest = {
      name: this.category?.name?? '', // if (!this.category || !this.category.name) { return null; }
      urlHandle: this.category?.urlHandle?? ''
    };

    if(this.id){
      this.editSubscription = this.categoryService.editCategory(this.id, editCategoryRequestModel)
        .subscribe({
          next: (response) => {
            this.router.navigateByUrl('/admin/categories');
          }
        });
    }
  }

  ngOnDestroy(): void {
    this.paramsSubcription?.unsubscribe();
    this.editSubscription?.unsubscribe();
  }
}
