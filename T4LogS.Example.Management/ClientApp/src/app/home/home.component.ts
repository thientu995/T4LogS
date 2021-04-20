import { AfterViewInit, Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AppComponent } from '../app.component';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements AfterViewInit {
  app: AppComponent;
  dataObj;
  dataObjDate;
  dataObjData;
  dataObjDataContent;
  selectObj;

  constructor(app: AppComponent, private route: ActivatedRoute) {
    this.app = app;
    this.dataObj = this.app.decodeFormatRoute(this.route.snapshot.paramMap.get('id'));
    this.app.getData(this.dataObj, true).subscribe(result => {
      this.dataObjDate = result;
    }, error => console.error(error));
  }

  getItemData(obj) {
    this.selectObj = obj;
    this.app.getData(this.selectObj, false).subscribe(result => {
      this.dataObjData = result;
    }, error => console.error(error));
  }

  getTypes() {
    return this.dataObjData.filter(x => x.Parent == this.selectObj.Location);
  }

  getFiles(itemType) {
    return this.dataObjData.filter(x => x.Parent == itemType.Location);
  }

  getContent(obj) {
    this.app.getData(obj, false).subscribe(result => {
      this.dataObjDataContent = result;
    }, error => console.error(error));
  }

  ngAfterViewInit() { }
}
