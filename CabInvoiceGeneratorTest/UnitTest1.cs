using CabInvoiceGenerator;
using NUnit.Framework;

namespace CabInvoiceGeneratorTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }
        [Test]
        public void GivenDistanceAndTime_CalculateFareMethodShould_ReturnTotalFare()
        {
            InvoiceGenerator invoiceGenerator = new InvoiceGenerator();
            double distance = 20;
            int time = 45;
            double actualFare = invoiceGenerator.CalculateFare(distance, time);
            double expectedFare = 245;
            Assert.AreEqual(expectedFare, actualFare);
        }
        [Test]
        public void GivenDistanceAndTime_CalculateFareMethodShould_ReturnMinimumFare()
        {
            InvoiceGenerator invoiceGenerator = new InvoiceGenerator();
            double distance = 0.2;
            int time = 2;
            double actualFare = invoiceGenerator.CalculateFare(distance, time);
            double expectedFare = 5;
            Assert.AreEqual(expectedFare, actualFare);
        }
        [Test]
        public void Given5Rides_CalculateFareMethodShould_ReturnTotalFare()
        {
            Ride[] rides =
            {
                new Ride(1.0, 1),
                new Ride(2.0, 2),
                new Ride(3.0, 2),
                new Ride(4.0, 4),
                new Ride(5.0, 3)
            };
            double expected = 162;
            InvoiceGenerator invoiceGenerator = new InvoiceGenerator();
            InvoiceSummary summary = invoiceGenerator.CalculateFare(rides);
            double actual = summary.totalFare;
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void Given5Rides_InvoiceSummaryShould_ReturnEnhancedInvoiceSummary()
        {
            Ride[] rides =
            {
                new Ride(1.0, 1),
                new Ride(2.0, 2),
                new Ride(3.0, 2),
                new Ride(4.0, 4),
                new Ride(5.0, 3)
            };
            InvoiceSummary expected = new InvoiceSummary(5, 162);
            InvoiceGenerator invoiceGenerator = new InvoiceGenerator();
            InvoiceSummary summary = invoiceGenerator.CalculateFare(rides);
            Assert.AreEqual(summary, expected);
        }
        [Test]
        public void GivenUserId_InvoiceServiceShould_ReturnListOfRides()
        {
            Ride[] rides =
            {
                new Ride(1.0, 1),
                new Ride(2.0, 2),
                new Ride(3.0, 2),
                new Ride(4.0, 4),
                new Ride(5.0, 3)
            };
            string userId = "12345";
            RideRepository rideRepository = new RideRepository();
            rideRepository.AddRide(userId, rides);
            Ride[] actual = rideRepository.GetRides(userId);
            Assert.AreEqual(rides, actual);
        }
    }
}