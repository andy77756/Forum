import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

  @Output()
  search = new EventEmitter<string>();

  text = "";
  constructor() { }

  ngOnInit(): void {
  }

  sendSearch(){
    this.search.emit(this.text);
  }
}
