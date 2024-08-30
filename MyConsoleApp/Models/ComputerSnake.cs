namespace MyApp.Models
{
    
    public class ComputerSnake
    {
        public int computer_id { get; set; }
        public string motherboard { get; set; } = string.Empty;
        public int cpu_cores { get; set; }
        public bool has_wifi { get; set; }
        public DateTime? release_date { get; set; }
        public decimal price { get; set; }
        public string video_card { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"Motherboard: {motherboard}\nVideoCard: {video_card}\nCPU Cores: {cpu_cores}\n" +
                   $"Has Wifi: {has_wifi}\nRelease Date: {release_date:yyyy-MM-dd}\nPrice: {price:C}\n";
        }
    }
}
