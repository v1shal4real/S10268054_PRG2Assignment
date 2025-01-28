using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10268054_PRG2Assignment
{
    abstract class Flight
    {
        public string flightNumber { get; set; }
        public string origin { get; set; }
        public string destination { get; set; }
        public DateTime expectedTime { get; set; }
        public string status { get; set; }

        public Flight(string FlightNumber, string Origin, string Destination, DateTime ExpectedTime, string Status)
        {
            flightNumber = FlightNumber;
            origin = Origin;
            destination = Destination;
            expectedTime = ExpectedTime;
            status = Status;
        }
        public abstract double CalculateFees(); // Absract method to be overriden by the subclasses

        public override string ToString()
        {
            return $"Flight: {flightNumber}, Origin: {origin}, Destination: {destination}, Status: {status}, Expected Time: {expectedTime}";
        }




    }
}
