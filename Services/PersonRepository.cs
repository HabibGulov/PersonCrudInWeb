using Npgsql;
using Dapper;
using Microsoft.AspNetCore.Mvc;

public class PersonRepository : ControllerBase, IPersonRepository
{
    public async Task<IEnumerable<Person>> GetAllPersonsAsync()
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.connectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryAsync<Person>(SqlCommands.GetAll);
            }
        }
        catch (NpgsqlException ex)
        {
            Console.WriteLine(ex.Message);
            return new List<Person>();
        }
    }

    public async Task<Person?> GetPersonByIdAsync(Guid id)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.connectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryFirstOrDefaultAsync<Person>(SqlCommands.GetById, new {Id=id});
            }
        }
        catch (NpgsqlException ex)
        {
            Console.WriteLine(ex.Message);
            return new Person();
        }
    }

    public async Task<bool> CreatePersonAsync(Person person)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.connectionString))
            {
                await connection.OpenAsync();
                return await connection.ExecuteAsync(SqlCommands.Create, person) > 0;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<bool> UpdatePersonAsync(Person person)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.connectionString))
            {
                await connection.OpenAsync();
                return await connection.ExecuteAsync(SqlCommands.Update, person) > 0;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in UpdatePersonAsync: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeletePersonAsync(Guid id)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommands.connectionString))
            {
                await connection.OpenAsync();
                return await connection.ExecuteAsync(SqlCommands.Delete, new{Id=id}) > 0;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in DeletePersonAsync: {ex.Message}");
            return false;
        }
    }
}


public static class SqlCommands
{
    public const string connectionString = "Server=localhost;Port=5432;Database=education_db;Username=postgres;Password=12345";
    public const string GetAll = "SELECT id, fullname, birthdate, email FROM person";
    public const string GetById = "SELECT id as Id, fullname as FullName, birthdate as Birthdate, email as Email FROM person WHERE id = @id";
    public const string Create = "INSERT INTO person (id, fullname, birthdate, email) VALUES (@Id, @FullName, @Birthdate, @Email)";
    public const string Update = "UPDATE person SET fullname = @FullName, birthdate = @Birthdate, email = @Email WHERE id = @Id";
    public const string Delete = "DELETE FROM person WHERE id = @Id";
}