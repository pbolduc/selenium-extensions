namespace Selenium.Kendo
{
    using System;
    using System.Drawing;
    using System.Threading;
    using OpenQA.Selenium;
    using Selenium.Extensions.Interfaces;

    public class CssClass
    {
        public const string Link = "k-link";
        public const string Icon = "k-icon";
        public const string Warning = "k-warning";
        public const string Menu = "k-menu";
        public const string Popup = "k-popup";
        public const string Group = "k-group";
        public const string Header = "k-header";
        public const string Footer = "k-footer";
        public const string Tooltip = "k-tooltip";
        public const string TooltipValidation = "k-tooltip-validation";

        public const string Weekend = "k-weekend";
        public const string OtherMonth = "k-other-month";

        public const string NavFast = "k-nav-fast";
        public const string NavPrev = "k-nav-prev";
        public const string NavNext = "k-nav-next";
        public const string NavToday = "k-nav-today";

        // k-animation-container
        // k-content
        // k-state-disabled
        // k-state-focused
        // k-state-hover
        // k-footer
        // k-widget
        // k-calendar
        // k-combobox
        // k-datepicker
        // k-datetimepicker
        // k-slider
        // k-timepicker
    }

    public class Calendar : Widget
    {
        public Calendar(ITestWebDriver driver, By by)
            : base(driver, by, "kendoCalendar")
        {            
        }

        private const string MonthView = "month";
        private const string YearView = "year";
        private const string DecadeView = "decade";
        private const string CenturyView = "century";

        //private const string nav = ".//div/a";
        //private const string cells = ".//table/tbody/tr[@role='row']/td[@role='gridcell']/a[@class='k-link']";

        private const string TodayCssSelector = "div." + CssClass.Footer + " > a." + CssClass.Link + "." + CssClass.NavToday;
        private const string NextCssSelector = "div > a." + CssClass.Link + "." + CssClass.NavNext;
        private const string PrevCssSelector = "div > a." + CssClass.Link + "." + CssClass.NavPrev;
        private const string FastCssSelector = "div > a." + CssClass.Link + "." + CssClass.NavFast;


        private const int WeeksPerPage = 6;

        public void ClickDate(DateTime date)
        {
            var view = View;
            if (view == MonthView)
            {
                var tableElement = FindElement().FindElement(By.XPath(".//table"));

                var startDate = GetDateAttribute(tableElement, "data-start");
                var endDate = startDate.AddDays(WeeksPerPage * 7 - 1);

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

        /// <summary>
        /// Gets the first date in the calendar.
        /// </summary>
        /// <exception cref="InvalidOperationException">invalid view type</exception>
        private DateTime GetStart()
        {
            var view = View;
            if (view == MonthView)
            {
                return GetMonthViewStart();
            }

            if (view == YearView)
            {
                return GetYearViewStart();
            }

            if (view == DecadeView)
            {
                return GetDecadeViewStart();
            }

            if (view == CenturyView)
            {
                return GetCenturyViewStart();
            }

            throw new InvalidOperationException($"invalid view type '{view ?? string.Empty}'.");
        }

        private DateTime GetMonthViewStart()
        {
            var element = FindElement().FindElement(By.XPath(".//table"));
            return GetDateAttribute(element, "data-start");
        }

        private DateTime GetYearViewStart()
        {
            var element = FindElement().FindElement(By.CssSelector("div > a." + CssClass.NavFast));
            var year = int.Parse(element.Text);
            return new DateTime(year, 1, 1);
        }

        private DateTime GetDecadeViewStart()
        {
            IWebElement element = FindElement().FindElement(By.CssSelector(FastCssSelector));
            var year = int.Parse(element.Text.Substring(0, 4));
            return new DateTime(year, 1, 1);
        }

        private DateTime GetCenturyViewStart()
        {
            // same structure as decade view
            return GetDecadeViewStart();
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

            var dayElement = FindElement().FindElement(By.XPath(xpath));
            return dayElement;
        }

        public void ClickNavigateUp()
        {
            var element = FindElement().FindElement(By.CssSelector(FastCssSelector));
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
            var element = FindElement().FindElement(By.CssSelector(PrevCssSelector));
            element.Click();
        }

        /// <summary>
        /// Will click the next 
        /// </summary>
        public void ClickNext()
        {
            var element = FindElement().FindElement(By.CssSelector(NextCssSelector));
            element.Click();
        }

        public void ClickToday()
        {
            var element = FindElement().FindElement(By.CssSelector(TodayCssSelector));
            element.Click();
        }

        /// <summary>
        /// Returns the current view of the calendar: month, year, decade or century.
        /// </summary>
        public string View => (string)Driver.ExecuteScript(Scripts.Calendar_view_name, FindElement());

        private DateTime GetDateAttribute(IWebElement element, string attributeName)
        {
            var targetDate = element.GetAttribute(attributeName);

            var date = DateParser.Parse(targetDate);
            return date;
        }
        
        private int ZoomAnimationTime()
        {
            var value = (long)Driver.ExecuteScript(Scripts.Calendar_options_animation_vertical_duration, FindElement());
            return Convert.ToInt32(value);
        }

        private int NextPrevAnimationTime()
        {
            var value = (long)Driver.ExecuteScript(Scripts.Calendar_options_animation_horizontal_duration, FindElement());
            return Convert.ToInt32(value);
        }
    }

    public static class DateParser
    {
        /// <summary>
        /// Parses a date with format yyyy/m/d when m is javascript month 0-11.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime Parse(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            int year = ToInt32(value[0])*1000
                       + ToInt32(value[1])*100
                       + ToInt32(value[2])*10
                       + ToInt32(value[3]);

            int month = Month(value);

            int day = (value[6] == '/')
                ? int.Parse(value.Substring(7))
                : int.Parse(value.Substring(8));

            return new DateTime(year, month, day);
        }

        private static int ToInt32(char c)
        {
            return c - '0';
        }

        private static int Month(string value)
        {
            var char5 = value[5];

            switch (char5)
            {
                case '0':
                    return 1;
                case '1':
                {
                    var char6 = value[6];
                    if (char6 == '/') return 2;
                    if (char6 == '0') return 11;
                    if (char6 == '1') return 12;
                    throw new InvalidOperationException();
                }
                case '2':
                    return 3;
                case '3':
                    return 4;
                case '4':
                    return 5;
                case '5':
                    return 6;
                case '6':
                    return 7;
                case '7':
                    return 8;
                case '8':
                    return 9;
                case '9':
                    return 10;
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}