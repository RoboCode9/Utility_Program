using System;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;

class Program
{
    // P/Invoke declarations to control the console window
    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    private const int SW_HIDE = 0;
    private const int SW_SHOW = 5;
    private const int SW_RESTORE = 9;

    [STAThread]

    static void Main()
    {
        int choice = 0;

        while (choice != 15)
        {
            Console.WriteLine("Please choose an option:");
            Console.WriteLine("1. Convert inches to centimeters");
            Console.WriteLine("2. Convert centimeters to inches");
            Console.WriteLine("3. Convert Fahrenheit to Celsius");
            Console.WriteLine("4. Convert Celsius to Fahrenheit");
            Console.WriteLine("5. Generate password");
            Console.WriteLine("6. Convert kilograms to pounds");
            Console.WriteLine("7. Convert pounds to kilograms");
            Console.WriteLine("8. Convert centimeters to feet");
            Console.WriteLine("9. Convert feet to centimeters");
            Console.WriteLine("10. Convert diameter in centimeters to circumference in centimeters");
            Console.WriteLine("11. Convert diameter in inches to circumference in inches");
            Console.WriteLine("12. Perform Arithmatic");
            Console.WriteLine("13. Screenshot and save locally program");
            Console.WriteLine("14. Screenshot to clipboard program");
            Console.WriteLine("15. Quit");

            bool isValidChoice = int.TryParse(Console.ReadLine(), out choice);

            if (!isValidChoice || choice < 1 || choice > 15)
            {
                Console.WriteLine("Invalid choice. Please try again.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    ConvertInchesToCentimeters();
                    break;
                case 2:
                    ConvertCentimetersToInches();
                    break;
                case 3:
                    ConvertFahrenheitToCelsius();
                    break;
                case 4:
                    ConvertCelsiusToFahrenheit();
                    break;
                case 5:
                    GeneratePassword();
                    break;
                case 6:
                    ConvertKilogramsToPounds();
                    break;
                case 7:
                    ConvertPoundsToKilograms();
                    break;
                case 8:
                    ConvertCentimetersToFeet();
                    break;
                case 9:
                    ConvertFeetToCentimeters();
                    break;
                case 10:
                    ConvertDiameterToCircumferenceCentimeters();
                    break;
                case 11:
                    ConvertDiameterToCircumferenceInches();
                    break;
                case 12:
                    PerformArithmaticOperation();
                    break;
                case 13:
                    screenshotv1();
                    break;
                case 14:
                    screenshotv2();
                    break;
                case 15:
                    Console.WriteLine("Exiting utility program");
                    break;
            }
        }
    }

    static void screenshotv1()
    {
        Console.Clear();
        int screenshotCounter = 1; // Initialize the counter
        Console.WriteLine("Press 1 to screenshot and save locally, press 2 to quit");
        while (true)
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(intercept: true).Key;

                if (key == ConsoleKey.D1)
                {
                    // Minimize the application window
                    var hWnd = NativeMethods.GetConsoleWindow();
                    NativeMethods.ShowWindow(hWnd, NativeMethods.SW_MINIMIZE);

                    // Delay to allow the window to minimize
                    System.Threading.Thread.Sleep(500); // You can adjust the delay as needed

                    // Capture a screenshot
                    using (Bitmap screenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height))
                    {
                        using (Graphics g = Graphics.FromImage(screenshot))
                        {
                            g.CopyFromScreen(Point.Empty, Point.Empty, Screen.PrimaryScreen.Bounds.Size);
                        }

                        Console.Clear();
                        string screenshotPath = $"screenshot({screenshotCounter}).png";
                        screenshot.Save(screenshotPath);
                        Console.WriteLine($"Screenshot saved to: {screenshotPath}");
                        Console.WriteLine("Press 1 to screenshot and save locally, press 2 to quit");
                        screenshotCounter++; // Increment the counter
                    }

                    // Restore the application window
                    NativeMethods.ShowWindow(hWnd, NativeMethods.SW_RESTORE);
                }
                else if (key == ConsoleKey.D2)
                {
                    break; // Exit the program
                }
            }
        }
    }

    static void screenshotv2()
    {
        Console.WriteLine("Options:");
        Console.WriteLine("1 - Save screenshot to clipboard");
        Console.WriteLine("2 - Exit");

        while (true)
        {
            Console.Write("Select an option: ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Clear();
                    // Minimize the application window
                    var hWnd = NativeMethods.GetConsoleWindow();
                    NativeMethods.ShowWindow(hWnd, NativeMethods.SW_MINIMIZE);

                    // Delay to allow the window to minimize
                    System.Threading.Thread.Sleep(500); // You can adjust the delay as needed

                    // Capture a screenshot
                    Bitmap screenshot = CaptureScreen();

                    // Save the screenshot to the clipboard
                    Clipboard.SetImage(screenshot);

                    // Show the console window
                    NativeMethods.ShowWindow(hWnd, NativeMethods.SW_RESTORE);

                    Console.WriteLine("Screenshot saved to clipboard.");
                    Console.WriteLine("Options:");
                    Console.WriteLine("1 - Save screenshot to clipboard");
                    Console.WriteLine("2 - Exit");
                    break;

                case "2":
                    Console.Clear();
                    Console.WriteLine("Exiting save screen to clipboard program");
                    return;

                default:
                    Console.Clear();
                    Console.WriteLine("Invalid option. Please select 1 or 2.");
                    break;
            }
        }
    }

    private static Bitmap CaptureScreen()
    {
        Rectangle screenBounds = Screen.PrimaryScreen.Bounds;
        Bitmap screenshot = new Bitmap(screenBounds.Width, screenBounds.Height);
        using (Graphics gfx = Graphics.FromImage(screenshot))
        {
            gfx.CopyFromScreen(screenBounds.Location, Point.Empty, screenBounds.Size);
        }
        return screenshot;
    }

    static class NativeMethods
    {
        public const int SW_MINIMIZE = 6;
        public const int SW_RESTORE = 9;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        public static extern IntPtr GetConsoleWindow();
    }

    static void PerformArithmaticOperation()
    {
        Console.Clear();
        Console.WriteLine("Choose an arithmetic operation:");
        Console.WriteLine("1. Addition");
        Console.WriteLine("2. Subtraction");
        Console.WriteLine("3. Multiplication");
        Console.WriteLine("4. Division");
        int operationChoice;
        bool isValidOperationChoice = int.TryParse(Console.ReadLine(), out operationChoice);

        if (!isValidOperationChoice || operationChoice < 1 || operationChoice > 4)
        {
            Console.WriteLine("Invalid operation choice. Please try again.");
            return;
        }

        Console.WriteLine("Enter the first number:");
        double num1;
        bool isValidNum1 = double.TryParse(Console.ReadLine(), out num1);

        if (!isValidNum1)
        {
            Console.WriteLine("Invalid input for the first number. Please enter a valid number.");
            return;
        }

        Console.WriteLine("Enter the second number:");
        double num2;
        bool isValidNum2 = double.TryParse(Console.ReadLine(), out num2);

        if (!isValidNum2)
        {
            Console.WriteLine("Invalid input for the second number. Please enter a valid number.");
            return;
        }

        double result = 0;

        switch (operationChoice)
        {
            case 1:
                result = num1 + num2;
                Console.WriteLine($"{num1} + {num2} = {result}");
                break;
            case 2:
                result = num1 - num2;
                Console.WriteLine($"{num1} - {num2} = {result}");
                break;
            case 3:
                result = num1 * num2;
                Console.WriteLine($"{num1} * {num2} = {result}");
                break;
            case 4:
                if (num2 == 0)
                {
                    Console.WriteLine("Division by zero is not allowed.");
                }
                else
                {
                    result = num1 / num2;
                    Console.WriteLine($"{num1} / {num2} = {result}");
                }
                break;
        }
    }

    static void ConvertInchesToCentimeters()
    {
        Console.Clear();
        Console.WriteLine("Enter the number of inches:");
        double inches;
        bool isValidInput = double.TryParse(Console.ReadLine(), out inches);

        if (!isValidInput)
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
            return;
        }

        double centimeters = inches * 2.54;
        centimeters = Math.Round(centimeters, 2); // Limit decimal places to two
        Console.WriteLine($"{inches} inches is equal to {centimeters} centimeters.");
    }

    static void ConvertCentimetersToInches()
    {
        Console.Clear();
        Console.WriteLine("Enter the number of centimeters:");
        double centimeters;
        bool isValidInput = double.TryParse(Console.ReadLine(), out centimeters);

        if (!isValidInput)
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
            return;
        }

        double inches = centimeters / 2.54;
        inches = Math.Round(inches, 2); // Limit decimal places to two
        Console.WriteLine($"{centimeters} centimeters is equal to {inches} inches.");
    }

    static void ConvertFahrenheitToCelsius()
    {
        Console.Clear();
        Console.WriteLine("Enter the temperature in Fahrenheit:");
        double fahrenheit;
        bool isValidInput = double.TryParse(Console.ReadLine(), out fahrenheit);

        if (!isValidInput)
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
            return;
        }

        double celsius = (fahrenheit - 32) * 5 / 9;
        celsius = Math.Round(celsius, 2); // Limit decimal places to two
        Console.WriteLine($"{fahrenheit}°F is equal to {celsius}°C.");
    }

    static void ConvertCelsiusToFahrenheit()
    {
        Console.Clear();
        Console.WriteLine("Enter the temperature in Celsius:");
        double celsius;
        bool isValidInput = double.TryParse(Console.ReadLine(), out celsius);

        if (!isValidInput)
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
            return;
        }

        double fahrenheit = celsius * 9 / 5 + 32;
        fahrenheit = Math.Round(fahrenheit, 2); // Limit decimal places to two
        Console.WriteLine($"{celsius}°C is equal to {fahrenheit}°F.");
    }

    static void GeneratePassword()
    {
        Console.Clear();
        Console.WriteLine("Choose the length of the password (8, 10, 12, or 16):");
        int length;
        bool isValidLength = int.TryParse(Console.ReadLine(), out length);

        if (!isValidLength || (length != 8 && length != 10 && length != 12 && length != 16))
        {
            Console.WriteLine("Invalid password length. Please try again.");
            return;
        }

        const string uppercaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string lowercaseLetters = "abcdefghijklmnopqrstuvwxyz";
        const string digits = "0123456789";
        const string specialCharacters = "!@#$%^&*()";

        string validCharacters = string.Join("", uppercaseLetters, lowercaseLetters, digits, specialCharacters);
        Random random = new Random();
        string password = new string(Enumerable.Repeat(validCharacters, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());

        Console.WriteLine("Generated password: " + password);

        // Prompt the user to save the password
        Console.Write("Do you want to save this password to a text file? (yes/no): ");
        string saveChoice = Console.ReadLine().Trim().ToLower();

        if (saveChoice == "yes")
        {
            Console.Write("Enter the file name (without extension): ");
            string fileName = Console.ReadLine();

            try
            {
                // Save the password to a text file
                string filePath = fileName + ".txt";
                System.IO.File.WriteAllText(filePath, password);
                Console.WriteLine($"Password saved to {filePath}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while saving the password: {e.Message}");
            }
        }
        else
        {
            Console.WriteLine("Password not saved.");
        }
        Console.WriteLine("Remember to copy this password from the command prompt. Highlight the password and press (ctrl + c)!");
    }

    static void ConvertKilogramsToPounds()
    {
        Console.Clear();
        Console.WriteLine("Enter the weight in kilograms:");
        double kilograms;
        bool isValidInput = double.TryParse(Console.ReadLine(), out kilograms);

        if (!isValidInput)
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
            return;
        }

        double pounds = kilograms * 2.20462;
        pounds = Math.Round(pounds, 2); // Limit decimal places to two
        Console.WriteLine($"{kilograms} kilograms is equal to {pounds} pounds.");
    }

    static void ConvertPoundsToKilograms()
    {
        Console.Clear();
        Console.WriteLine("Enter the weight in pounds:");
        double pounds;
        bool isValidInput = double.TryParse(Console.ReadLine(), out pounds);

        if (!isValidInput)
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
            return;
        }

        double kilograms = pounds / 2.20462;
        kilograms = Math.Round(kilograms, 2); // Limit decimal places to two
        Console.WriteLine($"{pounds} pounds is equal to {kilograms} kilograms.");
    }

    static void ConvertCentimetersToFeet()
    {
        Console.Clear();
        Console.WriteLine("Enter the length in centimeters:");
        double centimeters;
        bool isValidInput = double.TryParse(Console.ReadLine(), out centimeters);

        if (!isValidInput)
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
            return;
        }

        double feet = centimeters / 30.48;
        double inches = (feet - Math.Floor(feet)) * 12;
        feet = Math.Floor(feet);

        Console.WriteLine($"{centimeters} centimeters is equal to {feet} feet and {inches:F2} inches.");
    }

    static void ConvertFeetToCentimeters()
    {
        Console.Clear();
        Console.WriteLine("Enter the length in feet:");
        double feet;
        bool isValidInput = double.TryParse(Console.ReadLine(), out feet);

        if (!isValidInput)
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
            return;
        }

        double centimeters = feet * 30.48;
        centimeters = Math.Round(centimeters, 2); // Limit decimal places to two
        Console.WriteLine($"{feet} feet is equal to {centimeters} centimeters.");
    }

    static void ConvertDiameterToCircumferenceCentimeters()
    {
        Console.Clear();
        Console.WriteLine("Enter the diameter in centimeters:");
        double diameter;
        bool isValidInput = double.TryParse(Console.ReadLine(), out diameter);

        if (!isValidInput)
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
            return;
        }

        double circumference = Math.PI * diameter;
        circumference = Math.Round(circumference, 2); // Limit decimal places to two
        Console.WriteLine($"The circumference of a circle with a diameter of {diameter} centimeters is {circumference} centimeters.");
    }

    static void ConvertDiameterToCircumferenceInches()
    {
        Console.Clear();
        Console.WriteLine("Enter the diameter in inches:");
        double diameter;
        bool isValidInput = double.TryParse(Console.ReadLine(), out diameter);

        if (!isValidInput)
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
            return;
        }

        double circumference = Math.PI * diameter;
        circumference = Math.Round(circumference, 2); // Limit decimal places to two
        Console.WriteLine($"The circumference of a circle with a diameter of {diameter} inches is {circumference} inches.");
    }
}
