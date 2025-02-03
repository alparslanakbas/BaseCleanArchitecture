namespace BaseCleanArchitecture.Domain.Employees
{
    public sealed record Adress
    {
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Town { get; set; }
    }
}
