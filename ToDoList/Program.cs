

namespace ToDoList
{       
    class Program
    {        
        static void Main(string[] args)
        {
            List<Task> taskList = new List<Task>();
            Task task = new Task(); 
            Task.DisplayIntro();
            string output = "(Choose a file path)";

            try { 
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
                    
                    List<Task> listSortedByDate = taskList.OrderBy(t => t.DueDate).ToList(); //Sort by date
                    //List<Task> listSortedByProject = taskList.OrderBy(t => t.Project).ToList(); //Sort by project

                    foreach (Task t in listSortedByDate)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine(t.Print());
                        Console.ResetColor();
                    }
                }
                else
                {                    
                    Console.WriteLine("No tasks were added to the list.");                    
                }
            }

            void GetMainMenu() 
            {                
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(">> Pick an option:");
                Console.WriteLine(">> 1: Display the task list.");
                Console.WriteLine(">> 2: Create a ToDo list.");
                Console.WriteLine(">> 3: Add another task to the current list.");
                Console.WriteLine(">> 4: Mark an existing task as done. (Under construction)");
                Console.WriteLine(">> 5: Remove a task from the list.");
                Console.WriteLine(">> 6: Save and quit.");
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
                                FileManager.ReadFile();                                
                                GetMainMenu();
                            }
                            else
                            {
                                Console.WriteLine();
                                Console.ForegroundColor= ConsoleColor.Yellow;
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
                                MarkAsDone();                                
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
                                DeleteTask();                                
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
                    saveText.WriteLine(listedTask.Print());
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
                    saveText.WriteLine(listedTask.Print());
                }
                saveText.Close();
            }            

            void DeleteTask()
            {
                Console.WriteLine("Enter the title of the task you want to delete:");
                string input = Console.ReadLine();
                
                System.IO.StreamReader file = new System.IO.StreamReader(FileManager.filePath);
                string line = file.ReadLine();                

                while ((line = file.ReadLine()) != null)
                {
                    if (line.Contains(input))
                    {
                        Console.WriteLine(line + " was found.");
                        Console.WriteLine("Please confirm that you want to delete the task (Y/N).");
                        string confirmation = Console.ReadLine();

                        if (confirmation.Equals("y", StringComparison.OrdinalIgnoreCase))
                        {                            
                            string[] lines = File.ReadAllLines(FileManager.filePath);
                            StreamWriter saveText = new StreamWriter(output);                            
                            
                            foreach (string s in lines) 
                            {                                
                                if (!s.Contains(input))
                                { 
                                    saveText.WriteLine(s); 
                                }                                
                            }                            
                            saveText.Close();                            
                            Console.WriteLine("A new task list was created without the deleted task.");                            
                        }
                        else
                        {
                            Console.WriteLine("The task was not deleted.");
                        }
                    }                                       
                }                
            }

            //Method(s) under construction
            void MarkAsDone()
            {
                Console.WriteLine("Enter the title of a task:");
                var input = Console.ReadLine();

                Console.WriteLine("Under construction.");

                if (!string.IsNullOrEmpty(input))
                {
                    System.IO.StreamReader file = new System.IO.StreamReader(FileManager.filePath);

                    string line = file.ReadLine();
                    string[] lines = File.ReadAllLines(FileManager.filePath);

                    while ((line = file.ReadLine()) != null)
                    {
                        StreamWriter saveText = new StreamWriter(output);
                        foreach (string s in lines)
                        {
                            if (s.Contains(input))
                            {
                                //Update the task status to "Done"
                                saveText.WriteLine(s);
                            }
                        }
                        saveText.Close();
                        Console.WriteLine("An updated task list was created.");
                    }
                }
                else
                {
                    Console.WriteLine("No title was entered.");
                }
            }
        }        
    }    
}