using GraduateProcessor.API.Models;

namespace GraduateProcessor.API.Repositories;

public class DegreeRepository : IDegreeRepository
{
    public List<Major> GetValidMajorsBySchool(School school)
    {
        return school.Name switch
        {
            Constants.UniversityOfPortland =>
            [
                new Major("Biology"), new Major("Physics"), new Major("Mathematics"), new Major("Chemistry"),
                new Major("Political Science"), new Major("Business Administration"),
            ],
            Constants.UniversityOfLouisville =>
            [
                new Major("Computer Science"), new Major("Electrical Engineering"),
                new Major("Mechanical Engineering"), new Major("Biology"),
            ],
            Constants.CampbellsvilleUniversity =>
            [
                new Major("Economics"), new Major("Business Administration"), new Major("Psychology"),
                new Major("Sociology"), new Major("Political Science"),
            ],
            _ => []
        };
    }
}