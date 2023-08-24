using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Net;

class Program
{
    static void Main()
    {
        Console.Write("Enter URLs (comma-separated): ");
        string[] urls = Console.ReadLine().Split(',');

        foreach (string url in urls)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    string content = client.DownloadString(url);
                    Console.WriteLine($"Content of {url}:\n{content}\n");
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine($"Error downloading {url}: {ex.Message}\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while processing {url}: {ex.Message}\n");
            }
        }
    }
}





