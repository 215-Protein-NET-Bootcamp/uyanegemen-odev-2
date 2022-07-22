using homework2_NET.Context;
using homework2_NET.Models;
using homework2_NET.Repository.Abstract;
using Npgsql;

namespace homework2_NET.Repository.Concrete
{
    public class CountryRepository : IBaseRepository<Country>
    {
        private readonly AppDbContext appDbContext;
        public CountryRepository(AppDbContext dbContext) : base()
        {
            this.appDbContext = dbContext;
        }
        public List<Country> GetAllAsync()
        {
            string query = @"
                select * from public.country
            ";
            List<Country> countryList = new List<Country>();

            using (var connection = appDbContext.CreateConnection())
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

                    myReader.Close();
                    connection.Close();
                }
            }
            return countryList;
        }

        public Country GetByIdAsync(int entityId)
        {
            throw new NotImplementedException();
        }

        public Task InsertAsync(Country entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveAsync(Country entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Country entity)
        {
            throw new NotImplementedException();
        }
    }
}
