using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCleanArchitecture.Domain.Abstractions
{
    public abstract class BaseEntityDto
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset? UpdatedDate { get; set; }
        public bool IsDelete { get; set; }
        public DateTimeOffset DeleteAt { get; set; }
    }
}
