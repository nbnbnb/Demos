using ASPPatterns.Chap7.Concurrency.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ASPPatterns.Chap7.Concurrency.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private string _connectionString;

        private string _findByIdSQL =
            "SELECT * FROM People WHERE PersonId = @PersonId";
        private string _insertSQL =
            "INSERT People (FirstName, LastName, PersonId, Version) VALUES " +
            "(@FirstName, @LastName, @PersonId, @Version)";
        private string _updateSQL =
            "UPDATE People SET FirstName = "
            + "@FirstName, LastName = @LastName, Version =" +
            "@Version + 1 WHERE PersonId = @PersonId AND Version= @Version;";
        public PersonRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void Add(Person person)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = _insertSQL;
                command.Parameters.Add(new SqlParameter("@PersonId", person.Id));
                command.Parameters.Add(new SqlParameter("@Version", person.Version));
                command.Parameters.Add(new SqlParameter("@FirstName", person.FirstName));
                command.Parameters.Add(new SqlParameter("@LastName", person.LastName));
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public void Save(Person person)
        {
            int numberOfRecordsAffected = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = _updateSQL;
                command.Parameters.Add(new SqlParameter("@PersonId", person.Id));
                command.Parameters.Add(new SqlParameter("@Version", person.Version));
                command.Parameters.Add(new SqlParameter("@FirstName", person.FirstName));
                command.Parameters.Add(new SqlParameter("@LastName", person.LastName));
                connection.Open();
                numberOfRecordsAffected = command.ExecuteNonQuery();
            }

            if (numberOfRecordsAffected == 0)
                throw new ApplicationException(
                    @"No changes were made to Person Id (" + person.Id + "), this was "
                    + "due to another process updating the data.");
            else
                person.Version++;
        }
        public Person FindBy(Guid Id)
        {
            Person person = default(Person);
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = connection.CreateCommand();

                command.CommandText = _findByIdSQL;
                command.Parameters.Add(new SqlParameter("@PersonId", Id));
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        person = new Person
                        {
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Id = new Guid(reader["PersonId"].ToString()),
                            Version = int.Parse(reader["Version"].ToString())
                        };
                    }
                }
            }
            return person;
        }
    }
}
