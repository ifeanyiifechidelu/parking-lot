using Parking_Lot.Implementations.Transaction.Service;
using Parking_Lot.Objects.ParkingLot.Entity;
using Parking_Lot.Objects.Transaction.Entity;
namespace Parking_Lot.UnitTest;

public class Tests
{
    private TransactionService _transaction = null!;

        [SetUp]
        public void Setup()
        {
            _transaction = new TransactionService(100, 80, 40);
        }

        [Test]
        public void ParkVehicle_WhenParkingAvailable_ShouldReturnValidTicket()
        {
            // Arrange
            var motorcycle = new Vehicle { Type = VehicleType.Motorcycle, Size = 1 };

            // Act
            var ticket = _transaction.ParkVehicle(motorcycle);

            // Assert
            Assert.That(ticket, Is.Not.Null);
            Assert.That(ticket.Vehicle.Type, Is.EqualTo(VehicleType.Motorcycle));
            Assert.That(ticket.Vehicle.Size, Is.EqualTo(1));
            Assert.That(ticket.SpotNumber, Is.GreaterThan(0));
            Assert.That(ticket.EntryDateTime, Is.EqualTo(DateTime.Now));
        }

        [Test]
        public void ParkVehicle_WhenParkingUnavailable_ShouldReturnNull()
        {
            // Arrange
            var bus = new Vehicle { Type = VehicleType.Bus, Size = 3 };

            // Act
            var ticket = _transaction.ParkVehicle(bus);

            // Assert
            Assert.That(ticket, Is.Null);
        }

        [Test]
        public void UnparkVehicle_WhenValidTicket_ShouldReturnValidReceipt()
        {
            // Arrange
            var car = new Vehicle { Type = VehicleType.Car, Size = 1 };
            var ticket = _transaction.ParkVehicle(car);

            // Act
            var receipt = _transaction.UnparkVehicle(ticket);

            // Assert
            Assert.That(receipt, Is.Not.Null);
            Assert.That(receipt.EntryDateTime, Is.EqualTo(ticket.EntryDateTime));
            Assert.That(receipt.ExitDateTime, Is.GreaterThan(ticket.EntryDateTime));
            Assert.That(receipt.Fee, Is.GreaterThanOrEqualTo(0));
        }

        [Test]
        public void UnparkVehicle_WhenInvalidTicket_ShouldReturnNull()
        {
            // Arrange
            var invalidTicket = new ParkingTicket { SpotNumber = 99, EntryDateTime = DateTime.Now };

            // Act
            var receipt = _transaction.UnparkVehicle(invalidTicket);

            // Assert
            Assert.That(receipt, Is.Null);
        }
}