import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';



@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginFormGroup: FormGroup
  constructor(private fb: FormBuilder) { }

  ngOnInit(): void {
    this.loginFormGroup = this.fb.group({
      account: ['']
    });
  }



}
