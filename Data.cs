using System.Text.Json.Serialization;

namespace ChallengeInsert
{
    public struct Data
    {
        [JsonPropertyName("country")]
        public string country { get; set; }

        [JsonPropertyName("country_code")]
        public string country_code { get; set; }

        [JsonPropertyName("continent")]
        public string continent { get; set; }

        [JsonPropertyName("population")]
        public int population { get; set; }

        [JsonPropertyName("indicator")]
        public string indicator { get; set; }

        [JsonPropertyName("year_week")]
        public string year_week { get; set; }

        [JsonPropertyName("source")]
        public string source { get; set; }

        [JsonPropertyName("note")]
        public string note { get; set; }

        [JsonPropertyName("weekly_count")]
        public int weekly_count { get; set; }

        [JsonPropertyName("cumulative_count")]
        public int cumulative_count { get; set; }

        [JsonPropertyName("rate_14_day")]
        public double rate_14_day { get; set; }
    }
}
