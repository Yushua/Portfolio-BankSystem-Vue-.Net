namespace MyApp.Modelsa
{
    public class Computer
    {
        // Direct initialization with default non-null values
        public string Motherboard = string.Empty;
        public string VideoCard = string.Empty;
        public int CpuCores;
        public bool HasWifi;
        public DateOnly ReleaseDate = DateOnly.MinValue;
        public decimal Price;
    }
}