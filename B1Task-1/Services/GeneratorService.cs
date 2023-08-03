using System.Text;

namespace B1Task_1.Services;

public class GeneratorService
{
    private const string EnglishLetters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string RussianLetters = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
    private const int IntegerMinValue = 1;
    private const int EvenIntegerMaxValue = 100000000;
    private const int MaxIntegerPartOfNumber = 20;
    private const int MinFractionalPartOfNumber = 10000001;
    private const int MaxFractionalPartOfNumber = 99999999;

    public string Generate()
    {
        var resultStr = $"{GenerateDate(DateTime.Now.AddYears(-5), DateTime.Now)}||" +
                            $"{GenerateLetters(10, 0)}||{GenerateLetters(10, 1)}||" +
                            $"{GenerateEvenInteger()}||{GenerateDouble()}";

        return resultStr;
    }
    
    private static string GenerateDate(DateTime startDate, DateTime endDate)
    {
        var rnd = new Random();
        
        var randomYear = rnd.Next(startDate.Year, endDate.Year);
        var randomMonth = rnd.Next(1, 12) ;
        var randomDay = rnd.Next(1, DateTime.DaysInMonth(randomYear, randomMonth));

        if (randomYear == startDate.Year)
        {
            randomMonth = rnd.Next(startDate.Month, 12);

            if (randomMonth == startDate.Month)
            {
                randomDay = rnd.Next(startDate.Day, DateTime.DaysInMonth(randomYear, randomMonth));
            }
        }

        if (randomYear == endDate.Year)
        {
            randomMonth = rnd.Next(1, endDate.Month);

            if (randomMonth == endDate.Month)
            {
                randomDay = rnd.Next(1, endDate.Day);
            }
        }

        var generateDate = $"{randomDay}.{randomMonth}.{randomYear}";

        return generateDate;
    }
    
    private static string GenerateLetters(int length, int type)
    {
        var valid = type == 0 ? EnglishLetters : RussianLetters;

        var result = new StringBuilder();
        var rnd = new Random();
                
        while (0 < length--)
        {
            result.Append(valid[rnd.Next(valid.Length)]);
        }
                
        return result.ToString();
    }

    private static string GenerateEvenInteger()
    {
        var rnd = new Random();
 
        var number = 0;
        
        do
        {
            number = rnd.Next(IntegerMinValue, EvenIntegerMaxValue);
        }
        while(number % 2 != 0);
 
        return number.ToString();
    }

    private static string GenerateDouble()
    {
        return $"{GenerateInteger(1, MaxIntegerPartOfNumber)}" + "." +
               $"{GenerateInteger(MinFractionalPartOfNumber, MaxFractionalPartOfNumber)}";
    }

    private static string GenerateInteger(int minValue, int maxValue)
    {
        var rnd = new Random();

        return rnd.Next(minValue, maxValue).ToString();
    } 
}