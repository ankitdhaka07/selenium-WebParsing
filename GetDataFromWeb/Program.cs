using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace GetDataFromWeb
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IWebDriver driver = new ChromeDriver();

            try
            {
                // Load the webpage
                string url = "https://en.wikipedia.org/wiki/List_of_FIFA_World_Cup_finals"; // Replace with the actual URL
                driver.Navigate().GoToUrl(url);

                // Maximize browser window
                driver.Manage().Window.Maximize();

                // Wait for the table to load (use an explicit wait if needed)
                System.Threading.Thread.Sleep(2000); // Adjust if the page is slow

                // Locate the table elements
                // XPath for the first data row in the table (visible data only)


                // Extract the visible text data
                int iterations = 10;
                var payload = new List<List<string>>();
                for (int i = 1; i <= iterations; i++)
                {
                    string yearXPath = $"/html/body/div[2]/div/div[3]/main/div[3]/div[3]/div[1]/table[4]/tbody/tr[{i}]/th";
                    string winnerXPath = $"/html/body/div[2]/div/div[3]/main/div[3]/div[3]/div[1]/table[4]/tbody/tr[{i}]/td[1]";
                    string scoreXPath = $"/html/body/div[2]/div/div[3]/main/div[3]/div[3]/div[1]/table[4]/tbody/tr[{i}]/td[2]";
                    string runnerUpXPath = $"/html/body/div[2]/div/div[3]/main/div[3]/div[3]/div[1]/table[4]/tbody/tr[{i}]/td[3]";

                    string year = driver.FindElement(By.XPath(yearXPath)).Text;
                    string winner = driver.FindElement(By.XPath(winnerXPath)).Text;
                    string score = driver.FindElement(By.XPath(scoreXPath)).Text;
                    string runnerUp = driver.FindElement(By.XPath(runnerUpXPath)).Text;

                    // Create a dictionary to structure the data
                    payload.Add(
                    new List<string> { year, winner, score, runnerUp });
                }


                // Convert the dictionary to a JSON string
                string jsonPayload = JsonConvert.SerializeObject(payload, Formatting.Indented);

                // Output the JSON to console (or save it to a file)
                Console.WriteLine("Generated JSON Payload:");
                Console.WriteLine(jsonPayload);

                // Optional: Save JSON to a file
                //File.WriteAllText("payload.json", jsonPayload);

                // Clean up
                Console.WriteLine("Data extraction complete.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            finally
            {
                // Close the browser
                driver.Quit();
            }
        }
    }
}
