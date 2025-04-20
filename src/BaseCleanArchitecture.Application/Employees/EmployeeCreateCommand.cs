using BaseCleanArchitecture.Domain.Employees;
using CD.GenericRepository;
using CD.Results;
using FluentValidation;
using Mapster;
using MediatR;
using System.Net;

namespace BaseCleanArchitecture.Application.Employees
{
    public sealed record EmployeeCreateCommand
    (
        string FirstName,
        string LastName,
        DateOnly BirthOfDate,
        decimal Salary,
        PersonelInformation PersonelInformation,
        Adress? Adress
    ) :IRequest<Result<string>>;


    internal sealed class EmployeeCreateCommandHandler(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork) : IRequestHandler<EmployeeCreateCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(EmployeeCreateCommand request, CancellationToken cancellationToken)
        {
            var isExist = await employeeRepository.AnyAsync(e => e.PersonelInformation.IdentityNumber == request.PersonelInformation.IdentityNumber, cancellationToken);
            if (isExist)
            {
                return Result<string>.Failure(HttpStatusCode.Conflict,$"Bu {request.PersonelInformation.IdentityNumber} Kimlik Numarası ile kayıtlı bir çalışan mevcut");
            }

            Employee employee = request.Adapt<Employee>();

            employeeRepository.Add(employee);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<string>.Success($"{employee.FullName} Başarılı bir şekilde Eklendi");
        }
    }

    public sealed class EmployeeCreateCommandValidator : AbstractValidator<EmployeeCreateCommand>
    {
        public EmployeeCreateCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Ad alanı boş geçilemez")
                .MinimumLength(2).WithMessage("Ad en az 2 karakter içermelidir.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Soyad alanı boş geçilemez")
                .MinimumLength(2).WithMessage("Soyad en az 2 karakter içermelidir.");

            RuleFor(x => x.PersonelInformation.IdentityNumber)
                .NotEmpty().WithMessage("Kimlik Numarası alanı boş geçilemez")
                .Must(x => x.Length == 11).WithMessage("Kimlik Numarası 11 karakter olmalıdır.");

        }
    }

}
