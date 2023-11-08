using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.IO;
using System.Text.Json;
using CsvHelper;
using MyCarProject;
using CsvHelper.Configuration;
using System.Globalization;
using System.Dynamic;

class Program
{
    static List<Car> cars = new List<Car>();
    static int maxCars;
    static string lastFileName = "cars.json";

    static void Main(string[] args)
    {
        Console.Write("Enter the maximum number of cars: ");
        if (int.TryParse(Console.ReadLine(), out maxCars))
        {
            while (true)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1 - Add Car");
                Console.WriteLine("2 - Display Cars");
                Console.WriteLine("3 - Search Cars");
                Console.WriteLine("4 - Delete Car");
                Console.WriteLine("5 - Denostrate Behavior");
                Console.WriteLine("6 - Save Collection to File");
                Console.WriteLine("7 - Load Collection from File");
                Console.WriteLine("8 - Clear Collection");
                Console.WriteLine("0 - Exit");

                int choice;
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            AddCar();
                            break;
                        case 2:
                            DisplayCars();
                            break;
                        case 3:
                            SearchCars();
                            break;
                        case 4:
                            DeleteCar();
                            break;
                        case 5:
                            DemonstrateBehavior();
                            break;
                        case 6:
                            SaveCollectionToFile();
                            break;
                        case 7:
                            LoadCollectionFromFile();
                            break;
                        case 8:
                            ClearCollection();
                            break;
                        case 0:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid menu option.");
                }
            }
        }
        else
        {
            Console.WriteLine("Invalid input for the maximum number of cars.");
        }
    }

    static void SaveCollectionToFile()
    {
        Console.WriteLine("\nSave Collection to File Menu:");
        Console.WriteLine("1 - Save to *.csv or *.txt");
        Console.WriteLine("2 - Save to *.json");
        Console.Write("Enter your choice: ");
        if (int.TryParse(Console.ReadLine(), out int choice))
        {
            if (choice == 1)
            {
                SaveToCsvOrTxt();
            }
            else if (choice == 2)
            {
                SaveToJson();
            }
            else
            {
                Console.WriteLine("Invalid choice. Please enter 1 or 2.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid menu option.");
        }
    }

    static void LoadCollectionFromFile()
    {
        Console.WriteLine("\nLoad Collection from File Menu:");
        Console.WriteLine("1 - Load from *.csv or *.txt");
        Console.WriteLine("2 - Load from *.json");
        Console.Write("Enter your choice: ");
        if (int.TryParse(Console.ReadLine(), out int choice))
        {
            if (choice == 1)
            {
                LoadFromCsvOrTxt();
            }
            else if (choice == 2)
            {
                LoadFromJson();
            }
            else
            {
                Console.WriteLine("Invalid choice. Please enter 1 or 2.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid menu option.");
        }
    }

    static void ClearCollection()
    {
        cars.Clear();
        Console.WriteLine("Collection cleared.");
    }

    static void SaveToCsvOrTxt()
    {
        Console.Write("Enter the file name for saving (without extension): ");
        string fileName = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(fileName))
        {
            Console.WriteLine("Invalid file name. Please enter a valid name.");
            return;
        }

        try
        {
            string extension = fileName.EndsWith(".csv") || fileName.EndsWith(".txt") ? "" : ".csv";
            string fullFileName = fileName + extension;
            using (var writer = new StreamWriter(fullFileName))
            using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csv.WriteRecords(cars);
                Console.WriteLine($"Collection saved to {fullFileName}.");
                lastFileName = fullFileName;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while saving the collection: {ex.Message}");
        }
    }


    static void LoadFromCsvOrTxt()
    {
        Console.Write("Enter the file name to load from (with extension): ");
        string fileName = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(fileName))
        {
            Console.WriteLine("Invalid file name. Please enter a valid name.");
            return;
        }

        try
        {
            string fileContents = File.ReadAllText(fileName);
            var records = new List<Car>();
            using (var reader = new StringReader(fileContents))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var make = csv.GetField("Make");
                    var model = csv.GetField("Model");
                    var year = csv.GetField<int>("Year");
                    var price = csv.GetField<double>("Price");
                    var type = Enum.Parse<CarType>(csv.GetField("Type"), true);
                    var additionalInfo = csv.GetField("AdditionalInfo");
                    records.Add(new Car(make, model, year, price, type, additionalInfo));
                }
            }

            if (records.Count > 0)
            {
                cars.Clear();
                cars.AddRange(records);
                Console.WriteLine($"Collection loaded from {fileName}.");
                lastFileName = fileName;
            }
            else
            {
                Console.WriteLine("Error loading the collection. The file contains no valid data.");
            }
        }

        catch (Exception ex)
        {
            Console.WriteLine($"Error while loading the collection: {ex.Message}");
        }
    }

    static void SaveToJson()
    {
        Console.Write("Enter the file name for saving (with .json extension): ");
        string fileName = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(fileName))
        {
            Console.WriteLine("Invalid file name. Please enter a valid name.");
            return;
        }

        try
        {
            var carInfos = cars.Select(car => new
            {
                Make = car.Make,
                Model = car.Model,
                Year = car.Year,
                Price = car.Price,
                Type = (int)car.Type,
                AdditionalInfo = car.AdditionalInfo,
                FullInfo = car.FullInfo,
                IsAvailable = car.IsAvailable
            });

            string json = JsonSerializer.Serialize(carInfos, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(fileName, json);
            Console.WriteLine($"Collection saved to {fileName}.");
            lastFileName = fileName;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while saving the collection: {ex.Message}");
        }
    }

    static void LoadFromJson()
    {
        Console.Write("Enter the file name to load from (with .json extension): ");
        string fileName = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(fileName))
        {
            Console.WriteLine("Invalid file name. Please enter a valid name.");
            return;
        }

        try
        {
            string fileContents = File.ReadAllText(fileName);
            var carInfos = JsonSerializer.Deserialize<List<Car>>(fileContents);

            if (carInfos != null)
            {
                cars.Clear();
                cars.AddRange(carInfos.Select(info => new Car
                {
                    Make = info.Make,
                    Model = info.Model,
                    Year = info.Year,
                    Price = info.Price,
                    Type = info.Type,
                    AdditionalInfo = info.AdditionalInfo
                }));

                Console.WriteLine($"Collection loaded from {fileName}.");
                lastFileName = fileName;
            }
            else
            {
                Console.WriteLine("Error loading the collection. The file contains invalid data.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while loading the collection: {ex.Message}");
        }
    }



    static string GetCarTypeString(CarType type)
    {
        switch (type)
        {
            case CarType.Sedan:
                return "sedan";
            case CarType.SUV:
                return "SUV";
            default:
                return "Unknown";
        }
    }


    static void DisplayCarInfo(Car car)
    {
        Console.WriteLine($"Make: {car.Make}");
        Console.WriteLine($"Model: {car.Model}");
        Console.WriteLine($"Year: {car.Year}");
        Console.WriteLine($"Price: ${car.Price:F2}");
        Console.WriteLine($"Type: {car.Type}");
    }

    static void AddCar()
    {
        Console.WriteLine("Add a new car:");
        Console.WriteLine("1 - Enter car characteristics interactively");
        Console.WriteLine("2 - Enter car characteristics as a string");
        Console.Write("Enter your choice: ");

        if (int.TryParse(Console.ReadLine(), out int choice))
        {
            if (choice == 1)
            {
                AddCarInteractively();
            }
            else if (choice == 2)
            {
                AddCarFromString();
            }
            else
            {
                Console.WriteLine("Invalid choice. Please enter 1 or 2.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid menu option.");
        }
    }

    //додавання автівки
    static void AddCarInteractively()
    {
        if (cars.Count < maxCars)
        {
            Console.Write("Enter car make: ");
            string make = Console.ReadLine();

            Console.Write("Enter car model: ");
            string model = Console.ReadLine();

            Console.Write("Enter car year: ");
            if (int.TryParse(Console.ReadLine(), out int year) && year >= 1885 && year <= DateTime.Now.Year)
            {
                Console.Write("Enter car price: $");
                if (double.TryParse(Console.ReadLine(), out double price) && price >= 0)
                {
                    Console.WriteLine("Car Types:");
                    foreach (var carType in Enum.GetValues(typeof(CarType)))
                    {
                        Console.WriteLine($"{(int)carType} - {carType}");
                    }
                    Console.Write("Select car type: ");
                    if (Enum.TryParse(Console.ReadLine(), out CarType type))
                    {
                        Console.Write("Enter additional information (optional): ");
                        string additionalInfo = Console.ReadLine();

                        cars.Add(new Car(make, model, year, price, type, additionalInfo));
                        Console.WriteLine("Car added successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid car type. Please select a valid car type.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid price. Please enter a valid price.");
                }
            }
            else
            {
                Console.WriteLine("Invalid year. Please enter a valid year.");
            }
        }
        else
        {
            Console.WriteLine("Maximum number of cars reached.");
        }
    }

    static void AddCarFromString()
    {
        if (cars.Count < maxCars)
        {
            Console.Write("Enter car characteristics as a comma-separated string (Make, Model, Year, Price, Type): ");
            string carInfo = Console.ReadLine();

            if (Car.TryParse(carInfo, out Car newCar))
            {
                cars.Add(newCar);
                Console.WriteLine("Car added successfully.");
            }
            else
            {
                Console.WriteLine("Invalid car characteristics format. Please enter valid characteristics.");
            }
        }
        else
        {
            Console.WriteLine("Maximum number of cars reached.");
        }
    }

    //виведення авто
    static void DisplayCars()
    {
        Console.WriteLine("\nList of Cars:");
        foreach (var car in cars)
        {
            DisplayCarInfo(car);
            Console.WriteLine();
        }

        Console.WriteLine($"Total number of cars created: {Car.ObjectCount}");
    }

    static void SearchCars()
    {
        Console.WriteLine("\nSearch Cars:");
        Console.WriteLine("1 - Search by Make");
        Console.WriteLine("2 - Search by Model");
        Console.WriteLine("3 - Search by Year");
        Console.WriteLine("4 - Search by Price");
        Console.WriteLine("5 - Search by Type");
        Console.Write("Enter your choice: ");

        if (int.TryParse(Console.ReadLine(), out int choice))
        {
            switch (choice)
            {
                case 1:
                    Console.Write("Enter car make to search: ");
                    string searchMake = Console.ReadLine();
                    var foundByMake = cars.FindAll(car => car.Make.Contains(searchMake, StringComparison.OrdinalIgnoreCase));
                    DisplaySearchResults(foundByMake);
                    break;
                case 2:
                    Console.Write("Enter car model to search: ");
                    string searchModel = Console.ReadLine();
                    var foundByModel = cars.FindAll(car => car.Model.Contains(searchModel, StringComparison.OrdinalIgnoreCase));
                    DisplaySearchResults(foundByModel);
                    break;
                case 3:
                    Console.Write("Enter car year to search: ");
                    if (int.TryParse(Console.ReadLine(), out int searchYear))
                    {
                        var foundByYear = cars.FindAll(car => car.Year == searchYear);
                        DisplaySearchResults(foundByYear);
                    }
                    else
                    {
                        Console.WriteLine("Invalid year. Please enter a valid year.");
                    }
                    break;
                case 4:
                    Console.Write("Enter car price to search: $");
                    if (double.TryParse(Console.ReadLine(), out double searchPrice))
                    {
                        var foundByPrice = cars.FindAll(car => car.Price == searchPrice);
                        DisplaySearchResults(foundByPrice);
                    }
                    else
                    {
                        Console.WriteLine("Invalid price. Please enter a valid price.");
                    }
                    break;
                case 5:
                    Console.WriteLine("Car Types:");
                    foreach (var carType in Enum.GetValues(typeof(CarType)))
                    {
                        Console.WriteLine($"{(int)carType} - {carType}");
                    }
                    Console.Write("Enter car type to search: ");
                    if (Enum.TryParse(Console.ReadLine(), out CarType searchType))
                    {
                        var foundByType = cars.FindAll(car => car.Type == searchType);
                        DisplaySearchResults(foundByType);
                    }
                    else
                    {
                        Console.WriteLine("Invalid car type. Please select a valid car type.");
                    }
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid menu option.");
        }
    }

    static void DisplaySearchResults(List<Car> results)
    {
        if (results.Count == 0)
        {
            Console.WriteLine("No matching cars found.");
        }
        else
        {
            Console.WriteLine("Matching Cars:");
            foreach (var car in results)
            {
                DisplayCarInfo(car);
                Console.WriteLine();
            }
        }
    }

    static void DeleteCar()
    {
        Console.Write("Enter the index of the car to delete: ");
        if (int.TryParse(Console.ReadLine(), out int index) && index >= 0 && index < cars.Count)
        {
            cars.RemoveAt(index);
            Console.WriteLine("Car deleted successfully.");
        }
        else
        {
            Console.WriteLine("Invalid index. Please enter a valid index.");
        }
    }

    //демонстрація поведінки
    static void DemonstrateBehavior()
    {
        Console.WriteLine("\nDemonstrating Behavior:");

        if (cars.Count > 0)
        {
            int carIndex = 0;

            Car selectedCar = cars[carIndex];

            Console.WriteLine("FullInfo:");
            Console.WriteLine(selectedCar.FullInfo);

            Console.WriteLine("\nGetFullInfo (without additional info):");
            Console.WriteLine(selectedCar.GetFullInfo("Without Additional Information"));

            Console.WriteLine("\nGetFullInfo (with additional info):");
            Console.WriteLine(selectedCar.GetFullInfo());

            Console.WriteLine("\nGetFullInfo (include availability):");
            Console.WriteLine(selectedCar.GetFullInfo(true));

            Console.WriteLine("\nParsing a sample string:");
            string sampleString = "Ford,Focus,2023,25000,Sedan";
            try
            {
                Car parsedCar = Car.Parse(sampleString);
                Console.WriteLine("Parsed Car Info:");
                Console.WriteLine(parsedCar.FullInfo);
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Error parsing the sample string: {ex.Message}");
            }

            Console.WriteLine("\nConverting Car to String:");
            string carString = selectedCar.ToString();
            Console.WriteLine("Car as String: " + carString);
        }
        else
        {
            Console.WriteLine("No cars to demonstrate behavior.");
        }
    }

}
