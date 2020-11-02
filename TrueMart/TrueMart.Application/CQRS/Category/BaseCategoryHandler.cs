using System;
using System.Collections.Generic;
using System.Text;
using TrueMart.Application.DatabaseServices;

namespace TrueMart.Application.CQRS.Category
{
    public class BaseCategoryHandler 
    {
        protected readonly ICategoryService CategoryService;

        public BaseCategoryHandler(ICategoryService categoryService)
        {
            CategoryService = categoryService;
        }
    }
}
