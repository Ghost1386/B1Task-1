using System.ComponentModel.DataAnnotations;

namespace B1Task_1.Models;

public class Data
{
    [Key]
    public int Id { get; set; }
    
    public string Date { get; set; }
    
    public string English { get; set; }
    
    public string Russian { get; set; }
    
    public int Integer { get; set; }
    
    public double Fractional { get; set; }
}