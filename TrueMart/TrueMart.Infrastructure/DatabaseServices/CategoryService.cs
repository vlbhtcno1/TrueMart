using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using SqlKata.Compilers;
using SqlKata.Execution;
using TrueMart.Application.CQRS.Category.Command;
using TrueMart.Application.DatabaseServices;
using TrueMart.Application.Models;

namespace TrueMart.Infrastructure.DatabaseServices
{
    public class CategoryService : ICategoryService
    {
        private readonly IDatabaseConnectionFactory _database;

        public CategoryService(IDatabaseConnectionFactory database)
        {
            _database = database;
        }
        public async Task<bool> CreateCategory(CreateCategoryCommand command)
        {
            var parameters = new
            {
                Name = command.Name,
                UrlSlag = command.UrlSlag,
                RecordStatus = (int)command.RecordStatus,
                Note = command.Note,
                CreatedAt = DateTime.Now,
                CreatedBy = command.CreatedBy,
                LastModifiedBy = command.CreatedBy,
                LastModifiedAt = DateTime.Now,
            };
            using var conn = await _database.CreateConnectionAsync();
            var query = new QueryFactory(conn, new SqlServerCompiler());
            var affectedRecord = await query.Query("Category").InsertAsync(parameters);
            return affectedRecord > 1;


        }

        public async Task<IEnumerable<CategoryResponseModel>> FetchCategories()
        {
            using var conn = await _database.CreateConnectionAsync();
            var query = new QueryFactory(conn, new SqlServerCompiler());
            var queryResult = query.Query("Category").Select("Id", "Name", "UrlSlag", "Note", "CreatedAt",
                "LastModifiedAt", "RecordStatus");
            var result = await queryResult.GetAsync<CategoryResponseModel>();
            return result;
        }

        public async Task<bool> IsUniqueName(string categoryName, CancellationToken cancellationToken = new CancellationToken())
        {
            using var conn = await _database.CreateConnectionAsync();
            var query = new QueryFactory(conn, new SqlServerCompiler());
            var parameters = new { Name = categoryName };
            var result = await query.Query("Category").Where("Name", "=", categoryName).FirstOrDefaultAsync();
            return result == null;
        }
    }
}
