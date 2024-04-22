
using System.Text.RegularExpressions;

namespace ToDoList
{
    public static class FileManager
    {        
        public static string filePath = (Choose a file path);            
        public static List<Task> taskList = new List<Task>();        

        public static void CreateFile()
        {
            using (StreamWriter sw = new StreamWriter(File.Open(filePath, System.IO.FileMode.Append)))
            {
                foreach (Task line in taskList)
                {
                    Task task = (Task)line;
                    sw.WriteLine(task.ID);
                    sw.WriteLine(task.Title);
                    sw.WriteLine(task.DueDate);
                    sw.WriteLine(task.Status);
                    sw.WriteLine(task.Project);
                }
            }
        }       

        public static void GetCompleted()
        {
            try
            {
                var word = new[] { "Done" };
                var regex = new Regex(@"\b(?:" + String.Join("|", word) + @")\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                var count = File.ReadLines(filePath).Select(l => regex.Matches(l).Count).Sum();
                Console.WriteLine("The number of completed tasks is {0}", count);
            }
            catch
            {
                Console.WriteLine("No completed tasks were found.");
            }
        }

        public static void GetNotStarted()
        {
            try
            {
                var word = new[] { "Not started" };
                var regex = new Regex(@"\b(?:" + String.Join("|", word) + @")\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                var count = File.ReadLines(filePath).Select(l => regex.Matches(l).Count).Sum();
                Console.WriteLine("The number of not started tasks is {0}", count);
            }
            catch
            {
                Console.WriteLine("No unfinished tasks were found.");
            }
        }
        public static void GetOngoing()
        {
            try
            {
                var word = new[] { "Ongoing" };
                var regex = new Regex(@"\b(?:" + String.Join("|", word) + @")\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                var count = File.ReadLines(filePath).Select(l => regex.Matches(l).Count).Sum();
                Console.WriteLine("The number of ongoing tasks is {0}", count);
            }
            catch
            {
                Console.WriteLine("No overdue tasks were found.");
            }
        }

        public static void GetOverdue()
        {
            try
            {
                var word = new[] { "Overdue" };
                var regex = new Regex(@"\b(?:" + String.Join("|", word) + @")\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                var count = File.ReadLines(filePath).Select(l => regex.Matches(l).Count).Sum();
                Console.WriteLine("The number of overdue tasks is {0}", count);
            }
            catch
            {
                Console.WriteLine("No overdue tasks were found.");
            }
        }              

        public static void PrintHeader() 
        {
            Console.WriteLine("Title: ".PadRight(25) + "Due Date: ".PadRight(25) + "Status: ".PadRight(25) + "Project: ");
            Console.WriteLine("___________________________________________________________________________________");
        }

        public static void PrintFooter()
        {            
            Console.WriteLine("___________________________________________________________________________________");
            Console.WriteLine();
        }        
    }
}
