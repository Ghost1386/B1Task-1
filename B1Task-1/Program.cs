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
                Console.WriteLine("Выберите команду:" +
                                  "1. Сгенерировать файлы" +
                                  "2. Обьеденить файлы" +
                                  "3. Обьеденить файлы с удалением" +
                                  "4. Импортировать даннные в СУБД" +
                                  "5. Вычеслить сумму и медиану");
                
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
                        databaseService.ImportDataFromFiles();
                        break;
                    case 5:
                        databaseService.SumAndMedian();
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