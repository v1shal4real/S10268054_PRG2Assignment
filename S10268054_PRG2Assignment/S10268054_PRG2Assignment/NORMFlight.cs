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
    class NORMFlight : Flight
    {
        public NORMFlight(string FlightNumber, string Origin, string Destination, DateTime ExpectedTime, string Status)
        : base(FlightNumber, Origin, Destination, ExpectedTime, Status)
        {
        }

        public override double CalculateFees()
        {
            return 0;
        }

    }
}
