using Dapper;
using Microsoft.Extensions.Configuration;
using MvcProduct.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MvcProduct.Models.Data
{
    public class FamilyGetRepository : IGetRepository<Family>
    {
        private readonly string connectionString;
        private readonly IConfiguration configuration;
        public FamilyGetRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            connectionString = this.configuration.GetConnectionString("MvcProduct");
        }
        public List<Family> GetAll()
        {
            var result = new List<Family>();
            var query = "SELECT [Id], [Name] FROM [dbo].[Families]";
            using (var connection = new SqlConnection(connectionString))
            {
                result = connection.Query<Family>(query).ToList();
            }
            return result;
        }
    }
}
