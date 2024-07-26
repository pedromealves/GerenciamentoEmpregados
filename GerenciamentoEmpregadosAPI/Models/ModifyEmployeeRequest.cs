namespace GerenciamentoEmpregadosAPI.Models;

public record ModifyEmployeeRequest(
    string? FirstName,
    string? LastName,
    string? Email,
    string? CPF,
    DateTime? DateOfBirth,
    string? Address,
    bool? CurrentlyEmployed);

