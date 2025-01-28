using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10268054_PRG2Assignment
{
    class Airline
    {
        public string name { get; set; }
        public string code { get; set; }
        public Dictionary<string, Flight> flights { get; set; }

        public Airline(string Name, string Code, Dictionary<string, Flight> Flights)
        {
            name = Name;
            code = Code;
            flights = Flights;
        }
        public Airline(string Name, string Code)
        {
            name = Name;
            code = Code;
            
        }



        public bool AddFlight(Flight flight)
        {
            if (flight == null)
            {
                return false; // Invalid flight
            }
            else if (flights.ContainsKey(flight.flightNumber))
            {
                return false; // Flight already exists

            }
            else
            {
                flights[flight.flightNumber] = flight;
                return true;
            }
        }

        public bool RemoveFlight(string flightNumber)
        {
            if (!flights.ContainsKey(flightNumber))
            {
                flights.Remove(flightNumber);
                return true; // Successfully removed
            }

            else
            {
                return false; // Flight does not exists
            }
        }

        public double CalculateFees()
        {
            double totalFees = 0.0;

            foreach (var flight in flights.Values)
            {
                totalFees += flight.CalculateFees(); // Sum up fees for all flights
            }

            return totalFees;
        }

        public override string ToString()
        {
            return $"Airline Code: {code}, Name: {name}, Total Flights: {flights.Count}, Total Fees: {CalculateFees():C}";
        }
    }
}
