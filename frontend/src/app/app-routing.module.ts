import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CategoryListComponent } from './features/category/category-list/category-list.component';
import { LucideAngularModule } from 'lucide-angular';
import { AddCategoryComponent } from './features/category/add-category/add-category.component';


const routes: Routes = [
  {
    path: 'admin/categories',
    component: CategoryListComponent
  },
  {
    path: 'admin/categories/add',
    component: AddCategoryComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes),
            LucideAngularModule
  ],
  exports: [RouterModule]
})

export class AppRoutingModule { }
