import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CategoryListComponent } from './features/category/category-list/category-list.component';
import { LucideAngularModule } from 'lucide-angular';


const routes: Routes = [
  {
    path: 'admin/categories',
    component: CategoryListComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes),
            LucideAngularModule
  ],
  exports: [RouterModule]
})

export class AppRoutingModule { }
