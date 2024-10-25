using GraduateProcessor.API.Models;
using GraduateProcessor.API.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GraduateProcessor.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GraduateController
{
    private readonly IGraduateService _graduateService;

    public GraduateController(IGraduateService graduateService)
    {
        _graduateService = graduateService;
    }

    [HttpGet]
    public async Task<List<Graduate>> Get()
    {
        return await _graduateService.GetUndergraduates();
    }
    
    [HttpPost]
    public async Task Post([FromBody] List<Graduate> graduates)
    {
        await _graduateService.SaveGraduatesWithValidMajors(graduates);
    }
    
    [HttpPut]
    public async Task Put([FromBody] List<Graduate> graduates, [FromBody] Term term)
    {
        await _graduateService.UpdateGraduateTerms(graduates, term);
    }
    
    [HttpDelete]
    public async Task Delete([FromBody] List<Graduate> graduates)
    {
        await _graduateService.DeleteGraduatesWithInvalidMajors(graduates);
    }
}