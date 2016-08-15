namespace Selenium.Kendo
{
    using System;
    using Newtonsoft.Json;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.Extensions;


    public abstract class Widget
    {
        protected By By { get; }
        protected string Name { get; }
        protected IWebDriver WebDriver { get; }

        protected Widget(IWebDriver webDriver, By by, string name)
        {
            WebDriver = webDriver;
            By = by;
            Name = name;
        }

        /// <summary>
        /// Returns the data source as JSON.
        /// </summary>
        /// <returns></returns>
        public virtual string GetDataSourceData()
        {
            return AsJson($"{Target}.dataSource.data()");
        }

        public virtual T GetDataSourceData<T>()
        {
            var json = GetDataSourceData();
            T o = JsonConvert.DeserializeObject<T>(json);
            return o;
        }

        public virtual void SetValue(string value)
        {
            ExecuteJavaScript($"{Target}.value({value});{Target}.trigger('change');");
        }

        public virtual string GetValue()
        {
            string value = ExecuteJavaScript<string>($"return {Target}.value();");
            return value;
        }

        protected string AsJson(string expression)
        {
            var json = ExecuteJavaScript<string>($"return JSON.stringify({expression});");
            return json;
        }

        protected DateTime AsDate(string expression)
        {
            var iso8601 = ExecuteJavaScript<string>(expression);
            return DateTime.Parse(iso8601, null, System.Globalization.DateTimeStyles.RoundtripKind);
        }

        /// <summary>
        /// The name of the variable that contains the widget.
        /// </summary>
        protected string Target => "w";

        protected IWebElement Element => WebDriver.FindElement(By);

        /// <summary>
        /// The script should have a return value at the end.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="script"></param>
        /// <returns></returns>
        protected TResult ExecuteJavaScript<TResult>(string script)
        {
            var js = $@"return (function(element){{var {Target} = $(element).data('{Name}');{script}}})(arguments[0]);";
            TResult result = WebDriver.ExecuteJavaScript<TResult>(js, Element);
            return result;
        }

        protected void ExecuteJavaScript(string script)
        {
            var js = $@"return (function(element){{var {Target} = $(element).data('{Name}');{script}return true;}})(arguments[0]);";
            WebDriver.ExecuteJavaScript<bool>(js, Element);
        }
    }
}
