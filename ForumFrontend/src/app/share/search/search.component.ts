import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { faCaretDown } from '@fortawesome/free-solid-svg-icons';
import { $ } from 'protractor';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

  caretDown = faCaretDown;

  @Output()
  search = new EventEmitter<any>();

  @Input()
  text = "";

  selected = "keyTopic";

  constructor() { }

  ngOnInit(): void {

  }

  sendSearch(){
    this.search.emit({ key: this.text, searchField: this.selected});
  }
}
