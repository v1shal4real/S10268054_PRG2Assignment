// See https://aka.ms/new-console-template for more information
// Feature 1

// Dictionaries to store Airline and BoardingGate objects


using S10268054_PRG2Assignment;

Dictionary<string, Airline> airlineDictionary = new Dictionary<string, Airline>();
Dictionary<string, BoardingGate> boardingGateDictionary = new Dictionary<string, BoardingGate>();

LoadAirlines("airlines.csv", airlineDictionary);
LoadBoardingGates("boardinggates.csv", boardingGateDictionary);


Console.WriteLine("Airlines:");
foreach (var airline in airlineDictionary.Values)
{
    Console.WriteLine(airline.ToString());
}

Console.WriteLine("\nBoarding Gates:");
foreach (var gate in boardingGateDictionary.Values)
{
    Console.WriteLine(gate.ToString());
}


static void LoadAirlines(string filePath, Dictionary<string, Airline> airlineDictionary)
{
    try
    {
        using (StreamReader sr = new StreamReader("airlines.csv"))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {

                string[] parts = line.Split(',');
                if (parts.Length == 2)
                {
                    string name = parts[0].Trim();
                    string code = parts[1].Trim();


                    Airline airline = new Airline(name, code);


                    airlineDictionary[code] = airline;
                }
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error loading airlines: {ex.Message}");
    }
}

static void LoadBoardingGates(string filePath, Dictionary<string, BoardingGate> boardingGateDictionary)
{
    try
    {
        using (StreamReader sr = new StreamReader("boardingggates.csv"))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {

                string[] parts = line.Split(',');
                if (parts.Length == 4)
                {
                    string gateName = parts[0].Trim();
                    bool supportsCFFT = bool.Parse(parts[1].Trim());
                    bool supportsDDJB = bool.Parse(parts[2].Trim());
                    bool supportsLWTT = bool.Parse(parts[3].Trim());


                    BoardingGate gate = new(gateName, supportsCFFT, supportsDDJB, supportsLWTT);


                    boardingGateDictionary[gateName] = gate;
                }
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error loading boarding gates: {ex.Message}");
    }
}