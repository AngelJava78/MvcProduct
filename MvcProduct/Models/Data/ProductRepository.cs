using Dapper;
using Microsoft.Extensions.Configuration;
using MvcProduct.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MvcProduct.Models.Data
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;
        public ProductRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            connectionString = this.configuration.GetConnectionString("MvcProduct");
        }

        public int Add(Product item)
        {
            var rows = 0;
            var query = "[dbo].[usp_Products_Insert]";
            using (var connection = new SqlConnection(connectionString))
            {
                var parameters = new 
                { 
                    Name = item.Name, 
                    Description = item.Description, 
                    Family = item.Family, 
                    Bin = item.Bin, 
                    IsActive = item.IsActive, 
                    ReleaseDate = item.ReleaseDate 
                };
                rows = connection.Execute(query, parameters, commandType: CommandType.StoredProcedure);
                
            }
            return rows;
        }

        public bool Delete(int id)
        {
            var rows = 0;
            var query = "[dbo].[usp_Products_Delete]";
            using (var connection = new SqlConnection(connectionString))
            {
                var parameters = new
                {
                    Id = id
                };
                rows = connection.Execute(query, parameters, commandType: CommandType.StoredProcedure);
            }
            return rows > 0;
        }

        public List<Product> GetAll()
        {
            var result = new List<Product>();
            var query = "SELECT [Id], [Name], [Description], [Family], [Bin], [IsActive], [ReleaseDate] FROM [dbo].[Products]";
            using (var connection = new SqlConnection(connectionString))
            {
                result = connection.Query<Product>(query).ToList();
            }
            return result;
        }

        public Product GetById(int id)
        {
            Product result = null;
            var query = "SELECT [Id], [Name], [Description], [Family], [Bin], [IsActive], [ReleaseDate] FROM [dbo].[Products] WHERE Id = @Id";
            using (var connection = new SqlConnection(connectionString))
            {
                result = connection.Query<Product>(query, new { Id = id }, commandType: CommandType.Text).FirstOrDefault();
            }
            return result;
        }

        public bool Update(Product item)
        {
            var rows = 0;
            var query = "[dbo].[usp_Products_Update]";
            using (var connection = new SqlConnection(connectionString))
            {
                var parameters = new
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Family = item.Family,
                    Bin = item.Bin,
                    IsActive = item.IsActive,
                    ReleaseDate = item.ReleaseDate
                };
                rows = connection.Execute(query, parameters, commandType: CommandType.StoredProcedure);
            }
            return rows > 0;
        }
    }
}
