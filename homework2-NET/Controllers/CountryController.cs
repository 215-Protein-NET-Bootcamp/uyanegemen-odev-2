using homework2_NET.Models;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Data;

namespace homework2_NET.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class CountryController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public CountryController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("GetAll")]
        public JsonResult Get()
        {
            string query = @"
                select * from public.country
            ";
            List<Country> countryList = new List<Country>();

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
                        country.CountryId = (int)myReader["countryid"];
                        country.CountryName = (string)myReader["countryname"];
                        country.Continent = (string)myReader["continent"];
                        country.Currency = (string)myReader["currency"];
                        countryList.Add(country);   
                    }


                    myReader.Close();
                    connection.Close();
                }
            }

            return new JsonResult(countryList);
        }

        [HttpGet("{id:int}")]
        public JsonResult GetByIdAsync(int id)
        {
            string query = @"select * from public.country where countryid = " + id;

            Country country = new Country();

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
                        country.CountryId = (int)myReader["countryid"];
                        country.CountryName = (string)myReader["countryname"];
                        country.Continent = (string)myReader["continent"];
                        country.Currency = (string)myReader["currency"];
                    }


                    myReader.Close();
                    connection.Close();
                }
            }

            return new JsonResult(country);
        }

        [HttpGet("insert country")]
        public JsonResult InsertCountry(string name, string continent, string currency)
        {
            string country_info = String.Format("'{0}','{1}','{2}')",name,continent,currency);
            string query = @"insert into public.country(countryname,continent,currency) values("+country_info;
            bool success = true;

            string sqlDataSource = _configuration.GetConnectionString("postgreSqlCon");
            using (NpgsqlConnection connection = new NpgsqlConnection(sqlDataSource))
            {
                NpgsqlDataReader myReader;
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    try
                    {
                        myReader = command.ExecuteReader();

                        myReader.Close();
                        connection.Close();
                    }
                    catch (Exception)
                    {
                        success = false;    
                    }
                    
                }
            }
            if (success)
            {
                return new JsonResult("Country added successfuly.");
            }
            else
            {
                return new JsonResult("Country can't added.");
            }
            
        }

        [HttpGet("delete {id:int}")]
        public JsonResult DeleteById(int id)
        {
            string query = @"delete from public.country where countryid = " + id;
            bool success = true;

            string sqlDataSource = _configuration.GetConnectionString("postgreSqlCon");
            using (NpgsqlConnection connection = new NpgsqlConnection(sqlDataSource))
            {
                NpgsqlDataReader myReader;
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    try
                    {
                        myReader = command.ExecuteReader();

                        myReader.Close();
                        connection.Close();
                    }
                    catch (Exception)
                    {
                        success = false;

                    }
                   
                }
            }
            if (success)
            {
                return new JsonResult("Country deleted successfuly.");
            }
            else
            {
                return new JsonResult("Country can't deleted.");
            }
        }


    }
}
