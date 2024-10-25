using GraduateProcessor.API.Models;

namespace GraduateProcessor.API.Repositories;

public interface IDegreeRepository
{
    List<Major> GetValidMajorsBySchool(School school);
}