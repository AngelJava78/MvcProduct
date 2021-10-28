using MvcProduct.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Dapper;

namespace MvcProduct.Models.Data
{
    public class BinGetRepository : IGetRepository<Bin>
    {
        private readonly string connectionString;
        private readonly IConfiguration configuration;
        public BinGetRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            connectionString = this.configuration.GetConnectionString("MvcProduct");
        }
        public List<Bin> GetAll()
        {
            var result = new List<Bin>();
            var query = "SELECT [Id], [Name] FROM [dbo].[Bines]";
            using (var connection = new SqlConnection(connectionString))
            {
                result = connection.Query<Bin>(query).ToList();
            }
            return result;
        }
    }
}
