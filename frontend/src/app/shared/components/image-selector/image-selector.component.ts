import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Observable, Subscription } from 'rxjs';
import { BlogImage } from '../models/blog-image.model';
import { ImageService } from './services/image.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-image-selector',
  templateUrl: './image-selector.component.html',
  styleUrls: ['./image-selector.component.css']
})

export class ImageSelectorComponent implements OnInit {
  private file?: File;
  fileName: string = '';
  title: string = '';
  images$?: Observable<BlogImage[]>;

  @ViewChild('form', { static: false }) imageUploadForm?: NgForm; // จัดการ form และเข้าถึง properties/methods ของ form นั้นใน component

  constructor(public dialogRef: MatDialogRef<ImageSelectorComponent>,
    private imageService: ImageService) {
  }

  ngOnInit(): void {
    this.getImages();
  }

  onFileUploadChange(event: Event): void {
    const element = event.currentTarget as HTMLInputElement; // input element ทำให้เข้าถึง properties พิเศษของ input ได้ เช่น files, value, type
    this.file = element.files?.[0]; // pick current file
  }

  uploadImage(): void {
    if ((this.file) && (this.fileName !== '') && (this.title !== '')) {
      this.imageService.uploadImage(this.file, this.fileName, this.title)
        .subscribe({
          next: (response) => {
            this.imageUploadForm?.resetForm();
            this.getImages();
          }
        });
    }
  }

  selectImage(image: BlogImage): void {
    this.imageService.selectImage(image);
    this.dialogRef.close(image); // Close the dialog and return the selected image
  }

  private getImages() {
    this.images$ = this.imageService.getAllImages();
  }
}
