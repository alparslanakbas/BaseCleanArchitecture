using BaseCleanArchitecture.Domain.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCleanArchitecture.Infrastructure.EntityConfigurations
{
    internal sealed class EmployeeEntityConfiguration : BaseEntityConfiguration<Employee>
    {
        public override void Configure(EntityTypeBuilder<Employee> builder)
        {
            base.Configure(builder);

            // Employee
            builder.Property(i => i.FirstName).HasColumnName("BirincilAd");
            builder.Property(i => i.LastName).HasColumnName("IkincilAd");
            builder.Ignore(i => i.FullName);
            builder.Property(i => i.BirthOfDate).HasColumnName("DogumTarihi");
            builder.Property(e => e.Salary).HasColumnType("money").HasColumnName("Maas");

            // PersonelInformation
            builder.OwnsOne(p => p.PersonelInformation, builder =>
            {
                builder.Property(i => i.IdentityNumber).HasColumnName("TCNO");
                builder.Property(i => i.Phone).HasColumnName("Telefon");
                builder.Property(i => i.Email).HasColumnName("Mail");
            });

            // Adress
            builder.OwnsOne(a => a.Adress, builder =>
            {
                builder.Property(i => i.Country).HasColumnName("Ulke");
                builder.Property(i => i.City).HasColumnName("Sehir");
                builder.Property(i => i.Town).HasColumnName("Ilce");
            });
        }
    }
}
