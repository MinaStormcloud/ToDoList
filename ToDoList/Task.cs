
using System.Globalization;
using System.Text.RegularExpressions;

namespace ToDoList
{
    public class Task
    {            
        public string Title { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
        public string Project { get; set; }

        public Task()
        {

        }

        public Task(string title, DateTime dueDate, string status, string project)
        {
            Title = title;
            DueDate = dueDate;
            Status = status;
            Project = project;
        }        

        public static void DisplayIntro()
        {
            Console.WriteLine(">> Welcome to the ToDo list!");
            Console.WriteLine();
            FileManager.GetCompleted();
            FileManager.GetOngoing();
            FileManager.GetNotStarted();
            FileManager.GetOverdue();
            Console.WriteLine();
        }

        public void DisplayErrorMessage()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Wrong input. Please try again.");
            Console.ResetColor();
        }

        public string Print()
        {
            return "Title: " + Title.PadRight(15) + " " + "Due Date: " + DueDate.ToShortDateString().PadRight(15) + " " +
                "Status: " + Status.PadRight(15) + " " + "Project: " + Project;
        }

        public Task CreateTask()
        {
            Task task = new Task();                         

            while (true)
            {
                Console.WriteLine("\nEnter Task Title: ");
                Title = Console.ReadLine();                

                if (string.IsNullOrEmpty(Title) || Regex.IsMatch(Title, @"^\d+$"))
                {
                    DisplayErrorMessage();
                }
                else
                {
                    break;
                }
            }

            try
            {
                while (true)
                {
                    Console.WriteLine("\nEnter Task Due Date: ");
                    DueDate = Convert.ToDateTime(Console.ReadLine());
                    string s = task.DueDate.ToString();
                    bool dateOK = DateTime.TryParseExact(s, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                            DateTimeStyles.AssumeUniversal, out DateTime dt);

                    if (string.IsNullOrEmpty(s))
                    {
                        DisplayErrorMessage();
                    }

                    else
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.StackTrace.ToString();
            }

            while (true)
            {
                Console.WriteLine("\nEnter Task Project: ");
                Project = Console.ReadLine();

                if (string.IsNullOrEmpty(Project) || Regex.IsMatch(Project, @"^\d+$"))
                {
                    DisplayErrorMessage();
                }
                else
                {
                    break;
                }
            }

            while (true)
            {
                Console.WriteLine("\nEnter Task Status (Done, Ongoing, Not started, Overdue): ");
                Status = Console.ReadLine();

                if (string.IsNullOrEmpty(Status) || Regex.IsMatch(Status, @"^\d+$"))
                {
                    DisplayErrorMessage();
                }
                else if (Status.Equals("Done", StringComparison.OrdinalIgnoreCase) ||
                        Status.Equals("Ongoing", StringComparison.OrdinalIgnoreCase) ||
                        Status.Equals("Not started", StringComparison.OrdinalIgnoreCase) ||
                        Status.Equals("Overdue", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }
                else
                {
                    DisplayErrorMessage();
                }
            }    
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("The task was added to the list.");
            Console.ResetColor();
            return task;
        }        
    }    
}
