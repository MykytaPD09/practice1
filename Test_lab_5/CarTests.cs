using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyCarProject;

namespace MyCarProject
{

    [TestClass]
    public class CarTests
    {
        [TestMethod]
        public void TestCarConstructor1()
        {
            string make = "Ford";
            string model = "Focus";
            int year = 2023;
            double price = 25000;
            CarType type = CarType.Sedan;

            Car car = new Car(make, model, year, price, type);

            Assert.IsNotNull(car);
            Assert.AreEqual(make, car.Make);
            Assert.AreEqual(model, car.Model);
            Assert.AreEqual(year, car.Year);
            Assert.AreEqual(price, car.Price);
            Assert.AreEqual(type, car.Type);
        }

        [TestMethod]
        public void TestCarFullInfo()
        {
            Car car = new Car("Ford", "Focus", 2023, 25000, CarType.Sedan);
            string expected = "2023 Ford Focus (Sedan): $25000,00";

            string actual = car.FullInfo;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestGetFullInfoWithAdditionalInfo()
        {
            Car car = new Car("Ford", "Focus", 2023, 25000, CarType.Sedan, "Additional Info");
            string expected = "2023 Ford Focus (Sedan): $25000,00 - Additional Info";

            string actual = car.GetFullInfo();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestGetFullInfoWithoutAdditionalInfo()
        {
            Car car = new Car("Ford", "Focus", 2023, 25000, CarType.Sedan);
            string expected = "2023 Ford Focus (Sedan): $25000,00 - Additional Info";

            string actual = car.GetFullInfo("Additional Info");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestGetFullInfoIncludeAvailability()
        {
            Car carAvailable = new Car("Ford", "Focus", 2023, 25000, CarType.Sedan, true);
            Car carNotAvailable = new Car("Toyota", "Camry", 2022, 28000, CarType.Sedan, false);
            string expectedAvailable = "2023 Ford Focus (Sedan): $25000,00 - Available";
            string expectedNotAvailable = "2022 Toyota Camry (Sedan): $28000,00 - Not Available";

            string actualAvailable = carAvailable.GetFullInfo(true);
            string actualNotAvailable = carNotAvailable.GetFullInfo(true);

            Assert.AreEqual(expectedAvailable, actualAvailable);
            Assert.AreEqual(expectedNotAvailable, actualNotAvailable);
        }

        [TestMethod]
        public void TestCarParse()
        {
            string carString = "Nissan,Altima,2021,22000,Sedan";

            Car parsedCar = Car.Parse(carString);

            Assert.IsNotNull(parsedCar);
            Assert.AreEqual("Nissan", parsedCar.Make);
            Assert.AreEqual("Altima", parsedCar.Model);
            Assert.AreEqual(2021, parsedCar.Year);
            Assert.AreEqual(22000, parsedCar.Price);
            Assert.AreEqual(CarType.Sedan, parsedCar.Type);
        }

        [TestMethod]
        public void TestCarToString()
        {
            Car car = new Car("Honda", "Civic", 2022, 23000, CarType.Hatchback);
            string expected = "Honda,Civic,2022,23000,Hatchback";

            string carString = car.ToString();

            Assert.AreEqual(expected, carString);
        }

        [TestMethod]
        public void TestCarIsAvailable1()
        {
            Car availableCar = new Car("Toyota", "Camry", 2023, 28000, CarType.Sedan, true);
            Car notAvailableCar = new Car("Honda", "Civic", 2022, 23000, CarType.Hatchback, false);

            Assert.IsTrue(availableCar.IsAvailable);
            Assert.IsFalse(notAvailableCar.IsAvailable);
        }

        [TestMethod]
        public void TestCarConstructorWithInvalidYear()
        {
            Assert.ThrowsException<ArgumentException>(() => new Car("Ford", "Focus", -2023, 25000, CarType.Sedan));
        }

        [TestMethod]
        public void TestObjectCount()
        {

            int initialCount = Car.ObjectCount;

            Car car1 = new Car("Ford", "Focus", 2023, 25000, CarType.Sedan);
            Car car2 = new Car("Toyota", "Camry", 2022, 28000, CarType.Sedan);

            int finalCount = Car.ObjectCount;

            Assert.AreEqual(initialCount + 2, finalCount);
        }

        [TestMethod]
        public void TestDomainCharacteristics()
        {

            string initialDescription = Car.DomainCharacteristics;

            Car.DomainCharacteristics = "Automobile";

            string finalDescription = Car.DomainCharacteristics;

            Assert.AreEqual("Automobile", finalDescription);

            Car.DomainCharacteristics = initialDescription;
        }

        [TestMethod]
        public void TestCarConstructor()
        {
            string make = "Ford";
            string model = "Focus";
            int year = 2023;
            double price = 25000;
            CarType type = CarType.Sedan;

            Car car = new Car(make, model, year, price, type);

            Assert.IsNotNull(car);
            Assert.AreEqual(make, car.Make);
            Assert.AreEqual(model, car.Model);
            Assert.AreEqual(year, car.Year);
            Assert.AreEqual(price, car.Price);
            Assert.AreEqual(type, car.Type);
        }

        [TestMethod]
        public void TestCarIsAvailable()
        {
            Car availableCar = new Car("Toyota", "Camry", 2023, 28000, CarType.Sedan, true);
            Car notAvailableCar = new Car("Honda", "Civic", 2022, 23000, CarType.Hatchback, false);

            Assert.IsTrue(availableCar.IsAvailable);
            Assert.IsFalse(notAvailableCar.IsAvailable);
        }
    }
}