import { Component, Input, Output, OnInit, EventEmitter, OnDestroy, ElementRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Product } from '../../interfaces/product';
import { ProductService } from '../../services/product.service';
import Dropzone from 'dropzone';
import { FileToUmageUrlPipe } from '../../pipes/file-to-umage-url.pipe';

@Component({
  selector: 'app-form',
  standalone: true,
  imports: [CommonModule, FormsModule, FileToUmageUrlPipe],
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.css']
})
export class FormComponent implements OnInit, OnDestroy {
  @Input() product: Product | null = null;
  @Output() cancelEdit = new EventEmitter<void>();

  dropzone!: Dropzone;
  formMode: 'create' | 'update' = 'create';
  name: string = '';
  description: string = '';
  category: string = '';
  quantity: number = 0;
  price: number = 0;
  size: string = '';
  images: string[] = [
    "https://i.ibb.co/kQNZHsQ/3d-color-sweatshirt-for-men-lestyleparfait-kenya-sweatshirt-1.webp"
  ];
  files: File[] = [];
  stockLimit: number = 0;
  selectedFiles: File[] = [];

  constructor(private elementRef: ElementRef, private productService: ProductService) {}

  ngOnDestroy(): void {
    // Clean up
  }

  ngOnInit(): void {
    this.initializeDropzone();
    this.loadProductData();
  }

  private initializeDropzone(): void {
    this.dropzone = new Dropzone(this.elementRef.nativeElement.querySelector('.dropzone'), {
      url: '/your-upload-endpoint',
      autoProcessQueue: false,
      addRemoveLinks: true
    });

    this.dropzone.on('addedfile', (file: File) => {
      this.files.push(file);
      this.readFile(file);
    });

    this.dropzone.on('removedfile', (file: File) => {
      this.files.splice(this.files.indexOf(file), 1);
      const index = this.images.indexOf(file.name);
      if (index !== -1) {
        this.images.splice(index, 1);
      }
    });
  }

  private readFile(file: File): void {
    const reader = new FileReader();
    reader.onload = (e: any) => {
      this.images.push(e.target.result);
    };
    reader.readAsDataURL(file);
  }

  private loadProductData(): void {
    if (this.product) {
      this.formMode = 'update';
      this.name = this.product.name;
      this.description = this.product.description;
      this.category = this.product.type;
      this.quantity = this.product.quantity;
      this.price = this.product.price;
      this.size = this.product.size;
      this.images = this.product.images;
    }
  }

  onSelect(event: any): void {
    this.files.push(...event.addedFiles);
  }

  onRemove(event: File): void {
    this.files.splice(this.files.indexOf(event), 1);
  }

  onFileSelected(event: Event): void {
    const target = event.target as HTMLInputElement;
    if (target && target.files) {
      const files = target.files;
      for (let i = 0; i < files.length; i++) {
        this.files.push(files[i]);
        this.readFile(files[i]);
      }
      this.selectedFiles = Array.from(files);
    }
  }

  onSubmit(): void {
    this.uploadFiles(this.files).then((uploadedFileUrls) => {
      const productData: Product = {
        id: this.product ? this.product.id : '',
        name: this.name,
        description: this.description,
        type: this.category,
        quantity: this.quantity,
        price: this.price,
        size: this.size,
        images: [...this.images, ...uploadedFileUrls],
        stockLimit: this.stockLimit
      };
      const formData = new FormData();
      for (let i = 0; i < this.selectedFiles.length; i++) {
        formData.append('images', this.selectedFiles[i], this.selectedFiles[i].name);
      }

      if (this.formMode === 'create') {
        this.productService.createProduct(productData).subscribe({
          next: (response) => console.log('Product created', response),
          error: (error) => console.error('Error creating product', error),
        });
      } else {
        this.productService.updateProduct(productData).subscribe({
          next: (response) => console.log('Product updated', response),
          error: (error) => console.error('Error updating product', error),
        });
      }

      console.log('Form submitted with data:', productData);
    }).catch((error) => {
      console.error('Error uploading files', error);
    });
  }

  async uploadFiles(files: File[]): Promise<string[]> {
    const urls: string[] = [];
    // Logic to upload files and fill `urls` with their access URLs
    return urls;
  }

  onCancel(): void {
    this.cancelEdit.emit();
  }
}
