using GraduateProcessor.API.Models;
using GraduateProcessor.API.Repositories;
using GraduateProcessor.API.Services;
using NSubstitute;

namespace GraduateProcessor.Tests;

public class Tests
{
    private GraduateService _graduateService;
    private IGraduateRepository _graduateRepository;
    private IDegreeRepository _degreeRepository;

    [SetUp]
    public void Setup()
    {
        _degreeRepository = new DegreeRepository();
        _graduateRepository = Substitute.For<IGraduateRepository>();
        _graduateService = new GraduateService(_graduateRepository, _degreeRepository);
    }
    
    [Test]
    public async Task ShouldFilterForBachelorsGraduates()
    {
        // arrange
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
        
        _graduateRepository
            .GetGraduatesAsync()
            .Returns(graduates);

        // act
        var results = await _graduateService.GetUndergraduates();

        // assert
        Assert.That(results.Count, Is.EqualTo(bachelorGrads.Count));
        Assert.That(results.All(g => g.Degree.Type == DegreeType.BA || g.Degree.Type == DegreeType.BS));
        
        await _graduateRepository
            .Received(1)
            .GetGraduatesAsync();
    }

    [Test]
    public async Task ShouldUpdateGraduatesWithMismatchedTerms()
    {
        // arrange
        var expectedTerm = new Term(Season.Fall, 2024);
        var wrongTermGradName = Guid.NewGuid().ToString();
        List<Graduate> wrongTermGrads =
        [
            new Graduate() { Term = new Term(Season.Fall, 2023), FullName = wrongTermGradName},
            new Graduate() { Term = new Term(Season.Spring, 2024), FullName = wrongTermGradName},
            new Graduate() { Term = new Term(Season.Summer, 2025), FullName = wrongTermGradName},
        ];
        List<Graduate> graduates =
        [
            ..wrongTermGrads,
            new Graduate() { Term = expectedTerm, FullName = Guid.NewGuid().ToString()},
            new Graduate() { Term = expectedTerm, FullName = Guid.NewGuid().ToString()},
            new Graduate() { Term = expectedTerm, FullName = Guid.NewGuid().ToString()},
            new Graduate() { Term = expectedTerm, FullName = Guid.NewGuid().ToString()},
        ];

        // act
        await _graduateService.UpdateGraduateTerms(graduates, expectedTerm);

        // assert
        await _graduateRepository.Received()
            .SaveGraduatesAsync(Arg.Is<List<Graduate>>(grads =>
                grads.All(g => g.Term.Equals(expectedTerm) && g.FullName == wrongTermGradName) &&
                grads.Count == wrongTermGrads.Count));
    }

    [Test]
    public async Task ShouldSaveGradsWithValidMajors()
    {
        // arrange
        List<Graduate> gradsWithValidMajors =
        [
            new Graduate()
            {
                Degree = new Degree(new("Biology"), DegreeType.BS),
                School = new(Constants.UniversityOfPortland)
            },
            new Graduate()
            {
                Degree = new Degree(new("Computer Science"), DegreeType.BS),
                School = new(Constants.UniversityOfLouisville)
            },
            
            new Graduate()
            {
                Degree = new Degree(new("Sociology"), DegreeType.BS),
                School = new(Constants.CampbellsvilleUniversity)
            },
        ];
        List<Graduate> graduates =
        [
            ..gradsWithValidMajors,
            new Graduate()
            {
                Degree = new Degree(new("Economics"), DegreeType.BS),
                School = new(Constants.UniversityOfPortland)
            },
            new Graduate()
            {
                Degree = new Degree(new("Political Science"), DegreeType.BS),
                School = new(Constants.UniversityOfLouisville)
            },
            
            new Graduate()
            {
                Degree = new Degree(new("Biology"), DegreeType.BS),
                School = new(Constants.CampbellsvilleUniversity)
            },
        ];
        
        _graduateRepository
            .GetGraduatesAsync()
            .Returns(graduates);

        // act
        var results = await _graduateService.SaveGraduatesWithValidMajors(graduates);

        // assert
        Assert.That(results.Count, Is.EqualTo(gradsWithValidMajors.Count));
        Assert.That(results, Is.EqualTo(gradsWithValidMajors));
        
        await _graduateRepository
            .ReceivedWithAnyArgs(1)
            .SaveGraduatesAsync(default!);
    }
    
    [Test]
    public async Task ShouldDeleteGradsWithInvalidMajors()
    {
        // arrange
        List<Graduate> gradsWithInvalidMajors =
        [
           new Graduate()
            {
                Degree = new Degree(new("Economics"), DegreeType.BS),
                School = new(Constants.UniversityOfPortland)
            },
            new Graduate()
            {
                Degree = new Degree(new("Political Science"), DegreeType.BS),
                School = new(Constants.UniversityOfLouisville)
            },
            
            new Graduate()
            {
                Degree = new Degree(new("Biology"), DegreeType.BS),
                School = new(Constants.CampbellsvilleUniversity)
            },

        ];
        List<Graduate> graduates =
        [
            ..gradsWithInvalidMajors,
            new Graduate()
            {
                Degree = new Degree(new("Biology"), DegreeType.BS),
                School = new(Constants.UniversityOfPortland)
            },
            new Graduate()
            {
                Degree = new Degree(new("Computer Science"), DegreeType.BS),
                School = new(Constants.UniversityOfLouisville)
            },
            
            new Graduate()
            {
                Degree = new Degree(new("Sociology"), DegreeType.BS),
                School = new(Constants.CampbellsvilleUniversity)
            },
        ];
        
        _graduateRepository
            .GetGraduatesAsync()
            .Returns(graduates);

        // act
        var results = await _graduateService.DeleteGraduatesWithInvalidMajors(graduates);

        // assert
        Assert.That(results.Count, Is.EqualTo(gradsWithInvalidMajors.Count));
        Assert.That(results, Is.EqualTo(gradsWithInvalidMajors));
        
        await _graduateRepository
            .ReceivedWithAnyArgs(1)
            .DeleteGraduatesAsync(default!);
    }
}