namespace BaseCleanArchitecture.Domain.Employees
{
    public sealed record PersonelInformation
    {
        public string IdentityNumber { get; set; } = default!;
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}
