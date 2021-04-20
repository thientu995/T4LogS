import { AppComponent } from './../app.component';
import { AfterViewInit, Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements AfterViewInit {
  isExpanded = false;
  app: AppComponent;
  constructor(app: AppComponent) {
    this.app = app;
  }

  ngAfterViewInit() {
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
