using System;
using System.Collections.Generic;
using System.Text;
using TrueMart.Domain.Enums;

namespace TrueMart.Application.Models
{
    public class CategoryResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UrlSlag { get; set; }
        public string Note { get; set; }
        public RecordStatus RecordStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastModifiedAt { get; set; }
    }
}
