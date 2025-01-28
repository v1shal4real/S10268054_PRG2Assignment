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
