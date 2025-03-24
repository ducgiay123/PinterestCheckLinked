using System;
using System.Diagnostics;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;

namespace CheckDieLinkedinToolV3.Services
{
    internal class ChromeDriverServices
    {
        public class ChromeService
        {
            public IWebDriver _driver;
            private ChromeOptions _options;
            private ChromeDriverService _service;
            private static readonly object _lockObj = new object();

            public ChromeService()
            {
                _options = new ChromeOptions();
                _options.AddUserProfilePreference("useAutomationExtension", true);
                _options.AddUserProfilePreference("credentials_enable_service", true);
                _options.AddUserProfilePreference("profile.password_manager_enabled", false);
            }

            public void AddOption(string option)
            {
                _options.AddArgument(option);
            }

            public string GetURLOfDriver()
            {
                return _driver.Url;
            }

            public void SetUpProxy(string proxy)
            {
                _options.AddArgument($"--proxy-server={proxy}");
            }
            public void SetSize(int width, int height)
            {
                _driver.Manage().Window.Size = new System.Drawing.Size(width, height);
            }

            public void SetPositionByIndex(int index)
            {
                int witI = index % 5;
                int hitI = index / 5;

                    
                _driver.Manage().Window.Position = new System.Drawing.Point(witI*300, hitI*400);
            }
            public bool ElementExists(By by)
            {
                try
                {
                    return _driver.FindElements(by).Count > 0;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            }

            //public void SetSize()
            //{
            //    _driver.Manage().Window.Minimize();
            //}

            public void InitializeDriver()
            {
                if (_driver != null)
                {
                    throw new InvalidOperationException("Driver is already initialized.");
                }
                _options.AddArgument($"--window-size={500},{400}");
                // Create and initialize the ChromeDriverService
                _service = ChromeDriverService.CreateDefaultService();
                _service.HideCommandPromptWindow = true;

                // Initialize the ChromeDriver with the service and options
                _driver = new ChromeDriver(_service, _options);
            }

            public bool ClickAndWaitForURLToExist(By by)
            {
                try
                {
                    // Ensure the WebDriver is initialized and not null
                    if (_driver == null)
                    {
                        throw new InvalidOperationException("WebDriver is not initialized or has been disposed.");
                    }

                    // Find the element and click it
                    IWebElement element = _driver.FindElement(by);
                    element.Click();

                    // Wait for the URL to be valid and start with 'http'
                    WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30)); // Set appropriate timeout
                    wait.Until(d =>
                        !string.IsNullOrEmpty(d.Url) && d.Url.StartsWith("http"));

                    // Wait for the page to fully load after the URL changes
                    WaitToFullyLoadedURL();

                    return true; // URL exists and the page has fully loaded
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return false; // Something went wrong during the process
                }
            }

            public void ShutDownDriver()
            {
                if (_driver != null)
                {
                    try
                    {
                        // Use lock to ensure that only one thread can shut down the driver at a time
                        KillSpecificChromeDriverProcess(_service.ProcessId);

                        if (_driver != null)  // Check again in case driver is already quit by another thread
                            {
                                // Quit and dispose the driver
                                _driver.Quit();
                                _driver.Dispose();
                            }

                        // After quitting, kill any remaining Chrome or WebDriver processes
                        _driver = null;  // Ensure the driver is nullified after quitting
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error during driver shutdown: {ex.Message}");
                    }
                }
            }
            private void KillSpecificChromeDriverProcess(int processId)
            {
                try
                {
                    using (Process processKill = Process.Start(new ProcessStartInfo
                    {
                        FileName = "taskkill",
                        Arguments = $"/f /pid {processId} /t",
                        CreateNoWindow = true,
                        WindowStyle = ProcessWindowStyle.Hidden
                    }))
                    {
                        processKill.WaitForExit(); // Wait for the taskkill command to complete
                    }
                    Console.WriteLine($"Terminated process with PID: {processId}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to terminate process with PID: {processId}. Error: {ex.Message}");
                }
            }

            public void WaitToFullyLoadedURL()
            {
                if (_driver == null)
                {
                    Console.WriteLine("Driver is not initialized or has been closed.");
                    throw new InvalidOperationException("Driver is not initialized or has been closed.");
                }

                try
                {
                    WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
                    wait.Until(driver =>
                    {
                        if (_driver == null)
                        {
                            Console.WriteLine("Driver has been closed or terminated.");
                            return false;
                        }

                        try
                        {
                            return ((IJavaScriptExecutor)_driver).ExecuteScript("return document.readyState").ToString() == "complete";
                        }
                        catch (WebDriverException ex)
                        {
                            Console.WriteLine($"WebDriver exception occurred: {ex.Message}");
                            return false;
                        }
                    });
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine($"Operation failed: {ex.Message}");
                    throw;
                }
                catch (WebDriverTimeoutException)
                {
                    Console.WriteLine("Timeout while waiting for the page to fully load.");
                    throw;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                    throw;
                }
            }

            public void WaitForUrlChange(string initialUrl, int timeoutInSeconds = 30)
            {
                if (_driver == null)
                {
                    throw new InvalidOperationException("Driver is not initialized.");
                }

                try
                {
                    WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeoutInSeconds));
                    wait.Until(driver =>
                    {
                        try
                        {
                            // Check if the current URL is different from the initial URL
                            return !string.IsNullOrEmpty(driver.Url) && driver.Url != initialUrl;
                        }
                        catch (WebDriverException)
                        {
                            return false; // Handle cases where the driver becomes unavailable
                        }
                    });

                    // Wait for the page to fully load after the URL changes
                    WaitToFullyLoadedURL();
                }
                catch (WebDriverTimeoutException)
                {
                    Console.WriteLine("Timeout waiting for the URL to change.");
                    throw;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while waiting for the URL to change: {ex.Message}");
                    throw;
                }
            }

            private void KillChromeProcesses()
            {
                try
                {
                    // Get all processes named "chromedriver"
                    var processes = Process.GetProcessesByName("chromedriver");

                    // Loop through all processes
                    foreach (var process in processes)
                    {
                        try
                        {
                            // Create a process kill command using the process ID
                            using (Process process_kill = Process.Start(new ProcessStartInfo()
                            {
                                FileName = "taskkill",
                                Arguments = $"/f /pid {process.Id} /t",  // Kill the process by its ID, including child processes
                                CreateNoWindow = true,
                                WindowStyle = ProcessWindowStyle.Hidden
                            }))
                            {
                                // This ensures the process is killed and disposed of after the execution
                                Console.WriteLine($"Terminated process: {process.ProcessName} with PID: {process.Id}");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Failed to terminate process with PID: {process.Id}. Error: {ex.Message}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error killing Chrome/WebDriver processes: {ex.Message}");
                }
            }


            public bool GoToURLFullyLoaded(string url)
            {
                if (_driver == null)
                {
                    throw new InvalidOperationException("Driver is not initialized.");
                }

                try
                {
                    _driver.Navigate().GoToUrl(url);
                    WaitToFullyLoadedURL();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
