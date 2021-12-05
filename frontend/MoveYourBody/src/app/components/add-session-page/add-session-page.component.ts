import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-add-session-page',
  templateUrl: './add-session-page.component.html',
  styleUrls: ['./add-session-page.component.css']
})
export class AddSessionPageComponent implements OnInit {
  mobile: boolean = false;
  constructor() { }

  ngOnInit(): void {
    if (window.innerWidth <= 800) {
      this.mobile = true;
    }
    window.onresize = () => (this.mobile = window.innerWidth <= 991);
  }

}
