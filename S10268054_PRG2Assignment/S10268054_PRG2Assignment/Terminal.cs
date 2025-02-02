using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//==========================================================
// Student Number : S10368054
// Student Name : Thulasiahilan Vishal
//==========================================================
namespace S10268054_PRG2Assignment
{
    class Terminal
    {
        public string terminalName { get; set; }
        public Dictionary<string, Airline> airlines { get; set; }
        public Dictionary<string, Flight> flights { get; set; }
        public Dictionary<string, BoardingGate> boardingGates { get; set; }
        public Dictionary<string, double> gateFees { get; set; }

        public Terminal(string TerminalName, Dictionary<string, Airline> Airlines, Dictionary<string, Flight> Flights, Dictionary<string, BoardingGate> BoardingGates, Dictionary<string, double> GateFees)
        {
            terminalName = TerminalName;
            airlines = Airlines;
            flights = Flights;
            boardingGates = BoardingGates;
            gateFees = GateFees;

        }

        public bool AddAirline(Airline airline)
        {
            if (airlines.ContainsKey(airline.code))
            {
                airlines[airline.code] = airline;
                return true; // Airline had been added to the Airlines dictionary
            }
            return false; // Airline already exists
        }

        public bool AddBoardingGate(BoardingGate boardingGate)
        {
            if (boardingGates.ContainsKey(boardingGate.gateName))
            {
                boardingGates[boardingGate.gateName] = boardingGate;
                return true; // Boardng Gate had been successfully added to the Boarding Gates dictionary
            }
            return false; // Boarding Gate already exists
        }

        public Airline GetAirlineFromFlight(Flight flight)
        {
            foreach (var airline in airlines.Values)
            {
                if (airline.flights.ContainsKey(flight.flightNumber))
                {
                    return airline;
                }
            }
            return null; // Flight not found in any Airline. 
        }
        public void PrintAirlineFees()
        {
            foreach (var airline in airlines.Values)
            {
                Console.WriteLine($"Airline: {airline.name}, Fees: {airline.CalculateFees()}");
            }
        }

        public override string ToString()
        {
            return $"Terminal Name: {terminalName}, Airlines: {airlines.Count}, Boarding Gates: {boardingGates.Count}, Flights: {flights.Count}";
        }
    }
}
