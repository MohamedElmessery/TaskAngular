import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Item } from '../models/items.model';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
  })
};

@Injectable({
  providedIn: 'root'
})
    // calling of api

export class InvoiceService {

  constructor(private http: HttpClient) { }

  public getProducts(): Observable<Item[]>{
    return this.http.get<Item[]>('api/Invoice/GetProducts', httpOptions);
  }
  public getInvoiceById(id) {
    return this.http.get(`api/Invoice/GetInvoiceById/${id}`);
  }
  public addInvoice(data) {
    return this.http.post('api/Invoice/AddInvoice', data);
  }
  public deleteInvoice(id) {
    return this.http.delete(`api/Invoice/DeleteInvoice/${id}`);
  }
  public editItem(data) {
    return this.http.put('api/Invoice/EditProduct', data);
  }
  public getItemById(id) {
    return this.http.get(`api/Invoice/GeProductById/${id}`);
  }
}
