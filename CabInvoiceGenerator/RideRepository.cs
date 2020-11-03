using System;
using System.Collections.Generic;
using System.Text;

namespace CabInvoiceGenerator
{
    public class RideRepository
    {
        Dictionary<string, List<Ride>> userRides = null;
        public RideRepository()
        {
            this.userRides = new Dictionary<string, List<Ride>>();
        }
        public void AddRide(string userId, Ride[] rides)
        {
            bool rideList = this.userRides.ContainsKey(userId);
            if (!rideList)
            {
                List<Ride> list = new List<Ride>();
                list.AddRange(rides);
                this.userRides.Add(userId, list);
            }
        }
        public Ride[] GetRides(string userId)
        {
            return this.userRides[userId].ToArray();
        }
    }
}
