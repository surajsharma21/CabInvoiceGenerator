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
            double distance = 20;
            int time = 45;
            InvoiceGenerator invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);
            double actualFare = invoiceGenerator.CalculateFare(distance, time);
            double expectedFare = 245;
            Assert.AreEqual(expectedFare, actualFare);
        }
        [Test]
        public void GivenDistanceAndTime_CalculateFareMethodShould_ReturnMinimumFare()
        {
            double distance = 0.2;
            int time = 2;
            InvoiceGenerator invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);
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
            InvoiceGenerator invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);
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
            InvoiceGenerator invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);
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
            InvoiceGenerator invoiceGenerator = new InvoiceGenerator(RideType.PREMIUM);
            RideRepository rideRepository = new RideRepository();
            rideRepository.AddRide(userId, rides);
            Ride[] actual = rideRepository.GetRides(userId);
            Assert.AreEqual(rides, actual);
        }
        [Test]
        public void GivenInvalidRideType_Should_Return_CabInvoiceException()
        {
            try
            {
                double distance = -5; //in km
                int time = 20;   //in minute
                InvoiceGenerator invoiceGenerator = new InvoiceGenerator();
                double fare = invoiceGenerator.CalculateFare(distance, time);
            }
            catch (CabInvoiceException ex)
            {
                Assert.AreEqual(ex.type, CabInvoiceException.ExceptionType.INVALID_RIDE_TYPE);
            }
        }
        [Test]
        public void GivenInvalidDistance_Should_Return_CabInvoiceException()
        {
            try
            {
                double distance = -5; //in km
                int time = 20;   //in minute
                InvoiceGenerator invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);
                double fare = invoiceGenerator.CalculateFare(distance, time);
            }
            catch (CabInvoiceException ex)
            {
                Assert.AreEqual(ex.type, CabInvoiceException.ExceptionType.INVALID_DISTANCE);
            }
        }
        [Test]
        public void GivenInvalidTime_Should_Return_CabInvoiceException()
        {
            try
            {
                double distance = 5; //in km
                int time = -20;   //in minutes
                InvoiceGenerator invoiceGenerator = new InvoiceGenerator(RideType.NORMAL);
                double fare = invoiceGenerator.CalculateFare(distance, time);
            }
            catch (CabInvoiceException ex)
            {
                Assert.AreEqual(ex.type, CabInvoiceException.ExceptionType.INVALID_TIME);
            }
        }
        [Test]
        public void GivenInvalidUserId_InvoiceServiceShould_ReturnCabInvoiceException()
        {
            try
            {
                RideRepository rideRepository = new RideRepository();
                Ride[] actual = rideRepository.GetRides("InvalidUserID");
            }
            catch (CabInvoiceException ex)
            {
                Assert.AreEqual(ex.type, CabInvoiceException.ExceptionType.INVALID_USER_ID);
            }
        }
        [Test]
        public void GivenNullRides_InvoiceServiceShould_ReturnCabInvoiceException()
        {
            try
            {
                Ride[] rides =
                {
                    new Ride(5, 20),
                    null,
                    new Ride(2, 10)
                };
                RideRepository rideRepository = new RideRepository();
                rideRepository.AddRide("111", rides);
            }
            catch (CabInvoiceException ex)
            {
                Assert.AreEqual(ex.type, CabInvoiceException.ExceptionType.NULL_RIDES);
            }
        }
    }
}