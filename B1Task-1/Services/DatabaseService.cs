using System.Numerics;
using B1Task_1.Models;
using Microsoft.EntityFrameworkCore;

namespace B1Task_1.Services;

public class DatabaseService
{
    private const int NumberOfFiles = 100;
    private static readonly ApplicationContext applicationContext = new();

    public async Task ImportDataFromFiles()
    {
        var fileService = new FileService();
        
        for (int i = 84; i <= NumberOfFiles; i++)
        {
            var textInFiles = fileService.GetAllTextInFile(i);

            for (var j = 0; j < textInFiles.Count; j++)
            {
                var line = textInFiles[j].Split("||");

                var data = new Data
                {
                    Date = line[0],
                    English = line[1],
                    Russian = line[2],
                    Integer = int.Parse(line[3]),
                    Fractional = double.Parse(line[4])
                };
                
                
        
                await applicationContext.Data.AddAsync(data);

                Console.WriteLine($"Импортированно {j + 1} строк, ещё осталось {textInFiles.Count - 1 - j} в файле {i}");
            }
            
            await applicationContext.SaveChangesAsync();
        }
        
        Console.WriteLine("Все строки импортированны");
    }

    public async Task GetSumAndMedium()
    {
        var sum = await applicationContext.Data.SumAsync(d =>(long) d.Integer);
        
        Console.WriteLine($"Сумма всех целых чисел: {sum}");

        var listFractional = await applicationContext.Data.Select(d => d.Fractional).ToListAsync();
        
        listFractional.Sort();
        
        var midOfListFractional = listFractional.Count / 2;

        if (listFractional.Count % 2 != 0)
        {
            Console.WriteLine(listFractional[midOfListFractional]);
        }
        
        var medium = (listFractional[midOfListFractional] + listFractional[midOfListFractional - 1]) / 2;
        
        Console.WriteLine($"Медиана всех дробных чисел: {medium}");
    }
}