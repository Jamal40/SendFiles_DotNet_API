import { formatDate } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import UploadFileResponse from '../types/UploadImageResponse';

@Injectable({
  providedIn: 'root',
})
export class ImagesService {
  baseUrl = 'https://localhost:7264/api/Files/';
  constructor(private client: HttpClient) {}

  uploadImage(file: File): Observable<UploadFileResponse> {
    var form = new FormData();
    form.append('file', file);
    return this.client.post<UploadFileResponse>(this.baseUrl, form);
  }
}
