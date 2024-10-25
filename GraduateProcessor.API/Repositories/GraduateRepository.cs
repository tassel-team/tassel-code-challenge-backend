using GraduateProcessor.API.Models;

namespace GraduateProcessor.API.Repositories;

public class GraduateRepository : IGraduateRepository
{
    public GraduateRepository()
    {
        
    }

    public Task<List<Graduate>> GetGraduatesAsync()
    {
        List<Graduate> bachelorGrads =
        [
            new Graduate() { Degree = new Degree(new(Guid.NewGuid().ToString()), DegreeType.BA) },
            new Graduate() { Degree = new Degree(new(Guid.NewGuid().ToString()), DegreeType.BA) },
            new Graduate() { Degree = new Degree(new(Guid.NewGuid().ToString()), DegreeType.BS) },
            new Graduate() { Degree = new Degree(new(Guid.NewGuid().ToString()), DegreeType.BS) },
        ];
        List<Graduate> graduates =
        [
            ..bachelorGrads,
            new Graduate() { Degree = new Degree(new(Guid.NewGuid().ToString()), DegreeType.MS) },
            new Graduate() { Degree = new Degree(new(Guid.NewGuid().ToString()), DegreeType.MA) },
            new Graduate() { Degree = new Degree(new(Guid.NewGuid().ToString()), DegreeType.PhD) },
        ];

        return Task.FromResult(graduates);
    }

    public Task SaveGraduatesAsync(List<Graduate> graduates)
    {
        Console.WriteLine("Saving graduates...");
        return Task.CompletedTask;
    }

    public Task DeleteGraduatesAsync(List<Graduate> graduates)
    {
        Console.WriteLine("Deleting graduates...");
        return Task.CompletedTask;
    }
}