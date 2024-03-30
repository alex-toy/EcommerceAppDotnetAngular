import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'skinet';
  products : any[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.http.get('https://localhost:5001/api/product').subscribe({
      next: (response:any) => this.products = response.items,
      error: response => console.log(response),
      complete: () => console.log(this.products)
    });
  }
}
