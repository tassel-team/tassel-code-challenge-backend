namespace GraduateProcessor.API.Models;

public class Graduate
{
    public string FullName { get; set; }
    
    public Degree Degree { get; set; }
    
    public int Year { get; set; }
    
    public Term Term { get; set; }
    
    public School School { get; set; }
}