using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10268054_PRG2Assignment
{
    class NORMFlight : Flight
    {
        public NORMFlight(string FlightNumber, string Origin, string Destination, DateTime ExpectedTime, string Status)
        : base(FlightNumber, Origin, Destination, ExpectedTime, Status)
        {
        }

        public override double CalculateFees()
        {
            return 0.0;
        }

    }
}
