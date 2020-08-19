import { Component, OnInit } from '@angular/core';
import { Employee } from '../model/employee';
import { UserService } from '../shared/user.service';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { MatTableDataSource } from '@angular/material/table';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
 
export class HomeComponent implements OnInit {
  data?: Employee[];
  isLoginError : boolean = false;
  displayedColumns: string[] = ["Employee_Id","Full_Name","Branch","LocalSalary","PATE_Tax_Amount","Net_Pay_Amount"];
  CountryFilter = new FormControl();
  First_NameFilter = new FormControl();
  Last_NameFilter = new FormControl();
  filteredValues? = { Branch:'', FirstName:'', LastName:'' };
  
  
  
  constructor(private userService : UserService,private router : Router) { }
  listData?: MatTableDataSource<Employee>;
  
  
  ngOnInit() {
    this.getdata();
    
    this.CountryFilter.valueChanges.subscribe((CountryFilter)        => {
      this.filteredValues['Branch'] = CountryFilter;
      this.listData.filter = JSON.stringify(this.filteredValues);
      });
  
      this.First_NameFilter.valueChanges.subscribe((First_NameFilter) => {
        this.filteredValues['FirstName'] = First_NameFilter;
        this.listData.filter = JSON.stringify(this.filteredValues);
      });
  
      this.Last_NameFilter.valueChanges.subscribe((Last_NameFilter) => {
        this.filteredValues['LastName'] = Last_NameFilter;
        this.listData.filter = JSON.stringify(this.filteredValues);
      });
  
    this.listData.filterPredicate = this.customFilterPredicate();
  }
  getdata() {
    this.userService.getEmployeeDetails().subscribe(
      (response: Employee[] ) => {
        this.data = response;
        this.userService.employee = response;
        this.listData = new MatTableDataSource(this.data);
        console.log(this.listData);
      },
      (err : HttpErrorResponse)=>{
        this.isLoginError = true;
      });
  }
  Logout() {
    localStorage.removeItem('userToken');
    this.router.navigate(['/login']);
  }
  customFilterPredicate() {
    const myFilterPredicate = function(data:Employee, filter:string)
    :boolean {
      let searchString = JSON.parse(filter);
      let branchFound = data.Branch.toString().trim().toLowerCase().indexOf(searchString.Branch.toLowerCase()) !== -1
      let lnameFound = data.FirstName.toString().trim().toLowerCase().indexOf(searchString.FirstName.toLowerCase()) !== -1
      let fnameFound = data.LastName.toString().trim().toLowerCase().indexOf(searchString.LastName.toLowerCase()) !== -1
      if (searchString.topFilter) {
          return  branchFound || fnameFound || lnameFound
      } else {
          return branchFound && fnameFound && lnameFound
      }
    }
    return myFilterPredicate;
  }
  
}
