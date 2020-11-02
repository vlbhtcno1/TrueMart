using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TrueMart.Application.CQRS.Category.Command;
using TrueMart.Application.Models;

namespace TrueMart.Application.DatabaseServices
{
    public interface ICategoryService
    {
        Task<bool> CreateCategory(CreateCategoryCommand command);
        Task<IEnumerable<CategoryResponseModel>> FetchCategories();
        Task<bool> IsUniqueName(string categoryName,CancellationToken cancellationToken = new CancellationToken());
    }
}
