namespace MyApp.Models
{
    
    public class Computer
    {
        public int ComputerId { get; set; } // Maps to ComputerId column
        public string Motherboard { get; set; } = string.Empty; // Maps to Motherboard column
        public int CpuCores { get; set; } // Maps to CPUCores column
        public bool HasWifi { get; set; } // Maps to HasWifi column
        public DateTime ReleaseDate { get; set; } // Maps to ReleaseDate column
        public decimal Price { get; set; } // Maps to Price column
        public string VideoCard { get; set; } = string.Empty; // Maps to VideoCard column

        public override string ToString()
        {
            return $"Motherboard: {Motherboard}\nVideoCard: {VideoCard}\nCPU Cores: {CpuCores}\n" +
                   $"Has Wifi: {HasWifi}\nRelease Date: {ReleaseDate:yyyy-MM-dd}\nPrice: {Price:C}\n";
        }
    }
}
