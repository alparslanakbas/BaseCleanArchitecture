using BaseCleanArchitecture.Domain.Abstractions;

namespace BaseCleanArchitecture.Domain.Employees
{
    public sealed class Employee : BaseEntity
    {
        private static readonly Random _random = new Random();

        public Employee()
        {
            LastName = GenerateDefaultLastName();
        }
        public string FirstName { get; set; } = "Guest";
        public string LastName { get; set; }
        public string FullName => string.Join(" ", FirstName, LastName);
        public DateOnly BirthOfDate { get; set; }
        public decimal Salary { get; set; }
        public Adress? Adress { get; set; }
        public PersonelInformation PersonelInformation { get; set; } = default!;

        private string GenerateDefaultLastName()
        {
            return $"{FirstName}{_random.Next(100000, 999999)}";
        }
    }
}
