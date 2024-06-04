namespace AzureFunctionWakeUpApp
{
    class Program
    {
        private static Timer _timer;
        private static HttpClient _httpClient = new HttpClient();

        static void Main(string[] args)
        {
            // Define the interval (e.g., 10 seconds)
            int interval = 5000; // 5 seconds in milliseconds

            // Set up the timer to call the PingWebsite method at the specified interval
            _timer = new Timer(PingWebsite, null, 0, interval);

            // Prevent the application from exiting
            Console.WriteLine("Press [Enter] to exit the program...");
            Console.ReadLine();
        }

        private static async void PingWebsite(object state)
        {
            string[] urls = new[] {
                "https://qa-vis-weu-52256-pa-func.azurewebsites.net/",
                "https://qa-vis-weu-52256-wfm-func.azurewebsites.net/"
                };

            try
            {
                foreach (string url in urls)
                {
                    HttpResponseMessage response = await _httpClient.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"Ping successful at {DateTime.Now}: Status Code {response.StatusCode}");
                    }
                    else
                    {
                        Console.WriteLine($"Ping failed at {DateTime.Now}: Status Code {response.StatusCode}\n" +
                            $"{url} was failed");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ping failed at {DateTime.Now}: {ex.Message}");
            }
        }
    }
}
