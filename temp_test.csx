using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
var driver = new ChromeDriver();
try {
    driver.Navigate().GoToUrl("https://www.calculator.net/carbohydrate-calculator.html");
    System.Threading.Thread.Sleep(2000);
    var inputs = driver.FindElements(By.TagName("input"));
    foreach (var i in inputs) {
        var id = i.GetAttribute("id");
        var name = i.GetAttribute("name");
        var type = i.GetAttribute("type");
        if (!string.IsNullOrEmpty(id) || !string.IsNullOrEmpty(name))
            Console.WriteLine($"id={id} name={name} type={type} displayed={i.Displayed}");
    }
} finally { driver.Quit(); }
