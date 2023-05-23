using System.Reflection;

namespace TextProcessing
{
    public class Program
    {
        private static string _nameAssembly = "TextParser.dll";
        private static string _fullNameClass = "TextParser.UniqWordsCounter";
        private static string _nameMethod = "GetAllUniqWordsInText";
        static void Main(string[] args)
        {
            MethodInfo? getAllUniqWords = GetMethodFromAssembly();
          
            Console.WriteLine("Введите полный путь к текстовому файлу:");
            string path = Console.ReadLine();
            try
            {
                TxtFileReader txtFileReader = new TxtFileReader(path);
                string text = txtFileReader.ReadFile();
                Dictionary<string, int> allUniqWords = getAllUniqWords.Invoke(null, new object[] { text }) as Dictionary<string, int>;
                TxtFileWriter txtFileWriter = new TxtFileWriter("TextStatistics.txt");
                txtFileWriter.WriteToFileStatistics(allUniqWords);
                PrintInfo();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }

            Console.ReadLine();
        }

        private static MethodInfo? GetMethodFromAssembly()
        {
            Assembly asm = Assembly.LoadFrom(_nameAssembly);
            Type? type = asm.GetType(_fullNameClass);
            MethodInfo? getAllUniqWords = type?.GetMethod(_nameMethod, BindingFlags.NonPublic | BindingFlags.Static);
            return getAllUniqWords;
        }

        private static void PrintInfo()
        {
            Console.WriteLine("Количество всех уникальных слов подсчитано!" +
                                "\nТекстовый файл со статистикой находится в папке Debug данного проекта." +
                                "\nНажмите любую кнопку для выхода");
        }
    }
}




