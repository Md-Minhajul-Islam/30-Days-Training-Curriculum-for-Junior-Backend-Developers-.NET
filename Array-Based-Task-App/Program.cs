
using System.Globalization;
using System.Runtime.InteropServices;

public class AppTask
{
    public DateOnly RegisterDate;
    public DateOnly? StartDate;
    public DateOnly? EndDate;
    public string Content = string.Empty;
    public static string ErrorMessage =>  "Something Went Wrong, Try Again!";
   
    public static DateOnly? ParseDate(string date)
    {
        if (DateOnly.TryParseExact(
                date.Trim(),
                "yyyy-MM-dd",
                CultureInfo.InvariantCulture,
                DateTimeStyles.AllowWhiteSpaces,
                out DateOnly parsedDate))
        {
            return parsedDate;
        }
        return null;
    }
}


class Program
{
    static void Main(string[] args)
    {

        List<AppTask> taskList = new List<AppTask>();

        while (true)
        {
            if(taskList.Count > 0)
            {
                Console.WriteLine("All Tasks:");
                
                Console.WriteLine("\nTask no. | Registration Date | Description | Start Date | End Date \n");

                for(int i = taskList.Count-1, j = 1; i >= 0; i--, j++)
                {
                    Console.WriteLine($"{j}\t{taskList[i].RegisterDate}\t{taskList[i].Content}\t{taskList[i].StartDate}\t{taskList[i].EndDate}");
                }
            }
            else
            {
                Console.WriteLine("No Task Available!");
            }

            Console.Write("\nAdd Task? y/n: ");

            string? choose = Console.ReadLine();

            if(choose?.Trim() == "y")
            {
                Console.WriteLine("Enter Task Details:");

                Console.Write("Enter Task: ");
                string? content = Console.ReadLine();

                Console.Write("Enter Starting Day (yyyy-MM-dd): ");
                string? startDate = Console.ReadLine();

                Console.Write("Enter Ending Day (yyyy-MM-dd): ");
                string? endDate = Console.ReadLine();

                if(string.IsNullOrWhiteSpace(content) || string.IsNullOrWhiteSpace(startDate) || string.IsNullOrWhiteSpace(endDate))
                {

                    Console.WriteLine(AppTask.ErrorMessage);

                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();

                    continue;
                }

                DateOnly? StartDate = AppTask.ParseDate(startDate!);
                
                DateOnly? EndDate = AppTask.ParseDate(endDate!);

                if(StartDate == null || EndDate == null || StartDate > EndDate)
                {
                    Console.WriteLine(AppTask.ErrorMessage);

                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();

                    continue;
                }

                AppTask newTask = new AppTask
                {
                    RegisterDate = DateOnly.FromDateTime(DateTime.Today),
                    StartDate = StartDate,
                    EndDate = EndDate,
                    Content = content
                };
                
                taskList.Add(newTask);

                Console.WriteLine("Task Added Successfuly!");
            }
            else
            {
                Console.WriteLine("Exiting.........");
                break;
            }
        }
    }
}