using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Enter an HTTP status code (e.g., 200, 404, 500):");
        string statusCode = Console.ReadLine();

        // Base URL for the http.cat API
        string url = $"https://http.cat/{statusCode}";

        using HttpClient client = new HttpClient();
        try
        {
            // Get the image
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                // Save the image locally
                byte[] imageBytes = await response.Content.ReadAsByteArrayAsync();
                string filePath = $"{statusCode}.jpg";
                await File.WriteAllBytesAsync(filePath, imageBytes);

                Console.WriteLine($"Cat image for status code {statusCode} saved as {filePath}.");
            }
            else
            {
                Console.WriteLine($"Error: Could not retrieve image for status code {statusCode}.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
