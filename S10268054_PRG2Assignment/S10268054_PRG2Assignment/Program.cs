
using S10268054_PRG2Assignment;
//==========================================================
// Student Number : S10368054
// Student Name : Thulasiahilan Vishal
//==========================================================

// Feature 1

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
            bool isFirstLine = true; 
            while ((line = sr.ReadLine()) != null)
            {
                if (isFirstLine)
                {
                    isFirstLine = false;
                    continue; 
                }

                if (string.IsNullOrWhiteSpace(line)) continue;

                
                string[] parts = line.Split(',');
                if (parts.Length >= 5)
                {
                    string flightNumber = parts[0].Trim();
                    string origin = parts[1].Trim();
                    string destination = parts[2].Trim();
                    DateTime expectedTime = DateTime.Parse(parts[3].Trim());
                    string status = parts[4].Trim();
                    string specialRequestCode = parts.Length == 6 ? parts[5].Trim() : null;

                    
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

    return count; 
}




while (true) 
{
    
    Console.WriteLine("\n=============================================");
    Console.WriteLine("Welcome to Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine("1. List All Flights");
    Console.WriteLine("2. List Boarding Gates");
    Console.WriteLine("3. Create Flight");
    Console.WriteLine("4. Display Airline Flights");
    Console.WriteLine("5. Bulk Assign Flights to Boarding Gates");
    Console.WriteLine("\n0. Exit");
    Console.Write("\nPlease select your option: ");

    
    string input = Console.ReadLine().Trim();

    
    if (input == "1")
    {
        ListAllFlights(flightDictionary, airlineDictionary);
    }
    else if (input == "2")
    {
        ListAllBoardingGates(boardingGateDictionary);
    }
    else if (input == "3")
    {
        AddNewFlight(flightDictionary, filepath3);
    }
    else if (input == "4")
    {
        DisplayFlightDetails(airlineDictionary, flightDictionary, boardingGateDictionary);
    }
    else if (input == "5") 
    {
        BulkAssignFlights(flightDictionary, boardingGateDictionary, airlineDictionary);
    }
    else if (input == "0")
    {
        Console.WriteLine("\n Goodbye!");
        break; 
    }
    else
    {
        Console.WriteLine("\nInvalid option! Please enter a number from 0 to 5.");
    }
}





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
        
        string airlineCode = flight.flightNumber.Substring(0, 2);
        string airlineName = "Unknown Airline";

        if (airlineDictionary.ContainsKey(airlineCode))
        {
            airlineName = airlineDictionary[airlineCode].name;
        }

        
        Console.WriteLine("{0,-15} {1,-20} {2,-20} {3,-20} {4,-30}",
            flight.flightNumber, airlineName, flight.origin, flight.destination, flight.expectedTime);
    }

}


// Feature 4

void ListAllBoardingGates(Dictionary<string, BoardingGate> boardingGateDictionary)
{
    Console.WriteLine("\n=============================================");
    Console.WriteLine("List of Boarding Gates for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine("Gate Name  DDJB    CFFT    LWTT");

    foreach (var gate in boardingGateDictionary.Values)
    {
        
        Console.WriteLine("{0,-10} {1,-7} {2,-7} {3,-7}",
            gate.gateName,
            gate.supportsDDJB ? "True" : "False",
            gate.supportsCFFT ? "True" : "False",
            gate.supportsLWTT ? "True" : "False");
    }
}

// Feature 6

void AddNewFlight(Dictionary<string, Flight> flightDictionary, string filePath)
{
    bool addAnother = true; 

    while (addAnother)
    {
        
        Console.Write("\nEnter Flight Number: ");
        string flightNumber = Console.ReadLine().Trim().ToUpper();

        Console.Write("Enter Origin: ");
        string origin;
        while (string.IsNullOrWhiteSpace(origin = Console.ReadLine().Trim().ToUpper()))
        {
            Console.Write("Origin cannot be empty! Enter Origin:  ");
        }

        Console.Write("Enter Destination: ");
        string destination;
        while (string.IsNullOrWhiteSpace(destination = Console.ReadLine().Trim().ToUpper())) 
        {
            Console.Write("Destination cannot be empty! Enter Destination: ");
        };

        Console.Write("Enter Expected Departure/Arrival Time (dd/MM/yyyy HH:mm): ");
        DateTime expectedTime;
        while (!DateTime.TryParseExact(Console.ReadLine().Trim(), "d/M/yyyy HH:mm", null, System.Globalization.DateTimeStyles.None, out expectedTime))
        {
            Console.Write("Invalid format! Please enter date and time in dd/MM/yyyy HH:mm format: ");
        }


        
        Console.Write("Would you like to enter a Special Request Code? (DDJB, CFFT, LWTT) [Leave blank for none]: ");
        string specialRequestCode = Console.ReadLine().Trim().ToUpper();

        
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

        
        flightDictionary[flightNumber] = newFlight;

        
        AppendFlightToCSV(filepath3, newFlight);

        
        Console.WriteLine($"\nFlight {flightNumber} has been added!");

        
        Console.WriteLine("Would you like to add another flight? (Y/N)");
        string response = Console.ReadLine().Trim().ToUpper();
        addAnother = (response == "Y");
        
    } 
}

   



void AppendFlightToCSV(string filePath, Flight flight)
{
    try
    {
        using (StreamWriter sw = new StreamWriter(filePath, true)) 
        {
            string line = $"{flight.flightNumber},{flight.origin},{flight.destination},{flight.expectedTime:yyyy-MM-dd HH:mm},{flight.status}";

            
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


    string airlineCode;
    while (true)
    {
        Console.Write("\nEnter Airline Code: ");
        airlineCode = Console.ReadLine().Trim().ToUpper();

        if (airlineDictionary.ContainsKey(airlineCode))
        {
            break; 
        }
        Console.WriteLine("Invalid airline code! Please enter again: ");
    }



    
    Airline selectedAirline = airlineDictionary[airlineCode];

         List<Flight> airlineFlights = new List<Flight>();

        foreach (var flight in flightDictionary.Values)
        {
            if (flight.flightNumber.StartsWith(airlineCode))
            {
                airlineFlights.Add(flight);
            }
        }

        
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

// Advanced Feature


void BulkAssignFlights(Dictionary<string, Flight> flightDictionary, Dictionary<string, BoardingGate> boardingGateDictionary, Dictionary<string, Airline> airlineDictionary)
{
    Queue<Flight> flightQueue = new Queue<Flight>();
    List<BoardingGate> unassignedGates = new List<BoardingGate>();

    int initiallyAssignedFlights = 0;
    int initiallyAssignedGates = 0;

    foreach (var flight in flightDictionary.Values)
    {
        bool hasGate = false;

        foreach (var gate in boardingGateDictionary.Values)
        {
            if (gate.flight != null && gate.flight.flightNumber == flight.flightNumber)
            {
                hasGate = true;
                initiallyAssignedFlights++;
                break;
            }
        }

        if (!hasGate)
        {
            flightQueue.Enqueue(flight);
        }
    }

    Console.WriteLine($"\nTotal Flights without a Boarding Gate: {flightQueue.Count}");

    foreach (var gate in boardingGateDictionary.Values)
    {
        if (gate.flight == null)
        {
            unassignedGates.Add(gate);
        }
        else
        {
            initiallyAssignedGates++;
        }
    }

    Console.WriteLine($"Total Unassigned Boarding Gates: {unassignedGates.Count}\n");

    int flightsAssigned = 0;
    int gatesAssigned = 0;

    while (flightQueue.Count > 0 && unassignedGates.Count > 0)
    {
        Flight flight = flightQueue.Dequeue();
        BoardingGate assignedGate = null;

        foreach (var gate in unassignedGates)
        {
            if (flight is DDJBFlight && gate.supportsDDJB)
            {
                assignedGate = gate;
                break;
            }
            else if (flight is CFFTFlight && gate.supportsCFFT)
            {
                assignedGate = gate;
                break;
            }
            else if (flight is LWTTFlight && gate.supportsLWTT)
            {
                assignedGate = gate;
                break;
            }
        }

        if (assignedGate == null)
        {
            foreach (var gate in unassignedGates)
            {
                bool supportsNoRequests = !gate.supportsDDJB && !gate.supportsCFFT && !gate.supportsLWTT;

                if (supportsNoRequests)
                {
                    assignedGate = gate;
                    break;
                }
            }
        }

        if (assignedGate != null)
        {
            assignedGate.flight = flight;
            unassignedGates.Remove(assignedGate);
            flightsAssigned++;
            gatesAssigned++;

            string airlineCode = flight.flightNumber.Substring(0, 2);
            string airlineName = airlineDictionary.ContainsKey(airlineCode) ? airlineDictionary[airlineCode].name : "Unknown Airline";
            string specialRequest;

            if (flight is DDJBFlight)
            {
                specialRequest = "DDJB";
            }
            else if (flight is CFFTFlight)
            {
                specialRequest = "CFFT";
            }
            else if (flight is LWTTFlight)
            {
                specialRequest = "LWTT";
            }
            else
            {
                specialRequest = "None";
            }

            Console.WriteLine("\nFlight Assigned:");
            Console.WriteLine($"Flight Number  : {flight.flightNumber}");
            Console.WriteLine($"Airline Name   : {airlineName}");
            Console.WriteLine($"Origin         : {flight.origin}");
            Console.WriteLine($"Destination    : {flight.destination}");
            Console.WriteLine($"Expected Time  : {flight.expectedTime:dd/MM/yyyy HH:mm}");
            Console.WriteLine($"Special Request: {specialRequest}");
            Console.WriteLine($"Boarding Gate  : {assignedGate.gateName}");
        }
    }

    int totalProcessedFlights = flightsAssigned + initiallyAssignedFlights;
    int totalProcessedGates = gatesAssigned + initiallyAssignedGates;
    double percentageProcessedFlights;
    if (totalProcessedFlights > 0)
    {
        percentageProcessedFlights = (flightsAssigned * 100.0) / totalProcessedFlights;
    }
    else
    {
        percentageProcessedFlights = 0;
    }

    double percentageProcessedGates;
    if (totalProcessedGates > 0)
    {
        percentageProcessedGates = (gatesAssigned * 100.0) / totalProcessedGates;
    }
    else
    {
        percentageProcessedGates = 0;
    }
    Console.WriteLine("\n=============================================");
    Console.WriteLine("Bulk Flight Processing Summary");
    Console.WriteLine("=============================================");
    Console.WriteLine($"Total Flights Assigned: {flightsAssigned}");
    Console.WriteLine($"Total Gates Assigned: {gatesAssigned}");
    Console.WriteLine($"Percentage of Flights Processed Automatically: {percentageProcessedFlights:F2}%");
    Console.WriteLine($"Percentage of Gates Assigned Automatically: {percentageProcessedGates:F2}%");
}


Console.WriteLine("\nPress any key to exit...");
Console.ReadKey();




