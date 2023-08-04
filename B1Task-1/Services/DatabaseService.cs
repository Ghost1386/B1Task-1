using B1Task_1.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace B1Task_1.Services;

public class DatabaseService
{
    private static readonly ApplicationContext applicationContext = new();

    public async Task ImportDataFromFiles()
    {
        var fileService = new FileService();

        var textInFiles = fileService.GetAllTextInFiles();

        for (var i = 0; i < textInFiles.Count / 100; i++)
        {
            var line = textInFiles[i].Split("||");

            var data = new Data
            {
                Date = line[0],
                English = line[1],
                Russian = line[2],
                Integer = int.Parse(line[3]),
                Fractional = double.Parse(line[4])
            };
        
            await applicationContext.Data.AddAsync(data);

            Console.WriteLine($"Импортированно {i + 1} строк, ещё осталось {textInFiles.Count - 1 - i}");
        }
        
        await applicationContext.SaveChangesAsync();
        
        Console.WriteLine("Все строки импортированны");
    }
}