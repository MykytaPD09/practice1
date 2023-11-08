/*namespace MyCarProject
{
    public class Car
    {
        private string _make;
        private string _model;
        private int _year;
        private double _price;
        private CarType _type;
        private string _additionalInfo;


        public string Make
        {
            get { return _make; }
            private set { _make = value; }
        }

        public string Model
        {
            get { return _model; }
            private set { _model = value; }
        }

        public int Year
        {
            get { return _year; }
            private set { _year = value; }
        }

        public double Price
        {
            get { return _price; }
            private set { _price = value; }
        }

        public CarType Type
        {
            get { return _type; }
            private set { _type = value; }
        }

        public Car()
        {
            Make = "Unknown Make";
            Model = "Unknown Model";
            Year = 0;
            Price = 0.0;
            Type = CarType.Unknown;
        }

        public Car(string make, string model, int year, double price, CarType type) : this()
        {
            Make = make;
            Model = string.IsNullOrWhiteSpace(model) ? "Unknown Model" : model;
            Year = year;
            Price = price;
            Type = type;
        }

        public Car(string make, string model, int year, double price, CarType type, string additionalInfo) : this(make, model, year, price, type)
        {
            AdditionalInfo = additionalInfo;
        }

        public Car(string make, string model, int year, double price, CarType type, bool isAvailable) : this(make, model, year, price, type)
        {
            IsAvailable = isAvailable;
        }

        public string AdditionalInfo
        {
            get { return _additionalInfo; }
            set { _additionalInfo = value; }
        }

        public string FullInfo
        {
            get { return $"{Year} {Make} {Model} ({Type}): ${Price:F2}"; }
        }

        public string GetFullInfo()
        {
            return $"{FullInfo}{(string.IsNullOrWhiteSpace(AdditionalInfo) ? "" : " - " + AdditionalInfo)}";
        }

        public string GetFullInfo(string additionalInfo)
        {
            return $"{FullInfo} - {additionalInfo}";
        }

        public string GetFullInfo(bool includeAvailability)
        {
            if (includeAvailability)
            {
                return $"{FullInfo} - {(IsAvailable ? "Available" : "Not Available")}";
            }
            else
            {
                return FullInfo;
            }
        }

        private bool _isAvailable = true;

        public bool IsAvailable
        {
            get { return _isAvailable; }
            set
            {
                if (_isAvailable)
                {
                    _isAvailable = value;
                }
            }
        }
    }
}*/

namespace MyCarProject
{

    public class Car
    {
        private static int objectCount = 0;
        private static string domainCharacteristics = "Car";

        private string _make;
        private string _model;
        private int _year;
        private double _price;
        private CarType _type;
        private string _additionalInfo;

        public string Make
        {
            get { return _make; }
            set { _make = value; }
        }

        public string Model
        {
            get { return _model; }
            set { _model = value; }
        }

        public int Year
        {
            get { return _year; }
            set { _year = value; }
        }

        public double Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public CarType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public Car()
        {
            Make = "Unknown Make";
            Model = "Unknown Model";
            Year = 0;
            Price = 0.0;
            Type = CarType.Unknown;
            objectCount++;
        }

        public Car(string make, string model, int year, double price, CarType type) : this()
        {
            if (year < 1885 || year > DateTime.Now.Year)
            {
                throw new ArgumentException("Year must be between 1885 and the current year.", nameof(year));
            }

            Make = make;
            Model = string.IsNullOrWhiteSpace(model) ? "Unknown Model" : model;
            Year = year;
            Price = price;
            Type = type;
        }

        public Car(string make, string model, int year, double price, CarType type, string additionalInfo) : this(make, model, year, price, type)
        {
            AdditionalInfo = additionalInfo;
        }

        public Car(string make, string model, int year, double price, CarType type, bool isAvailable) : this(make, model, year, price, type)
        {
            IsAvailable = isAvailable;
        }

        public string AdditionalInfo
        {
            get { return _additionalInfo; }
            set { _additionalInfo = value; }
        }

        public string FullInfo
        {
            get { return $"{Year} {Make} {Model} ({Type}): ${Price:F2}"; }
        }

        public string GetFullInfo()
        {
            return $"{FullInfo}{(string.IsNullOrWhiteSpace(AdditionalInfo) ? "" : " - " + AdditionalInfo)}";
        }

        public string GetFullInfo(string additionalInfo)
        {
            return $"{FullInfo} - {additionalInfo}";
        }

        public string GetFullInfo(bool includeAvailability)
        {
            if (includeAvailability)
            {
                return $"{FullInfo} - {(IsAvailable ? "Available" : "Not Available")}";
            }
            else
            {
                return FullInfo;
            }
        }

        private bool _isAvailable = true;

        public bool IsAvailable
        {
            get { return _isAvailable; }
            set
            {
                if (_isAvailable)
                {
                    _isAvailable = value;
                }
            }
        }

        public static int ObjectCount
        {
            get { return objectCount; }
        }

        public static string DomainCharacteristics
        {
            get { return domainCharacteristics; }
            set { domainCharacteristics = value; }
        }

        public static Car Parse(string s)
        {
            var parts = s.Split(',');

            if (parts.Length < 5)
                throw new FormatException("Invalid input string format");

            string make = parts[0].Trim();
            string model = parts[1].Trim();
            int year;
            double price;
            if (!int.TryParse(parts[2].Trim(), out year) || !double.TryParse(parts[3].Trim(), out price))
                throw new FormatException("Invalid input string format");

            CarType type;
            if (!Enum.TryParse(parts[4].Trim(), out type))
                throw new FormatException("Invalid input string format");

            return new Car(make, model, year, price, type);
        }

        public static bool TryParse(string s, out Car obj)
        {
            obj = null;

            try
            {
                obj = Parse(s);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public override string ToString()
        {
            return $"{Make},{Model},{Year},{Price},{Type}";
        }
    }
}
