using System.Data;
using Microsoft.Data.SqlClient;

namespace B1Task_1.Services;

public class DatabaseService
{
    private const string ConnectionStrings = "Server=localhost;Database=B1-1;" +
                                             "Trusted_Connection=True;TrustServerCertificate=True";

    public async void ImportDataFromFiles()
    {
        var fileService = new FileService();

        var textInFiles = fileService.GetAllTextInFiles();
        
        var sqlExpression = "INSERT INTO Data (Date, English, Russian, Integer, Fractional) VALUES ";

        for (int i = 0; i < textInFiles.Count; i++)
        {
            var line = textInFiles[i].Split("||");

            sqlExpression += $"('{line[0]}, {line[1]}, {line[2]}, {line[3]}'), ";

            Console.WriteLine($"Импортированно {i + 1} строк, ещё осталось {textInFiles.Count - 1 - i}");
        }
        
        Console.WriteLine("Осталось немного...");

        await using (var connection = new SqlConnection(ConnectionStrings))
        {
            await connection.OpenAsync();
 
            var command = new SqlCommand(sqlExpression, connection);

            await connection.CloseAsync();
            
            Console.WriteLine($"Добавлено объектов: {command.ExecuteNonQueryAsync()}");
        }
    }

    public async void SumAndMedian()
    {
        var sqlExpression = "sp_B1-1";
 
        await using (var connection = new SqlConnection(ConnectionStrings))
        {
            await connection.OpenAsync();
                 
            var command = new SqlCommand(sqlExpression, connection);
            
            command.CommandType = CommandType.StoredProcedure;
            
            await using (var reader = await command.ExecuteReaderAsync())
            {
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        var integer = reader.GetInt32(0);
                        var fractional = reader.GetDecimal(1);
                        
                        Console.WriteLine($"{integer}\n{fractional}");
                    }
                }
            }
        }
    }
}