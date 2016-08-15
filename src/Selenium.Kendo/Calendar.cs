namespace Selenium.Kendo
{
    using System;
    using System.Drawing;
    using System.Threading;
    using OpenQA.Selenium;

    public class Calendar : Widget
    {
        public Calendar(IWebDriver webDriver, By by)
            : base(webDriver, by, "kendoCalendar")
        {            
        }

        private const string MonthView = "month";
        private const string YearView = "year";
        private const string DecadeView = "decade";
        private const string CenturyView = "century";

        private const string otherMonth = "k-other-month";
        private const string weekend = "k-weekend";

        private const string otherMonthWeekend = otherMonth + " " + weekend;

        private const string prev = ".//div/a[@class='k-link k-nav-prev']";
        private const string next = ".//div/a[@class='k-link k-nav-next']";
        private const string fast = ".//div/a[@class='k-link k-nav-fast']";
        private const string nav = ".//div/a";

        private const string table = ".//table";
        private const string cells = ".//table/tbody/tr[@role='row']/td[@role='gridcell']/a[@class='k-link']";

        private const string today = ".//div[@class='k-footer']/a[@class='k-link k-nav-today']";

        private const int weeksPerPage = 6;

        public void ClickDate(DateTime date)
        {
            var view = View;
            if (view == MonthView)
            {
                var tableElement = Element.FindElement(By.XPath(table));

                var startDate = GetDateAttribute(tableElement, "data-start");
                var endDate = startDate.AddDays(weeksPerPage * 7 - 1);

                if (startDate <= date && date <= endDate)
                {
                    var target = FindDay(date);
                    target.Click(); // select date
                }
                else
                {
                    ClickNavigateUp();  // move view to year
                    ClickDate(date);
                }
            }
            else if (view == YearView)
            {
                var start = GetStart();

                // are we on the correct year??
                if (SameYear(start, date))
                {
                    var element = FindDay(new DateTime(date.Year, date.Month, 1));
                    element.Click(); // move view to month
                    WaitForNavigation();

                    ClickDate(date); 
                }
                else
                {
                    ClickNavigateUp();  // move view to decade
                    WaitForNavigation();

                    ClickDate(date);
                }

            }
            else if (view == DecadeView)
            {
                var start = GetStart();
                if (SameDecade(start, date))
                {
                    var element = FindDay(new DateTime(date.Year, 1, 1));
                    element.Click(); // move to to year
                    WaitForNavigation();

                    ClickDate(date); 
                }
                else
                {
                    ClickNavigateUp();  // move view to century
                    WaitForNavigation();
                    ClickDate(date);
                }
            }
            else if (view == CenturyView)
            {
                var start = GetStart();
                if (SameCentury(start, date))
                {
                    var element = FindDay(new DateTime(date.Year, 1, 1));
                    element.Click(); // move to to decade
                    WaitForNavigation();

                    ClickDate(date);
                }
                else
                {
                    if (date < start)
                    {
                        ClickPrevious();
                        WaitForNavigation();
                    }
                    else
                    {
                        ClickNext();
                        WaitForNavigation();
                    }

                    ClickDate(date);
                }
            }
        }

        private DateTime GetStart()
        {
            var view = View;
            if (view == MonthView)
            {
                var element = Element.FindElement(By.XPath(table));
                return GetDateAttribute(element, "data-start");
            }
            else if (view == YearView)
            {
                var element = Element.FindElement(By.XPath(fast));
                var year = int.Parse(element.Text);
                return new DateTime(year, 1, 1);
            }
            else if (view == DecadeView || view == CenturyView)
            {
                IWebElement element  = Element.FindElements(By.XPath(nav))[1];

                var year = int.Parse(element.Text.Substring(0, 4));
                return new DateTime(year, 1, 1);
            }

            throw new InvalidOperationException($"invalid view type '{view ?? string.Empty}'.");
        }

        private bool SameYear(DateTime a, DateTime b)
        {
            return a.Year == b.Year;
        }
        private bool SameDecade(DateTime a, DateTime b)
        {
            return a.Year / 10 == b.Year / 10;
        }
        private bool SameCentury(DateTime a, DateTime b)
        {
            return a.Year / 100 == b.Year / 100;
        }

        private IWebElement FindDay(DateTime date)
        {
            var year = date.Year;
            var month = date.Month - 1;
            var day = date.Day;

            string xpath = $".//table/tbody/tr[@role='row']/td[@role='gridcell']/a[@class='k-link' and @data-value='{year}/{month}/{day}']";

            var dayElement = Element.FindElement(By.XPath(xpath));
            return dayElement;
        }

        public void ClickNavigateUp()
        {
            var element = Element.FindElement(By.XPath(fast));
            element.Click();
            WaitForNavigation();
        }

        private void WaitForNavigation()
        {
            var delay = ZoomAnimationTime();
            if (delay != 0)
            {
                delay *= 2;
            }
            else
            {
                delay = 200;
            }

            Thread.Sleep(delay);
        }

        public void ClickPrevious()
        {
            var element = Element.FindElement(By.XPath(prev));
            element.Click();
        }

        /// <summary>
        /// Will click the next 
        /// </summary>
        public void ClickNext()
        {
            var element = Element.FindElement(By.XPath(next));
            element.Click();
        }

        public void ClickToday()
        {
            var element = Element.FindElement(By.XPath(today));
            element.Click();
        }

        /// <summary>
        /// Returns the current view of the calendar: month, year, decade or century.
        /// </summary>
        public string View => ExecuteJavaScript<string>($"return {Target}.view().name;");

        private DateTime GetDateAttribute(IWebElement element, string attributeName)
        {
            var targetDate = element.GetAttribute(attributeName);

            int year = int.Parse(targetDate.Substring(0, 4));

            int month = Month(targetDate);

            // single or double digit month?
            int day = (targetDate[6] == '/')
                ? int.Parse(targetDate.Substring(7))
                : int.Parse(targetDate.Substring(8));

            return new DateTime(year, month, day);
        }


        private int Month(string dataValue)
        {
            var char5 = dataValue[5];

            switch (char5)
            {
                case '0': return 1;
                case '1':
                {
                    var char6 = dataValue[6];
                    if (char6 == '/') return 2;
                    if (char6 == '0') return 11;
                    if (char6 == '1') return 12;
                    throw new InvalidOperationException();
                }
                case '2': return 3;
                case '3': return 4;
                case '4': return 5;
                case '5': return 6;
                case '6': return 7;
                case '7': return 8;
                case '8': return 9;
                case '9': return 10;
                default:
                    throw new InvalidOperationException();

            }
        }

        private int ZoomAnimationTime()
        {
            string js = $"var animation = {Target}.options.animation;if (animation) {{ return animation.vertical.duration; }} else {{ return 0; }}";
            var value = ExecuteJavaScript<long>(js);
            return Convert.ToInt32(value);
        }

        private int NextPrevAnimationTime()
        {
            string js = $"var animation = {Target}.options.animation;if (animation) {{ return animation.horizontal.duration; }} else {{ return 0; }}";
            var value = ExecuteJavaScript<long>(js);
            return Convert.ToInt32(value);
        }
    }
}