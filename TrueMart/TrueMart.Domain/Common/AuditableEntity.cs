using System;
using System.Collections.Generic;
using System.Text;

namespace TrueMart.Domain.Common
{
    public abstract class AuditableEntity
    {
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime LastModifiedAt { get; set; }
        public int LastModifiedBy { get; set; }
        public string Note { get; set; }

    }
}
