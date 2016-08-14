namespace Selenium.Kendo
{
    using Newtonsoft.Json;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.Extensions;

    public abstract class SimpleWidget : Widget
    {
        protected SimpleWidget(By @by, string name) : base(@by, name)
        {
        }

        public virtual void SetValue(IWebDriver driver, string value)
        {
            ExecuteJavaScript(driver, $"{Target}.value({value});{Target}.trigger('change');");
        }

        public virtual string GetValue(IWebDriver driver)
        {
            string value = ExecuteJavaScript<string>(driver, $"return {Target}.value();");
            return value;
        }
    }

    public abstract class Widget
    {
        protected By By { get; }
        protected string Name { get; }

        protected Widget(By by, string name)
        {
            By = by;
            Name = name;
        }

        /// <summary>
        /// Returns the data source as JSON.
        /// </summary>
        /// <param name="driver"></param>
        /// <returns></returns>
        public virtual string GetDataSourceData(IWebDriver driver)
        {
            var json = ExecuteJavaScript<string>(driver, $"return JSON.stringify({Target}.dataSource.data());");
            return json;
        }

        public virtual T GetDataSourceData<T>(IWebDriver driver)
        {
            var json = GetDataSourceData(driver);
            T o = JsonConvert.DeserializeObject<T>(json);
            return o;
        }



        /// <summary>
        /// The name of the variable that contains the widget.
        /// </summary>
        protected string Target => "w";

        protected IWebElement TargetElement(IWebDriver driver) => driver.FindElement(By);
        protected IWebElement ParentElement(IWebDriver driver) => driver.FindElement(By).GetParent();

        protected TResult ExecuteJavaScript<TResult>(IWebDriver driver, string script)
        {
            IWebElement input = TargetElement(driver);

            var js = $@"
(function(element){{
var {Target} = $(element).data('{Name}');
{script}
}})(arguments[0]);
";

            TResult result = driver.ExecuteJavaScript<TResult>(js, input);
            return result;
        }

        protected void ExecuteJavaScript(IWebDriver driver, string script)
        {
            var input = driver.FindElement(By);

            var js = $@"
(function(element){{
var {Target} = $(element).data('{Name}');
{script}
return true;
}})(arguments[0]);
";

            driver.ExecuteJavaScript<bool>(js, input);
        }
    }
}
