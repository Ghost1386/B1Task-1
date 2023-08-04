using System.Text;
using B1Task_1.Enums;

namespace B1Task_1.Services;

public class FileService
{
    private const string DefaultPath = "C:/Users/egor5/OneDrive/Рабочий стол/B1Task-1/B1Task-1/B1Task-1/Files/";
    private const string TitleForMergeFile = "merged.txt";
    private const int NumberOfFiles = 100;
    private const int NumberOfLines = 100000;
    
    public async Task<string> CreateFilesAndWrite()
    {
        var generatorService = new GeneratorService();

        for (int i = 1; i <= NumberOfFiles; i++)
        {
            var stringBuilder = new StringBuilder();
                
            for (int j = 1; j <= NumberOfLines; j++)
            {
                stringBuilder.AppendLine(generatorService.Generate());
            }
                
            await using (var writer = new StreamWriter(DefaultPath + $"{i}.txt"))
            {
                await writer.WriteAsync(stringBuilder.ToString());
            }
        }

        return "Файлы успешно сгенерированы";
    }

    public async Task<int> MergeGeneratedFiles(TypeMerge typeMerge, string removedText)
    {
        var numberOfIterations = 0;
        
        if (typeMerge == TypeMerge.WithRemoval)
        {
            for (var i = 1; i <= NumberOfFiles; i++)
            {
                var textInFile = File.ReadLines(DefaultPath + $"{i}.txt").ToList();
                
                for (int j = 0; j < textInFile.Count; j++)
                {
                    if (textInFile[j].Contains(removedText))
                    {
                        textInFile.RemoveAt(j);

                        numberOfIterations++;
                    }
                }

                foreach (var line in textInFile)
                {
                    await using (var writer = new StreamWriter(DefaultPath + $"{i}.txt"))
                    {
                        await writer.WriteLineAsync(line);
                    }
                }
            }
        }
        
        await using (var outputStream = File.Create(DefaultPath + TitleForMergeFile))
        {
            for (var i = 1; i <= NumberOfFiles; i++)
            {
                await using (var inputStream = File.OpenRead(DefaultPath + $"{i}.txt"))
                {
                    await inputStream.CopyToAsync(outputStream);
                }
                
                Console.WriteLine($"Файл {i} импортирован.");
            }
        }

        return numberOfIterations;
    }

    public List<string> GetAllTextInFiles()
    {
        var textInFile = File.ReadLines(DefaultPath + TitleForMergeFile).ToList();

        return textInFile;
    }
}