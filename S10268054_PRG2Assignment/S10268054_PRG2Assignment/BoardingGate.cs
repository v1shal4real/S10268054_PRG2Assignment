using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10268054_PRG2Assignment
{
    class BoardingGate
    {
        public string gateName { get; set; }
        public bool supportsCFFT { get; set; }
        public bool supportsDDJB { get; set; }
        public bool supportsLWTT { get; set; }
        public Flight flight { get; set; }

        public BoardingGate(string GateName, bool SupportsCFFT, bool SupportsDDJB, bool SupportsLWTT, Flight Flight)
        {
            gateName = GateName;
            supportsCFFT = SupportsCFFT;
            supportsDDJB = SupportsDDJB;
            supportsLWTT = SupportsLWTT;
            flight = Flight;
        }
        public BoardingGate(string GateName, bool SupportsCFFT, bool SupportsDDJB, bool SupportsLWTT)
        {
            gateName = GateName;
            supportsCFFT = SupportsCFFT;
            supportsDDJB = SupportsDDJB;
            supportsLWTT = SupportsLWTT;
            
        }



        public override string ToString()
        {
            return $"Gate: {gateName}, Supports CFFT: {supportsCFFT}, Supports DDJB: {supportsDDJB}, Supports LWTT: {supportsLWTT}, Flight: {flight.ToString()}";
        }
    }
}
