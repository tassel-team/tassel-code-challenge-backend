using GraduateProcessor.API.Models;
using GraduateProcessor.API.Repositories;

namespace GraduateProcessor.API.Services;

public class GraduateService : IGraduateService
{
    private readonly IGraduateRepository _graduateRepository;
    private readonly IDegreeRepository _degreeRepository;

    public GraduateService(IGraduateRepository graduateRepository, IDegreeRepository degreeRepository)
    {
        _graduateRepository = graduateRepository;
        _degreeRepository = degreeRepository;
    }

    /// <summary>
    /// Gets a list of graduates with BS/BA degrees.
    /// </summary>
    /// <returns></returns>
    public Task<List<Graduate>> GetUndergraduates()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Given a list of graduates, save graduates with the new term if they aren't already in the given term
    /// </summary>
    /// <param name="graduates"></param>
    /// <param name="term"></param>
    /// <returns></returns> 
    public Task UpdateGraduateTerms(List<Graduate> graduates, Term term)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Given a list of graduates, saves graduates whose major is supported at their school.
    /// </summary>
    /// <param name="graduates"></param>
    /// <returns>The valid graduates that were saved</returns>
    public Task<List<Graduate>> SaveGraduatesWithValidMajors(List<Graduate> graduates)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Given a list of graduates, deletes graduates whose major is not supported at their school.
    /// </summary>
    /// <param name="graduates"></param>
    /// <returns>The deleted graduates with invalid majors</returns>
    public Task<List<Graduate>> DeleteGraduatesWithInvalidMajors(List<Graduate> graduates)
    {
        throw new NotImplementedException();
    }
}