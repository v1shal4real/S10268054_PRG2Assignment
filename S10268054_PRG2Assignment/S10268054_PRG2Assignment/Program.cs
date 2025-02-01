﻿// See https://aka.ms/new-console-template for more information



using S10268054_PRG2Assignment;
// Feature 1
// Dictionaries to store Airline and BoardingGate objects

Dictionary<string, Airline> airlineDictionary = new Dictionary<string, Airline>();
Dictionary<string, BoardingGate> boardingGateDictionary = new Dictionary<string, BoardingGate>();

string filepath1 = @"C:\S0268054_PRG2Assignment\S10268054_PRG2Assignment\S10268054_PRG2Assignment\airlines.csv";
Console.WriteLine("Loading Airlines...");
int airlineCount = LoadAirlines(filepath1, airlineDictionary);
Console.WriteLine($"{airlineCount} Airlines Loaded!");

string filepath2 = @"C:\S0268054_PRG2Assignment\S10268054_PRG2Assignment\S10268054_PRG2Assignment\boardinggates.csv";
Console.WriteLine("\nLoading Boarding Gates...");
int boardingGateCount = LoadBoardingGates(filepath2, boardingGateDictionary);
Console.WriteLine($"{boardingGateCount} Boarding Gates Loaded!");


int LoadAirlines(string filePath, Dictionary<string, Airline> airlineDictionary)
{
    int count = 0;
    try
    {
        using (StreamReader sr = new StreamReader(filePath))
        {
            string line;
            bool isFirstLine = true;
            while ((line = sr.ReadLine()) != null)
            {
                if (isFirstLine)
                {
                    isFirstLine = false;
                    continue; // Skip header line
                }

                if (string.IsNullOrWhiteSpace(line)) continue;

                string[] parts = line.Split(',');
                if (parts.Length == 2)
                {
                    string name = parts[0].Trim();
                    string code = parts[1].Trim();

                    airlineDictionary[code] = new Airline(name, code);
                    count++;
                }
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error loading airlines: {ex.Message}");
    }
    return count;
}

int LoadBoardingGates(string filePath, Dictionary<string, BoardingGate> boardingGateDictionary)
{
    int count = 0;
    try
    {
        using (StreamReader sr = new StreamReader(filePath))
        {
            string line;
            bool isFirstLine = true;
            while ((line = sr.ReadLine()) != null)
            {
                if (isFirstLine)
                {
                    isFirstLine = false;
                    continue; // Skip header line
                }
                if (string.IsNullOrWhiteSpace(line)) continue;

                string[] parts = line.Split(',');
                if (parts.Length == 4)
                {
                    if (!bool.TryParse(parts[1].Trim(), out bool supportsCFFT) ||
                        !bool.TryParse(parts[2].Trim(), out bool supportsDDJB) ||
                        !bool.TryParse(parts[3].Trim(), out bool supportsLWTT))
                    {
                        Console.WriteLine($"Invalid boolean values in line: {line}");
                        continue;
                    }

                    string gateName = parts[0].Trim();
                    boardingGateDictionary[gateName] = new BoardingGate(gateName, supportsCFFT, supportsDDJB, supportsLWTT);
                    count++;
                }
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error loading boarding gates: {ex.Message}");
    }
    return count;
}

// Feature 2

// Dictionary to store Flight objects
Dictionary<string, Flight> flightDictionary = new Dictionary<string, Flight>();


string filepath3 = @"C:\S0268054_PRG2Assignment\S10268054_PRG2Assignment\S10268054_PRG2Assignment\flights.csv";
Console.WriteLine("Loading Flights...");
int flightCount = LoadFlights(filepath3, flightDictionary);
Console.WriteLine($"{flightCount} Flights Loaded!");

int LoadFlights(string filePath, Dictionary<string, Flight> flightDictionary)
{
    int count = 0;

    try
    {
        using (StreamReader sr = new StreamReader(filePath))
        {
            string line;
            bool isFirstLine = true; // Flag to skip the header
            while ((line = sr.ReadLine()) != null)
            {
                if (isFirstLine)
                {
                    isFirstLine = false;
                    continue; // Skip the header line
                }

                if (string.IsNullOrWhiteSpace(line)) continue;

                // Assuming CSV format: flightNumber,origin,destination,expectedTime,status,specialRequestCode
                string[] parts = line.Split(',');
                if (parts.Length >= 5)
                {
                    string flightNumber = parts[0].Trim();
                    string origin = parts[1].Trim();
                    string destination = parts[2].Trim();
                    DateTime expectedTime = DateTime.Parse(parts[3].Trim());
                    string status = parts[4].Trim();
                    string specialRequestCode = parts.Length == 6 ? parts[5].Trim() : null;

                    // Create the appropriate Flight object based on the special request code
                    Flight flight;
                    switch (specialRequestCode)
                    {
                        case "DDJB":
                            flight = new DDJBFlight(flightNumber, origin, destination, expectedTime, status, 300.0);
                            break;
                        case "CFFT":
                            flight = new CFFTFlight(flightNumber, origin, destination, expectedTime, status, 150.0);
                            break;
                        case "LWTT":
                            flight = new LWTTFlight(flightNumber, origin, destination, expectedTime, status, 500.0);
                            break;
                        default:
                            flight = new NORMFlight(flightNumber, origin, destination, expectedTime, status);
                            break;
                    }

                    // Add the Flight object to the dictionary
                    flightDictionary[flightNumber] = flight;
                    count++;
                }
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error loading flights: {ex.Message}");
    }

    return count; // Return the number of flights loaded
}

ListAllFlights(flightDictionary, airlineDictionary);


// Feature 3



void ListAllFlights(Dictionary<string, Flight> flightDictionary, Dictionary<string, Airline> airlineDictionary)
{
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Flights Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine("{0,-15} {1,-20} {2,-20} {3,-20} {4,-30}",
        "Flight Number", "Airline Name", "Origin", "Destination", "Expected Departure/Arrival Time");

    Console.WriteLine("-------------------------------------------------------------------------------------------------------");

    foreach (var flight in flightDictionary.Values)
    {
        // Get airline name from the first two letters of the flight number
        string airlineCode = flight.flightNumber.Substring(0, 2);
        string airlineName = "Unknown Airline"; // Default in case the airline is not found

        if (airlineDictionary.ContainsKey(airlineCode))
        {
            airlineName = airlineDictionary[airlineCode].name;
        }

        // Display the flight details in a properly formatted table
        Console.WriteLine("{0,-15} {1,-20} {2,-20} {3,-20} {4,-30}",
            flight.flightNumber, airlineName, flight.origin, flight.destination, flight.expectedTime);
    }

}
ListAllBoardingGates(boardingGateDictionary);

// Feature 4


void ListAllBoardingGates(Dictionary<string, BoardingGate> boardingGateDictionary)
{
    Console.WriteLine("\n=============================================");
    Console.WriteLine("List of Boarding Gates for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine("Gate Name  DDJB    CFFT    LWTT");

    foreach (var gate in boardingGateDictionary.Values)
    {
        // Print gate name and whether it supports each request
        Console.WriteLine("{0,-10} {1,-7} {2,-7} {3,-7}",
            gate.gateName,
            gate.supportsDDJB ? "True" : "False",
            gate.supportsCFFT ? "True" : "False",
            gate.supportsLWTT ? "True" : "False");
    }
}

// Feature 6

AddNewFlight(flightDictionary, filepath3);

void AddNewFlight(Dictionary<string, Flight> flightDictionary, string filePath)
{
    bool addAnother = true; // Loop control variable

    while (addAnother)
    {
        // Prompt user for basic flight information
        Console.WriteLine("\nEnter Flight Number: ");
        string flightNumber = Console.ReadLine().Trim();

        Console.WriteLine("Enter Origin: ");
        string origin = Console.ReadLine().Trim();

        Console.WriteLine("Enter Destination: ");
        string destination = Console.ReadLine().Trim();

        Console.WriteLine("Enter Expected Departure/Arrival Time (dd/MM/yyyy HH:mm): ");
        DateTime expectedTime;
        while (!DateTime.TryParseExact(Console.ReadLine().Trim(), "d/M/yyyy HH:mm", null, System.Globalization.DateTimeStyles.None, out expectedTime))
        {
            Console.WriteLine("Invalid format! Please enter date and time in dd/MM/yyyy HH:mm format: ");
        }


        // Ask user if they want to enter a Special Request Code
        Console.WriteLine("Would you like to enter a Special Request Code? (DDJB, CFFT, LWTT) [Leave blank for none]: ");
        string specialRequestCode = Console.ReadLine().Trim().ToUpper();

        // Create the appropriate Flight object
        Flight newFlight;
        switch (specialRequestCode)
        {
            case "DDJB":
                newFlight = new DDJBFlight(flightNumber, origin, destination, expectedTime, "Scheduled", 300.0);
                break;
            case "CFFT":
                newFlight = new CFFTFlight(flightNumber, origin, destination, expectedTime, "Scheduled", 150.0);
                break;
            case "LWTT":
                newFlight = new LWTTFlight(flightNumber, origin, destination, expectedTime, "Scheduled", 500.0);
                break;
            default:
                newFlight = new NORMFlight(flightNumber, origin, destination, expectedTime, "Scheduled");
                break;
        }

        // Add the new flight to the dictionary
        flightDictionary[flightNumber] = newFlight;

        // Append the new flight to the CSV file
        AppendFlightToCSV(filepath3, newFlight);

        // Print confirmation message matching your required output
        Console.WriteLine($"\nFlight {flightNumber} has been added!");

        // Ask if they want to add another flight
        Console.WriteLine("Would you like to add another flight? (Y/N)");
        string response = Console.ReadLine().Trim().ToUpper();
        addAnother = (response == "Y");
    } 
}

    Console.WriteLine("All flights have been successfully added.");


// Function to append a new flight entry to the flights.csv file
void AppendFlightToCSV(string filePath, Flight flight)
{
    try
    {
        using (StreamWriter sw = new StreamWriter(filePath, true)) // Open file in append mode
        {
            string line = $"{flight.flightNumber},{flight.origin},{flight.destination},{flight.expectedTime:yyyy-MM-dd HH:mm},{flight.status}";

            // Include special request code if applicable
            if (flight is DDJBFlight)
                line += ",DDJB";
            else if (flight is CFFTFlight)
                line += ",CFFT";
            else if (flight is LWTTFlight)
                line += ",LWTT";

            sw.WriteLine(line);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error saving flight to file: {ex.Message}");
    }
}


// Feature 7
DisplayFlightDetails(airlineDictionary, flightDictionary, boardingGateDictionary);
void DisplayFlightDetails(Dictionary<string, Airline> airlineDictionary, Dictionary<string, Flight> flightDictionary, Dictionary<string, BoardingGate> boardingGateDictionary)
{

        Console.WriteLine("\n=============================================");
        Console.WriteLine("List of Airlines for Changi Airport Terminal 5");
        Console.WriteLine("=============================================");
        Console.WriteLine("{0,-15} {1,-30}", "Airline Code", "Airline Name");

        foreach (var airline in airlineDictionary.Values)
        {
            Console.WriteLine("{0,-15} {1,-30}", airline.code, airline.name);
        }


        Console.WriteLine("\nEnter Airline Code: ");
        string airlineCode = Console.ReadLine().Trim().ToUpper();


        while (!airlineDictionary.ContainsKey(airlineCode))
        {
            Console.WriteLine("Invalid airline code! Please enter again: ");
            airlineCode = Console.ReadLine().Trim().ToUpper();
        }
        Airline selectedAirline = airlineDictionary[airlineCode]; // Now it's guaranteed to be valid






    List<Flight> airlineFlights = new List<Flight>();

        foreach (var flight in flightDictionary.Values)
        {
            if (flight.flightNumber.StartsWith(airlineCode))
            {
                airlineFlights.Add(flight);
            }
        }

        // If no flights are found, inform the user and exit
        if (airlineFlights.Count == 0)
        {
            Console.WriteLine("No flights found for " + selectedAirline.name);
            return;
        }


        Console.WriteLine("\n=============================================");
        Console.WriteLine($"List of Flights for {selectedAirline.name}");
        Console.WriteLine("=============================================");
        Console.WriteLine("{0,-15} {1,-25} {2,-20} {3,-20} {4,-25}",
            "Flight Number", "Airline Name", "Origin", "Destination", "Expected Time");

        foreach (var flight in airlineFlights)
        {
            Console.WriteLine("{0,-15} {1,-25} {2,-20} {3,-20} {4,-25}",
                flight.flightNumber, selectedAirline.name, flight.origin, flight.destination, flight.expectedTime.ToString("dd/MM/yyyy hh:mm tt"));
        }
}







Console.WriteLine("\nPress any key to exit...");
Console.ReadKey();




