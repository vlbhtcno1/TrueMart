using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TrueMart.Domain.Enums
{
    public enum RecordStatus
    {
        [Description("Active")]
        Active = 1,
        [Description("In active")]
        InActive = 2
    }
}
