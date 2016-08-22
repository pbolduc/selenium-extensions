namespace Selenium.Kendo
{
    using System;
    using System.Collections.ObjectModel;
    using OpenQA.Selenium;
    using Selenium.Extensions.Interfaces;


    public class DatePicker : Widget
    {
        public IDatePickerApi Api { get; }

        public DatePicker(ITestWebDriver driver, By by)
            : base(driver, by, "kendoDatePicker")
        {
            Api = new DatePickerApi(this);
        }

        public Calendar GetCalendar()
        {
            var div = (ReadOnlyCollection<IWebElement>) Driver.ExecuteScript(Scripts.DatePicker_dateView_div, FindElement());          
            var calendarContainer = div[0];
            var calendarElement = calendarContainer.FindElement(By.XPath(".//div[@class='k-widget k-calendar']"));

            Calendar calendar = new Calendar(Driver, By.Id(calendarElement.GetAttribute("id")));
            return calendar;
        }

        public void ClickCalendarIcon()
        {
            var element = FindElement().NextSibling("span[@class='k-select']");
            element.Click();
        }

        //public virtual void SetValue(DateTime value, string format)
        //{
        //    string stringValue = FormatDate(value, format);
        //    SetValue(stringValue);
        //}

        public bool PopupCalendarVisible => (bool)Driver.ExecuteScript(Scripts.DatePicker_dateView_popup_visible, FindElement());
        public string Format => (string)Driver.ExecuteScript(Scripts.DatePicker_options_format, FindElement());
        public string Depth => (string)Driver.ExecuteScript(Scripts.DatePicker_options_depth, FindElement());
        public string Start => (string)Driver.ExecuteScript(Scripts.DatePicker_options_start, FindElement());
        public string Footer => (string)Driver.ExecuteScript(Scripts.DatePicker_options_footer, FindElement());
        public string Prefix => (string)Driver.ExecuteScript(Scripts.DatePicker_options_prefix, FindElement());

        public DateTime Min
        {
            get
            {
                var o = (long)Driver.ExecuteScript(Scripts.DatePicker_min_get, FindElement());                
                return KendoDate.Epoch.AddMilliseconds(o);
            }
        }

        public DateTime Max
        {
            get
            {
                var o = (long)Driver.ExecuteScript(Scripts.DatePicker_max_get, FindElement());
                return KendoDate.Epoch.AddMilliseconds(o);
            }
        }

        public DateTime Value
        {
            get
            {
                var o = (long)Driver.ExecuteScript(Scripts.DatePicker_value_get, FindElement());
                return KendoDate.Epoch.AddMilliseconds(o);
            }
        }

        private void JavaScriptOpen()
        {
            Driver.ExecuteScript(Scripts.DatePicker_open, FindElement());
        }

        private void JavaScriptClose()
        {
            Driver.ExecuteScript(Scripts.DatePicker_close, FindElement());
        }

        private string FormatDate(DateTime value, string format)
        {
            var formatted = value.ToString(format);
            return formatted;
        }

        private string FormatDate(DateTime value, string format, IFormatProvider provider)
        {
            var formatted = value.ToString(format, provider);
            return formatted;
        }

        public interface IDatePickerApi : IKendoWidgetApi
        {
            /// <summary>
            /// Uses JavaScript API to open the calendar.
            /// </summary>
            void Open();
            /// <summary>
            /// Uses JavaScript API to close the calendar.
            /// </summary>
            void Close();
        }

        private class DatePickerApi : KendoWidgetApi<DatePicker>, IDatePickerApi
        {

            public DatePickerApi(DatePicker datePicker)
                : base(datePicker)
            {
            }

            public void Open()
            {
                Widget.JavaScriptOpen();
            }

            public void Close()
            {
                Widget.JavaScriptClose();
            }
        }
    }
}