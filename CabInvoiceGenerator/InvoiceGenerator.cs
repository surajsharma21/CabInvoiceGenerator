using System;
using System.Collections.Generic;
using System.Text;

namespace CabInvoiceGenerator
{
    public class InvoiceGenerator
    {
        public RideRepository rideRepository;
        public double CalculateFare(double distance, int time)
        {
            int costPerKilometer = 10;
            int costPerMinute = 1;
            int minimumFare = 5;
            double totalFare = distance * costPerKilometer + time * costPerMinute;
            if (totalFare < minimumFare)
                return minimumFare;
            return totalFare;
        }
        public InvoiceSummary CalculateFare(Ride[] rides)
        {
            double totalFare = 0;
            foreach (Ride ride in rides)
            {
                totalFare += this.CalculateFare(ride.distance, ride.time);
            }
            return new InvoiceSummary(rides.Length, totalFare);
        }
        public void AddRides(string userId, Ride[] rides)
        {
            rideRepository.AddRide(userId, rides);
        }
        public InvoiceSummary GetInvoiceSummary(string userId)
        {
            return this.CalculateFare(rideRepository.GetRides(userId));
        }
    }
}
