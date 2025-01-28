// See https://aka.ms/new-console-template for more information



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

// Load flights and print confirmation messages
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



