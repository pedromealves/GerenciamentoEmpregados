namespace GerenciamentoEmpregadosAPI.Models;

public record AddEmployeeRequest(string FirstName,
    string LastName,
    string Email,
    string CPF);

