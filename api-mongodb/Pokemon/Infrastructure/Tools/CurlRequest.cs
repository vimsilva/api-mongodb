using System.Text.Json;

namespace api_mongodb.Infrastructure.Tools
{
    public class CurlRequest<T>
    {
        public static async Task<T> Get(string url)
        {
            T result = default(T);
            using (var httpClient = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        result = JsonSerializer.Deserialize<T>(content);
                    }
                    else
                    {
                        Console.WriteLine($"Failed to get data. Status code: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }

            return result;
        }
    }
}
