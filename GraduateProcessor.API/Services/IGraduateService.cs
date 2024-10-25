using GraduateProcessor.API.Models;

namespace GraduateProcessor.API.Services;

public interface IGraduateService
{
    /// <summary>
    /// Gets a list of graduates with BS/BA degrees.
    /// </summary>
    /// <returns></returns>
    Task<List<Graduate>> GetUndergraduates();

    /// <summary>
    /// Given a list of graduates, update graduates to the new term if they aren't already in the given term
    /// </summary>
    /// <param name="graduates"></param>
    /// <param name="term"></param>
    /// <returns></returns>
    Task UpdateGraduateTerms(List<Graduate> graduates, Term term);
    
    /// <summary>
    /// Given a list of graduates, saves graduates whose major is supported at their school.
    /// </summary>
    /// <param name="graduates"></param>
    /// <returns>The valid graduates that were saved</returns>
    Task<List<Graduate>> SaveGraduatesWithValidMajors(List<Graduate> graduates);
    
    /// <summary>
    /// Given a list of graduates, deletes graduates whose major is not supported at their school.
    /// </summary>
    /// <param name="graduates"></param>
    /// <returns>The deleted graduates with invalid majors</returns>
    Task<List<Graduate>> DeleteGraduatesWithInvalidMajors(List<Graduate> graduates);
}