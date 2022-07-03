import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { ImagesService } from '../services/images.service';
import { ProductsService } from '../services/products.service';
import Product from '../types/Product';

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.css'],
})
export class AddProductComponent implements OnInit, OnDestroy {
  porductForm = new FormGroup({
    name: new FormControl<string>('', Validators.required),
    image: new FormControl<string>('', Validators.required),
  });
  uploadingStatus = 'not chosen';
  subscriptions: Subscription[] = [];

  constructor(
    private imagesService: ImagesService,
    private productsService: ProductsService
  ) {}

  ngOnInit(): void {}

  uploadPhoto(target: EventTarget | null) {
    var fileInput = target as HTMLInputElement | null;
    if (!fileInput?.files) return;
    var file = fileInput.files[0];
    this.uploadingStatus = 'started uploading';
    var sub = this.imagesService.uploadImage(file).subscribe({
      next: (value) => {
        this.porductForm.patchValue({ image: value.url });
        this.uploadingStatus = 'uploading succeeded';
      },
      error: (err) => {
        this.uploadingStatus = 'uploading failed';
      },
    });

    this.subscriptions.push(sub);
  }

  submit() {
    if (!this.isReadyToSubmit()) return;
    var product = new Product(this.porductForm.value);
    this.productsService.addProduct(product).subscribe((value) => {
      console.log(value);
    });
  }

  isReadyToSubmit() {
    return (
      this.porductForm.valid && this.uploadingStatus === 'uploading succeeded'
    );
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach((sub) => sub.unsubscribe());
  }
}
