using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10268054_PRG2Assignment
{
    class LWTTFlight : Flight
    {
        public double requestFee { get; set; }

        public LWTTFlight(string FlightNumber, string Origin, string Destination, DateTime ExpectedTime, string Status, double RequestFee)
            : base(FlightNumber, Origin, Destination, ExpectedTime, Status)
        {
            requestFee = RequestFee;
        }

        public override double CalculateFees()
        {
            return requestFee; // Low-weight transport flights charge a request fee
        }

        public override string ToString()
        {
            return base.ToString() + $", Request Fee: {requestFee}";
        }
    }
}
