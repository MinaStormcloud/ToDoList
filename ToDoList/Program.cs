
namespace ToDoList
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Task> taskList = new List<Task>();            
            Task task = new Task();

            List<Task> listFromFile = new List<Task>();
            int id;
            string title;
            DateTime dueDate;
            string status;
            string project;
            LoadFile();
            Task.DisplayIntro();            

            try
            {
                GetMainMenu();
            }
            catch (Exception e)
            {
                e.StackTrace.ToString();
            }

            void AddContent()
            {
                Console.WriteLine("Please add a task or write 'exit' to quit");

                while (true)
                {
                    var listInput = Console.ReadLine();

                    if (listInput.Equals("quit", StringComparison.OrdinalIgnoreCase) ||
                        listInput.Equals("exit", StringComparison.OrdinalIgnoreCase))
                    {
                        break;
                    }

                    else
                    {
                        FileManager.CreateFile();
                        SaveTask();
                    }
                }

                if (taskList.Count > 0)
                {
                    Console.WriteLine("");
                    Console.WriteLine("The list contains " + taskList.Count + " tasks.");
                    Console.WriteLine("The following tasks were added to the ToDo list:");
                    FileManager.PrintHeader();
                    List<Task> listSortedByDate = taskList.OrderBy(t => t.DueDate).ToList();                     

                    foreach (Task t in listSortedByDate)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine(t.Print());
                        Console.ResetColor();
                    }

                    FileManager.PrintFooter();
                }
                else
                {
                    Console.WriteLine("No tasks were added to the list.");
                }
            }

            void GetMainMenu()
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("MAIN MENU:");
                Console.WriteLine(">> Pick an option:");
                Console.WriteLine(">> 1: Display the task list.");
                Console.WriteLine(">> 2: Create a ToDo list.");
                Console.WriteLine(">> 3: Add another task to the current list.");
                Console.WriteLine(">> 4: Edit an existing task.");
                Console.WriteLine(">> 5: Remove a task from the list.");                
                Console.WriteLine(">> 6: Sort tasks by date.");
                Console.WriteLine(">> 7: Sort tasks by project.");
                Console.WriteLine(">> 8: Save and quit.");
                Console.ResetColor();

                bool knownKeyPressed = false;

                do
                {
                    ConsoleKeyInfo keyRead = Console.ReadKey();

                    switch (keyRead.Key)
                    {
                        case ConsoleKey.NumPad1:

                            if (File.Exists(FileManager.filePath))
                            {
                                Console.Clear();
                                FileManager.PrintHeader();                                                               
                                PrintFile();
                                FileManager.PrintFooter();
                                GetMainMenu();
                            }
                            else
                            {
                                Console.WriteLine();
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("There is no such file.");
                                Console.ResetColor();
                            }
                            knownKeyPressed = true;
                            break;

                        case ConsoleKey.NumPad2:
                            Console.Clear();
                            AddContent();
                            GetMainMenu();
                            knownKeyPressed = true;
                            break;

                        case ConsoleKey.NumPad3:
                            Console.WriteLine();

                            if (File.Exists(FileManager.filePath))
                            {
                                Console.Clear();
                                AddNewTask();
                                GetMainMenu();
                            }
                            else
                            {
                                Console.WriteLine();
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("There is no file to edit.");
                                Console.ResetColor();
                            }
                            knownKeyPressed = true;
                            break;

                        case ConsoleKey.NumPad4:
                            Console.WriteLine();

                            if (File.Exists(FileManager.filePath))
                            {
                                Console.Clear();
                                EditByID();                                                               
                                GetMainMenu();
                            }
                            else
                            {
                                Console.WriteLine();
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("There is no file to edit.");
                                Console.ResetColor();
                            }
                            knownKeyPressed = true;
                            break;

                        case ConsoleKey.NumPad5:
                            Console.WriteLine();

                            if (File.Exists(FileManager.filePath))
                            {
                                Console.Clear();
                                DeleteByID();
                                GetMainMenu();
                            }
                            else
                            {
                                Console.WriteLine();
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("There is no file to remove.");
                                Console.ResetColor();
                            }
                            knownKeyPressed = true;
                            break;                        

                        case ConsoleKey.NumPad6:
                            Console.WriteLine();

                            if (File.Exists(FileManager.filePath))
                            {
                                Console.Clear();
                                FileManager.PrintHeader();
                                SortByDate();
                                FileManager.PrintFooter();
                                GetMainMenu();
                            }
                            else
                            {
                                Console.WriteLine();
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("There is no such file.");
                                Console.ResetColor();
                            }                            
                            knownKeyPressed = true;
                            break;

                        case ConsoleKey.NumPad7:
                            Console.WriteLine();
                            if (File.Exists(FileManager.filePath))
                            {
                                Console.Clear();
                                FileManager.PrintHeader();
                                SortByProject();
                                FileManager.PrintFooter();
                                GetMainMenu();
                            }
                            else
                            {
                                Console.WriteLine();
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("There is no such file.");
                                Console.ResetColor();
                            }
                            knownKeyPressed = true;
                            break;

                        case ConsoleKey.NumPad8:
                            Console.WriteLine();
                            Console.WriteLine("Goodbye, and thanks for all the fish!");
                            knownKeyPressed = true;
                            break;

                        default:
                            Console.WriteLine("Wrong key, please try again.");
                            knownKeyPressed = false;
                            break;
                    }
                } while (!knownKeyPressed);
            }

            void SaveTask()
            {
                Task t = new Task();
                t.CreateTask();
                taskList.Add(t);
                StreamWriter saveText = new StreamWriter(FileManager.filePath);

                foreach (Task listedTask in taskList)
                {
                    saveText.WriteLine(listedTask.Save());
                }
                saveText.Close();
            }

            void AddNewTask()
            {
                Task t = new Task();
                t.CreateTask();
                taskList.Add(t);
                StreamWriter saveText = new StreamWriter(FileManager.filePath, true);

                foreach (Task listedTask in taskList)
                {
                    saveText.WriteLine(listedTask.Save());
                }
                saveText.Close();
            }

            void LoadFile()
            {
                string file = FileManager.filePath;
                int lineNumber = 1;

                try
                {
                    using (StreamReader sr = new StreamReader(file))
                    {
                        string line;

                        while ((line = sr.ReadLine()) != null)
                        {
                            string[] parts = line.Split(",");
                            id = lineNumber;
                            title = parts[0].Trim();
                            dueDate = DateTime.Parse(parts[1].Trim());
                            status = parts[2].Trim();
                            project = parts[3].Trim();

                            listFromFile.Add(new Task(id, title, dueDate, status, project));
                            lineNumber++;                            
                        }                        
                    }
                }
                catch (Exception ex)
                {
                    ex.StackTrace.ToString();
                }
            }

            void PrintFile() 
            {
                ResetTmpList();
                LoadFile();
                foreach (Task t in listFromFile)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine(t.Print());
                    Console.ResetColor();
                }
            }

            void ResetTmpList() 
            { 
                listFromFile.Clear();
            }

            void SortByID()
            {
                try
                {                    
                    var sortedList = listFromFile.OrderBy(task => task.ID).ToList();                    

                    foreach (var task in sortedList)
                    {                        
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine($"ID: {task.ID}, Title: {task.Title}, Due: {task.DueDate.ToString("yyyy-MM-dd")}, Status: {task.Status}, Project: {task.Project}");
                        Console.ResetColor();                        
                    }
                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    ex.StackTrace.ToString();
                }
            }

            void SortByDate()
            {
                try
                {
                    ResetTmpList();
                    LoadFile();
                    var sortedList = listFromFile.OrderBy(task => task.DueDate).ToList();

                    foreach (Task t in sortedList)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine(t.Print());
                        Console.ResetColor();
                    }                    
                }
                catch (Exception ex)
                {
                    ex.StackTrace.ToString();
                }
            }

            void SortByProject()
            {
                try
                {
                    ResetTmpList();
                    LoadFile();
                    var sortedList = listFromFile.OrderBy(task => task.Project).ToList();

                    foreach (Task t in sortedList)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine(t.Print());
                        Console.ResetColor();
                    }                    
                }
                catch (Exception ex)
                {
                    ex.StackTrace.ToString();
                }
            } 
            
            void EditByID() 
            {
                bool active = true;

                while (active)
                {
                    SortByID();

                    Console.Write("Update ID (Press 'Q' to exit): ");
                    string userInput = Console.ReadLine();

                    if (userInput.ToLower().Trim() == "q")
                    {
                        GetMainMenu();
                    }
                    
                    if (!int.TryParse(userInput, out int id))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input. Please enter a valid ID.");
                        Console.ResetColor();
                        continue;
                    }

                    int currentId = id - 1;
                    
                    if (currentId < 0 || currentId >= listFromFile.Count)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"ID {id} is out of range.");
                        Console.ResetColor();
                        continue;
                    }
                    
                    try
                    {
                        string currentTitle = listFromFile[currentId].Title;
                        DateTime currentDueDate = listFromFile[currentId].DueDate;
                        string currentProject = listFromFile[currentId].Project;
                        string currentStatus = listFromFile[currentId].Status;

                        Console.Write("Change title (press Enter to keep the current title): ");
                        
                        string newTitle = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(newTitle))
                        {
                            newTitle = currentTitle;
                        }

                        DateTime newDueDate = DateTime.MinValue;
                        bool isValidDate = false;
                        
                        while (!isValidDate)
                        {
                            Console.Write("New due date (yyyy-MM-dd. Press Enter to keep the current due date): ");
                            string dueDateInput = Console.ReadLine();

                            if (string.IsNullOrWhiteSpace(dueDateInput))
                            {                                
                                newDueDate = currentDueDate;
                                isValidDate = true;
                            }
                            
                            else if (DateTime.TryParse(dueDateInput, out newDueDate))
                            {
                                isValidDate = true;
                            }
                            else
                            {                                
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"{dueDateInput} is not a valid date format (yyyy-MM-dd)");
                                Console.ResetColor();
                            }
                        }

                        Console.Write("Change project name (press Enter to keep the current project name): ");
                        string newProject = Console.ReadLine();
                        
                        if (string.IsNullOrWhiteSpace(newProject))
                        {
                            newProject = currentProject;
                        }

                        Console.Write("Change status (press Enter to keep the current status): ");
                        string newStatus = Console.ReadLine();
                        
                        if (string.IsNullOrWhiteSpace(newStatus))
                        {
                            newStatus = currentStatus;
                        }
                        
                        listFromFile.RemoveAt(currentId);
                        listFromFile.Add(new Task(currentId + 1, newTitle, newDueDate, newProject, newStatus));
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Task '{newTitle}' was updated successfully!");
                        SaveChanges();
                        Console.ResetColor();

                        active = false;
                    }
                    catch (ArgumentOutOfRangeException)
                    {                        
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"ID {id} not found.");
                        Console.ResetColor();
                    }
                }
            }

            void DeleteByID()
            {
                bool active = true;

                while (active)
                {                    
                    SortByID();

                    Console.Write("Enter ID for the task to remove (Press'Q' to exit): ");
                    string input = Console.ReadLine();
                    
                    if (input.ToLower().Trim() == "q")
                    {
                        GetMainMenu();
                    }
                    
                    if (Int32.TryParse(input, out int id))
                    {
                        if (id >= 1 && id <= listFromFile.Count)
                        {
                            try
                            {
                                listFromFile.RemoveAt(id - 1);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"Task with ID {id} removed successfully!");
                                Console.ResetColor();
                                SaveChanges();
                                active = false;
                            }
                            catch (ArgumentOutOfRangeException)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"ID {id} not found.");
                                Console.ResetColor();
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"ID {id} is out of range.");
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input. Please enter a valid ID.");
                        Console.ResetColor();
                    }
                }
                
            }

            void SaveChanges() 
            {
                try 
                {
                    StreamWriter saveText = new StreamWriter(FileManager.filePath, false);

                    foreach (Task listedTask in listFromFile)
                    {
                        saveText.WriteLine(listedTask.Save());
                    }
                    saveText.Close();
                    ResetTmpList();
                    LoadFile();
                }
                catch (Exception ex) 
                { 
                    ex.ToString();
                }                
            }            
        }
    }
}
