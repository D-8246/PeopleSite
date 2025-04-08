using Microsoft.Data.SqlClient;

namespace PeopleSite.Data
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }

    public class PeopleManager
    {
        private readonly string _connectionString;
        public PeopleManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Person> GetAll()
        {
            var connection = new SqlConnection(_connectionString);
            var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM People";
            connection.Open();
            var reader = cmd.ExecuteReader();
            List<Person> people = new();
            while (reader.Read())
            {
                people.Add(new Person
                {
                    Id = (int)reader["Id"],
                    FirstName = (string)reader["firstName"],
                    LastName = (string)reader["lastName"],
                    Age = (int)reader["age"],
                });
            }
            return people;
        }

        public int AddPeople(List<Person> people)
        {
            var connection = new SqlConnection(_connectionString);
            var cmd = connection.CreateCommand();
            cmd.CommandText = "INSERT INTO People VALUES (@firstName, @lastName, @age)";
            int i = 0;
            connection.Open();

            foreach (Person person in people)
            {
                if (person.FirstName != null && person.LastName != null && person.Age != 0)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@firstName", person.FirstName);
                    cmd.Parameters.AddWithValue("@lastName", person.LastName);
                    cmd.Parameters.AddWithValue("@age", person.Age);
                    cmd.ExecuteNonQuery();
                    i++;
                }
            }
            return i;
        }

        public void DeleteAll(List<int> ids)
        {
            var connection = new SqlConnection(_connectionString);
            var cmd = connection.CreateCommand();
            cmd.CommandText = "DELETE FROM People WHERE Id = @id";
            connection.Open();

            foreach (var id in ids)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
