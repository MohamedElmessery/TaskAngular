import { Component } from '@angular/core';
import { InvoiceService } from '../services/invoiceService';
import { Item } from '../models/items.model';

@Component({
  selector: 'app-invoice',
  templateUrl: './invoice.component.html'
})
export class InvoiceComponent {
  
  invoiceItems;
  items: Item[] ;
  TotalAmount;
  InvoiceNumber;
  noData;
  Total;
  ItemUnitPrice = 0;
  AvailableQuantity;
  productId;
  InvoiceIms: any = [];
  Quntity;
  TotalCount = 0;
  AvailableQuantityErrorMs = "";
  SaveErrorMs = "";
  addMessage = "";
  invoiceId ;
  edited = false;
  editUnitPrice;
  editAvailableAmount;
  constructor(private invoiceService: InvoiceService) { }

  ngOnInit() {
    //get items and bind it in select box
    this.invoiceService.getProducts()
      .subscribe(
        response => {
          console.log(response);
          this.items = response;         
        },
        error => {
          console.log(error);
        });
  }
  //items value changed
  dataChanged(newObj) {
    this.productId = newObj.target.value;
    let Item = [];
    if (this.productId !== undefined) {
      Item = this.items.filter(a => a.productId == this.productId);
      if (Item[0] !== undefined) {
        this.ItemUnitPrice = Item[0].unitPrice;
        this.AvailableQuantity = Item[0].availableQuantity;
      }
      
    }
    
  }
  //show your invoice by its id number
  showInvice(event) {
    if (event.target.value.length !== 0) {
      this.invoiceService.getInvoiceById(event.target.value)
        .subscribe(
          response => {
            this.invoiceItems = response;
            if (response[0] !== undefined) {
              this.TotalAmount = "Total Amount " + ":" + response[0].totalAmount;
              this.InvoiceNumber = "Invoice Number " + ":" + response[0].invoiceId;
              this.noData = "";
            }
            if (response[0] === undefined) {
              this.noData = "There is no data";
              this.TotalAmount = "";
              this.InvoiceNumber = "";

            }
          },
          error => {
            console.log(error);
          });
    }
    else {
      this.noData = "There is no data";
      this.TotalAmount = "";
      this.InvoiceNumber = "";
      this.invoiceItems = [];
    }
  }
  // calc the total of one item
  CountTotal(event) {
    this.Quntity = Number(event.target.value);
    if (event.target.value > this.AvailableQuantity)
      this.AvailableQuantityErrorMs = "this quantity are more than available";
    else {
      this.Total = event.target.value * this.ItemUnitPrice;
      this.AvailableQuantityErrorMs = "";
    }
    
  }
  // add new item in InvoiceIms and calc the total amount of all items in array
  Add() {
     if (this.Total !== undefined && this.Quntity !== 0 && this.Quntity <= this.AvailableQuantity) {
       this.Total = this.Quntity * this.ItemUnitPrice;
       this.InvoiceIms.push({ productId: Number(this.productId), total: this.Total, quantity: this.Quntity })
       console.log(this.InvoiceIms);
       this.TotalCount = this.TotalCount + this.Total;
       this.addMessage = "item added , add onther item or save your invoice";
   }
    
  }
  // save invoice in data base
  save() {
    if (this.InvoiceIms.length !== 0) {
      this.invoiceService.addInvoice({ invoiceProducts: this.InvoiceIms, totalAmount: this.TotalCount })
        .subscribe(
          response => {
            this.invoiceItems = response;
            console.log(response);
            if (response[0] !== undefined) {
              this.TotalAmount = "Total Amount " + ":" + response[0].totalAmount;
              this.InvoiceNumber = "Invoice Number " + ":" + response[0].invoiceId;
              this.SaveErrorMs = "your invoice saved successfuly";
              setTimeout(() => this.SaveErrorMs = "", 1000);
              this.addMessage = "";
            }
          },
          error => {
            console.log(error);
          });
    }
    else
      this.SaveErrorMs = "please add items";
  }
  
  delete() {
    if (this.invoiceId != undefined) {
      this.invoiceService.deleteInvoice(this.invoiceId)
        .subscribe(
          response => {
            console.log(response);
            if (response === 1) {
              this.SaveErrorMs = "your invoice deleted successfuly";
              setTimeout(() => this.SaveErrorMs = "", 1000);
              this.addMessage = "";
            }
            else {
              this.SaveErrorMs = "your invoice not deleted ";
              setTimeout(() => this.SaveErrorMs = "", 2000);
              this.addMessage = "";
            }
            window.location.reload();
          },
          error => {
            console.log(error);
          });

    }
    else {
      this.SaveErrorMs = "please enter id";
      setTimeout(() => this.SaveErrorMs = "", 2000);
      this.addMessage = "";
    }
  }
  edit() {
    if (this.productId != undefined) {
      this.invoiceService.getItemById(this.productId)
        .subscribe(
          response => {
            console.log(response);
            this.editAvailableAmount = response[0].availableQuantity;
            this.editUnitPrice = response[0].unitPrice;
          },
          error => {
            console.log(error);
          });

      this.edited = true;
    }
    else {
      this.SaveErrorMs = "please select item  ";
      setTimeout(() => this.SaveErrorMs = "", 2000);
      this.addMessage = "";
    }
  }

  saveItem() {
    this.invoiceService.editItem({ ProductId: this.productId, UnitPrice: this.editUnitPrice, AvailableQuantity: this.editAvailableAmount })
      .subscribe(
        response => {          
          console.log(response);
          if (response == 1) {           
            this.SaveErrorMs = "item edited successfuly";
            setTimeout(() => this.SaveErrorMs = "", 1000);
            this.addMessage = "";
          }
        },
        error => {
          console.log(error);
        });
    window.location.reload();
    //this.edited = false;

  }
}

