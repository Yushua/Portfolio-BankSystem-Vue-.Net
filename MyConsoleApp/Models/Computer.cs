using System.Text.Json.Serialization;

namespace MyApp.Models
{
    
    public class Computer
    {
        [JsonPropertyName("computer_id")]
        public int ComputerId { get; set; }
        [JsonPropertyName("motherboard")]
        public string Motherboard { get; set; } = string.Empty;

        [JsonPropertyName("cpu_cores")]
        public int CpuCores { get; set; }

        [JsonPropertyName("has_wifi")]
        public bool HasWifi { get; set; }

        [JsonPropertyName("release_date")]
        public DateTime? ReleaseDate { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("video_card")]
        public string VideoCard { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"Motherboard: {Motherboard}\nVideoCard: {VideoCard}\nCPU Cores: {CpuCores}\n" +
                   $"Has Wifi: {HasWifi}\nRelease Date: {ReleaseDate:yyyy-MM-dd}\nPrice: {Price:C}\n";
        }
    }
}
