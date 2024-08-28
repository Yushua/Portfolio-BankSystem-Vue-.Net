namespace MyApp.Modelsa
{
    public class Computer
    {
        // Direct initialization with default non-null values
        private string _motherboard = string.Empty;
        private string _videoCard = string.Empty;
        private int _cpuCores;
        private bool _hasWifi;
        private DateOnly _releaseDate = DateOnly.MinValue;
        private decimal _price;

        public string Motherboard
        {
            get => _motherboard;
            set => _motherboard = value ?? throw new ArgumentNullException(nameof(Motherboard));
        }

        public string VideoCard
        {
            get => _videoCard;
            set => _videoCard = value ?? throw new ArgumentNullException(nameof(VideoCard));
        }

        public int CPUCores
        {
            get => _cpuCores;
            set
            {
                if (value > 0)
                    _cpuCores = value;
                else
                    throw new ArgumentException("CPU Cores must be greater than 0");
            }
        }

        public bool HasWifi
        {
            get => _hasWifi;
            set => _hasWifi = value;
        }

        public DateOnly ReleaseDate
        {
            get => _releaseDate;
            set => _releaseDate = value;
        }

        public decimal Price
        {
            get => _price;
            set
            {
                if (value >= 0)
                    _price = value;
                else
                    throw new ArgumentException("Price cannot be negative");
            }
        }

        // Constructor to initialize all properties
        public Computer(string motherboard, string videoCard, int cpuCores, bool hasWifi, DateOnly releaseDate, decimal price)
        {
            Motherboard = motherboard;
            VideoCard = videoCard;
            CPUCores = cpuCores;
            HasWifi = hasWifi;
            ReleaseDate = releaseDate;
            Price = price;
        }

        // Default constructor
        public Computer() { }
    }
}