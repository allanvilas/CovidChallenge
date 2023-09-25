using System.Text.Json;

namespace ChallengeInsert
{
    public class FetchData
    {
        public Data[] Main()
        {
            Console.WriteLine($"[TASK]: Executing fetch task");
            string jsonEndpointUrl = "https://opendata.ecdc.europa.eu/covid19/nationalcasedeath/json/";

            Data[] data;
            using (HttpClient httpClient = new HttpClient())
            {

                try
                {
                    Console.WriteLine($"[TASK]: Connecting to {jsonEndpointUrl}");

                    HttpResponseMessage response = httpClient.GetAsync(jsonEndpointUrl).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"[TASK]: status 200 sucess");

                        string jsonContent = response.Content.ReadAsStringAsync().Result;

                        data = JsonSerializer.Deserialize<Data[]>(jsonContent);

                        Console.WriteLine($"[TASK]: Fetched data {data}");
                        return data;
                    }
                    else
                    {
                        Console.WriteLine($"Failed to fetch data. Status code: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }

            return null;

        }
    }
}
