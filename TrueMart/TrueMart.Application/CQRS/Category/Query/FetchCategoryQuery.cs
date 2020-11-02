using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TrueMart.Application.DatabaseServices;
using TrueMart.Application.Models;

namespace TrueMart.Application.CQRS.Category.Query
{
    public class FetchCategoryQuery : IRequest<IEnumerable<CategoryResponseModel>>
    {
    }

    public class FetchCategoryQueryHandler : BaseCategoryHandler,IRequestHandler<FetchCategoryQuery, IEnumerable<CategoryResponseModel>>
    {
        public async Task<IEnumerable<CategoryResponseModel>> Handle(FetchCategoryQuery request, CancellationToken cancellationToken)
        {
            var result = await CategoryService.FetchCategories();
            return result;
        }

        public FetchCategoryQueryHandler(ICategoryService categoryService) : base(categoryService)
        {

        }
    }
}
