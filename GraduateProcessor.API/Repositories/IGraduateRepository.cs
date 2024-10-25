using GraduateProcessor.API.Models;

namespace GraduateProcessor.API.Repositories;

public interface IGraduateRepository
{
    Task<List<Graduate>> GetGraduatesAsync();
    Task SaveGraduatesAsync(List<Graduate> graduates);
    
    Task DeleteGraduatesAsync(List<Graduate> graduates);
}