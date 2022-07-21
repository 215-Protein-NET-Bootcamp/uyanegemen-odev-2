using homework2_NET.Models;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Data;

namespace homework2_NET.Controllers
{
    public class CommonResponse<T>
    {
        public CommonResponse(T data)
        {
            Data = data;
        }
        public CommonResponse(string error)
        {
            Error = error;
            Success = false;
        }
        public bool Success { get; set; } = true;
        public string Error { get; set; }
        public T Data { get; set; }
    }


    [Route("api/[controller]")]
    [ApiController]
    
    public class CountryController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public CountryController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            //string query = @" 
            //    select countryname as ""country_name"", 
            //    continent as ""continent"", currency as ""currency""   
            //    from country
            //";
            string query = @"
                select * from public.country
            ";
            List<Country> countryList = new List<Country>();
            //DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("postgreSqlCon");
            using (NpgsqlConnection connection = new NpgsqlConnection(sqlDataSource))
            {
                NpgsqlDataReader myReader;
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    myReader = command.ExecuteReader();
                    while (myReader.Read())
                    {
                        Country country = new Country();
                        country.CountryId = (long)myReader["countryid"];
                        country.CountryName = (string)myReader["countryname"];
                        country.Continent = (string)myReader["continent"];
                        country.Currency = (string)myReader["currency"];
                        countryList.Add(country);   
                    }
                    //table.Load(myReader);

                    myReader.Close();
                    connection.Close();
                }
            }

            return new JsonResult(countryList);
        }
    }
}
