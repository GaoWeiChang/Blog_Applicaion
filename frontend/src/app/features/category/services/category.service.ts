import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AddCategoryRequest, Category } from '../models/category.model';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private http: HttpClient) { }

  getAllCategories(query?: string): Observable<Category[]>{
    let params = new HttpParams();
    
    if (query){
      params = params.set('query', query);
    }

    return this.http.get<Category[]>(`${environment.apiBaseUrl}/api/Categories`, {
      params : params
    });
  }

  addCategory(model: AddCategoryRequest): Observable<void>{
    return this.http.post<void>(`${environment.apiBaseUrl}/api/Categories`, model);
  }

  deleteCategory(id: string): Observable<Category>{
    return this.http.delete<Category>(`${environment.apiBaseUrl}/api/Categories/${id}`);
  }
}
