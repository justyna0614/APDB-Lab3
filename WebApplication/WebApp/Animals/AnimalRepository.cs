using System.Data.SqlClient;

namespace WebApp.Animals;

public interface IAnimalRepository
{
    public IEnumerable<Animal> GetAnimal(string orderBy);
    public bool AddAnimal(AnimalDTO dto);
    public bool UpdateAnimal(int id, AnimalDTO dto);
    public bool DeleteAnimal(int id);
}

public class AnimalRepository : IAnimalRepository
{
    private IConfiguration _configuration;

    public AnimalRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IEnumerable<Animal> GetAnimal(string orderBy)
    {
        using var connection = new SqlConnection(
            _configuration.GetConnectionString("DefaultConnection")
        );
        connection.Open();

        var command = new SqlCommand($"SELECT * FROM Animal ORDER BY {orderBy}", connection);
        using var reader = command.ExecuteReader();
        var animals = new List<Animal>();

        while (reader.Read())
        {
            Area area;
            Enum.TryParse(reader["Area"].ToString(), out area);
            Category category;
            Enum.TryParse(reader["Category"].ToString(), out category);
            var animal = new Animal()
            {
                Id = (int)reader["IdAnimal"],
                Name = reader["Name"].ToString()!,
                Description = reader["Description"].ToString()!,
                Category = category,
                Area = area
            };
            animals.Add(animal);
        }

        return animals;
    }

    public bool AddAnimal(AnimalDTO dto)
    {
        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();

        using var command =
            new SqlCommand("INSERT INTO Animal (Name, Description, CATEGORY, AREA) VALUES (@name, @description, @category, @area)",
                connection);
        command.Parameters.AddWithValue("@name", dto.Name);
        command.Parameters.AddWithValue("@description", dto.Description);
        command.Parameters.AddWithValue("@category", dto.Category);
        command.Parameters.AddWithValue("@area", dto.Area);

        var affectedRows = command.ExecuteNonQuery();

        return affectedRows == 1;
    }

    public bool UpdateAnimal(int id, AnimalDTO dto)
    {
        using var connection = new SqlConnection(
            _configuration.GetConnectionString("DefaultConnection")
        );
        connection.Open();

        using var command = new SqlCommand(
            $"UPDATE Animal SET Name = @name, Description = @description, Category = @category, Area = @area WHERE IdAnimal = {id}",
            connection);
        command.Parameters.AddWithValue("@name", dto.Name);
        command.Parameters.AddWithValue("@description", dto.Description);
        command.Parameters.AddWithValue("@category", dto.Category);
        command.Parameters.AddWithValue("@area", dto.Area);

        var updated = command.ExecuteNonQuery();
        return updated > 0;
    }

    public bool DeleteAnimal(int id)
    {
        using var connection = new SqlConnection(
            _configuration.GetConnectionString("DefaultConnection")
        );
        connection.Open();
        var command = new SqlCommand($"DELETE FROM Animal WHERE IdAnimal = {id}", connection);
        var deleted = command.ExecuteNonQuery();

        return deleted > 0;
    }
}