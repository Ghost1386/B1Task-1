using B1Task_1.Enums;
using B1Task_1.Services;

namespace B1Task_1
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            try
            {
                Console.WriteLine("Выберите команду:\n" +
                                  "1. Сгенерировать файлы\n" +
                                  "2. Обьеденить файлы\n" +
                                  "3. Обьеденить файлы с удалением\n" +
                                  "4. Импортировать даннные в СУБД\n" +
                                  "5. Вычеслить сумму и медиану\n");
                
                var result = int.Parse(Console.ReadLine());

                var fileService = new FileService();
                var databaseService = new DatabaseService();
                
                switch (result)
                {
                    case 1:
                        Console.WriteLine(await fileService.CreateFilesAndWrite());
                        break;
                    case 2:
                        Console.WriteLine(await fileService.MergeGeneratedFiles(TypeMerge.WithoutRemoval, 
                            string.Empty));
                        break;
                    case 3:
                        Console.WriteLine(await fileService.MergeGeneratedFiles(TypeMerge.WithRemoval, 
                            Console.ReadLine()));
                        break;
                    case 4:
                        await databaseService.ImportDataFromFiles();
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}