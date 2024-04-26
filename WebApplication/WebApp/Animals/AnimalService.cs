namespace WebApp.Animals;

public interface IAnimalService
{
    public IEnumerable<Animal> GetAnimal(string orderBy);
    public bool AddAnimal(AnimalDTO dto);
    public bool UpdateAnimal(int id, AnimalDTO dto);
    public bool DeleteAnimal(int id);
}

public class AnimalService : IAnimalService
{
    private static HashSet<string> AvaliableOrderByParameters = ["Name", "Description", "Category", "Area"];


    private IAnimalRepository _repository;

    public AnimalService(IAnimalRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<Animal> GetAnimal(string orderBy)
    {
        var safeOrderBy = AvaliableOrderByParameters.Contains(orderBy) ? orderBy : "Name";
        return _repository.GetAnimal(safeOrderBy);
    }

    public bool AddAnimal(AnimalDTO dto)
    {
        
        return _repository.AddAnimal(dto);
    }

    public bool UpdateAnimal(int id, AnimalDTO dto)
    {
        return _repository.UpdateAnimal(id, dto);
    }

    public bool DeleteAnimal(int id)
    {
        return _repository.DeleteAnimal(id);
    }
}