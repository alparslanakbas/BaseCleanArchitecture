using BaseCleanArchitecture.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCleanArchitecture.Infrastructure.EntityConfigurations
{
    internal abstract class BaseEntityConfiguration<TBaseEntity> : IEntityTypeConfiguration<TBaseEntity> where TBaseEntity : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TBaseEntity> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.CreatedDate).IsRequired().HasColumnName("OlusturmaTarihi");
            builder.Property(i => i.UpdatedDate).HasColumnName("GuncellemeTarihi");
            builder.Property(i => i.IsDelete).HasDefaultValue(false).HasColumnName("Durum");
            builder.Property(i => i.DeleteAt).HasColumnName("DurumTarihi");
        }
    }
}
