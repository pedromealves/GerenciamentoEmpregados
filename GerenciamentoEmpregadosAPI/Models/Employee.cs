using System.ComponentModel.DataAnnotations;

namespace GerenciamentoEmpregadosAPI.Models
{
    public class Employee
    {
        [Key]
        public Guid Id { get; init; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string CPF { get; set; }
        public DateTime DateOfBirth { get; set; } = DateTime.MinValue;
        public string Address { get; set; } = string.Empty;
        public bool CurrentlyEmployed { get; set; } = true;

        public Employee() { }

        public Employee(string firstName, string lastName, string email, string cpf)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            CPF = cpf;
        }
    }
}
