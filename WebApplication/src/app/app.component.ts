import { Component, inject, OnInit } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { RouterOutlet } from '@angular/router';
import { Observable } from 'rxjs';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

interface Employee {
  id?: string;
  firstName: string;
  lastName?: string;
  email: string;
  cpf?: string;
  dateOfBirth?: Date;
  address?: string;
  currentlyEmployed?: boolean;
}

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CommonModule, FormsModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})

export class AppComponent implements OnInit {
  title = 'Web Application';
  http = inject(HttpClient);
  urlApi = 'https://localhost:7278/employee';

  employees$: Observable<Employee[]> | undefined;
  newEmployee: Partial<Employee> = { };
  employeeToUpdate: Employee | undefined;
  filter: Partial<Employee> = {};

  ngOnInit(): void {
    this.getEmployees();
  }

  getEmployees(): void {
    this.employees$ = this.http.get<Employee[]>(this.urlApi);
    console.log(this.employees$);
  }

  filterEmployees(): void {
    console.log("Chegou no filterEmployees");
    console.log("filter firstname: ", this.filter.firstName);
    let params = new HttpParams();
    if (this.filter.firstName) {
      params = params.set('firstName', this.filter.firstName);
    }
    if (this.filter.lastName) {
      params = params.set('lastName', this.filter.lastName);
    }
    if (this.filter.email) {
      params = params.set('email', this.filter.email);
    }
    if (this.filter.cpf) {
      params = params.set('cpf', this.filter.cpf);
    }
    this.employees$ = this.http.get<Employee[]>(this.urlApi, { params });
    this.employees$.subscribe();
  }

  setEmployeeToUpdate(employee: Employee): void {
    this.employeeToUpdate = { ...employee }; // Cria cópia do objeto
  }

  updateEmployee(): void {
    if (this.employeeToUpdate){
      const employeeToSave = {
        firstName: this.employeeToUpdate.firstName,
        lastName: this.employeeToUpdate.lastName,
        email: this.employeeToUpdate.email,
        cpf: this.employeeToUpdate.cpf,
        dateOfBirth: this.employeeToUpdate.dateOfBirth,
        address: this.employeeToUpdate.address,
        currentlyEmployed: this.employeeToUpdate.currentlyEmployed
      };

      this.http.put(`${this.urlApi}`, employeeToSave).subscribe(() => {
        this.getEmployees(); // Atualiza a lista de funcionários
        this.employeeToUpdate = undefined; // Limpa o formulário
      });
    }
  }

  addEmployee(): void {
    if (this.newEmployee.firstName && this.newEmployee.email) {
      this.http.post(`${this.urlApi}`, this.newEmployee).subscribe(() => {
        this.getEmployees(); 
        this.newEmployee = { }; 
      });
    }
  }

  deleteEmployee(cpf: string): void {
    this.http.delete(`${this.urlApi}/${cpf}`).subscribe(() => {
      this.getEmployees();
    });
  }
}
