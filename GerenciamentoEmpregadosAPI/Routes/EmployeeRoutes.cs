using GerenciamentoEmpregadosAPI.Data;
using GerenciamentoEmpregadosAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Runtime.CompilerServices;

namespace GerenciamentoEmpregadosAPI.Routes
{
    public static class EmployeeRoutes
    {
        public static void AddEmployeeRoutes(WebApplication app)
        {
            
            // Retorna lista de empregados com possibilidade de filtro (OK)
            app.MapGet("/employee", async (AppDbContext context, string? firstName, string? lastName, string? email, string? cpf) =>
            {
                var query = context.Employees.AsQueryable();

                if (!string.IsNullOrEmpty(firstName))
                {
                    query = query.Where(employee => employee.FirstName.Contains(firstName));
                }

                if (!string.IsNullOrEmpty(lastName))
                {
                    query = query.Where(employee => employee.LastName.Contains(lastName));
                }

                if (!string.IsNullOrEmpty(email))
                {
                    query = query.Where(employee => employee.Email.Contains(email));
                }

                if (!string.IsNullOrEmpty(cpf))
                {
                    query = query.Where(employee => employee.CPF.Contains(cpf));
                }

                var employees = await query
                .Select(employee => new EmployeeDetailedInfo(
                    employee.FirstName,
                    employee.LastName,
                    employee.Email,
                    employee.CPF,
                    employee.DateOfBirth,
                    employee.Address,
                    employee.CurrentlyEmployed)).ToListAsync();
                
                return Results.Ok(employees);
            });

            // Cria empregado (OK)
            app.MapPost("/employee", async (AddEmployeeRequest request , AppDbContext context) =>
            {
                var isEmployeeRegistered = await context.Employees.AnyAsync(employee =>
                employee.CPF == request.CPF);

                if (isEmployeeRegistered) return Results.Conflict("Employee already registered!");

                var newEmployee = new Employee(request.FirstName, request.LastName, request.Email, request.CPF);
                await context.Employees.AddAsync(newEmployee);
                await context.SaveChangesAsync();

                var employeeBasicInfo = new EmployeeBasicInfo(newEmployee.FirstName, newEmployee.Email);
                return Results.Ok(employeeBasicInfo);
            });

            // Encontra employee por cpf e atualiza os dados requisitados (OK)
            app.MapPut("/employee", async (ModifyEmployeeRequest request, AppDbContext context) =>
            {
                var employee = await context.Employees.FirstOrDefaultAsync(employee => employee.CPF == request.CPF);

                if (employee == null) return Results.NotFound("Employee Not Found!");

                if (!string.IsNullOrEmpty(request.FirstName)) employee.FirstName = request.FirstName;
                if (!string.IsNullOrEmpty(request.LastName)) employee.LastName = request.LastName;
                if (!string.IsNullOrEmpty(request.Email)) employee.Email = request.Email;
                if (!string.IsNullOrEmpty(request.CPF)) employee.CPF = request.CPF;
                if (request.DateOfBirth.HasValue) employee.DateOfBirth = request.DateOfBirth.Value;
                if (!string.IsNullOrEmpty(request.Address)) employee.Address = request.Address;
                if (request.CurrentlyEmployed.HasValue) employee.CurrentlyEmployed = request.CurrentlyEmployed.Value;

                await context.SaveChangesAsync();

                return Results.Ok("Employee updated!");
            });

            // Encontra employee por cpf e o deleta (OK)
            app.MapDelete("/employee/{cpf}", async (AppDbContext context, string cpf) =>
            {
                var employee = await context.Employees.FirstOrDefaultAsync(employee => employee.CPF == cpf);

                if(employee == null) return Results.NotFound("Employee Not Found!");

                context.Employees.Remove(employee);
                await context.SaveChangesAsync();

                return Results.Ok("Employee deleted!");

            });
        }
    }
}
