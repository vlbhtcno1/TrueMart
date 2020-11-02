using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TrueMart.Application.DatabaseServices;
using TrueMart.Domain.Enums;

namespace TrueMart.Application.CQRS.Category.Command
{
    public class CreateCategoryCommand : IRequest<bool>
    {
        public string Name { get; set; }
        public string UrlSlag { get; set; }
        public RecordStatus RecordStatus { get; set; }
        public string Note { get; set; }
        public int CreatedBy { get; set; }

    }
    public class CreateCategoryCommandHandler : BaseCategoryHandler,IRequestHandler<CreateCategoryCommand,bool>
    {
        public CreateCategoryCommandHandler(ICategoryService categoryService) : base(categoryService)
        {

        }
        public async Task<bool> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var result = await CategoryService.CreateCategory(request);
            return result;
        }

       
    }
}
