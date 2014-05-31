using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel.Syndication;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using EuriborSharp.Interfaces;
using EuriborSharp.Model;
using EuriborSharp.Views;
using System.Globalization;

namespace EuriborSharp.Presenters
{
    public class MainFormPresenter : IDisposable
    {
        private const string FEED_ADDRESS = @"http://www.suomenpankki.fi/fi/_layouts/BOF/RSS.ashx/tilastot/Korot/fi";
        
        // Flag: Has Dispose already been called? 
        bool disposed = false;
        
        private readonly IMainForm _mainForm;

        public MainFormPresenter()
        {
            _mainForm = new MainForm();
            _mainForm.UpdateClicked += _mainForm_UpdateClicked;
            _mainForm.ClearClicked += _mainForm_ClearClicked;
        }

        // Public implementation of Dispose pattern callable by consumers. 
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern. 
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here. 
                //
                _mainForm.Dispose();
            }

            // Free any unmanaged objects here. 
            //
            disposed = true;
        }

        void _mainForm_ClearClicked(object sender, EventArgs e)
        {
            _mainForm.ClearAll();
        }

        void _mainForm_UpdateClicked(object sender, EventArgs e)
        {
            ReadRssFeed();
        }

        public Form GetMainForm()
        {
            return (Form) _mainForm;
        }

        private void ReadRssFeed()
        {
            using (var reader = XmlReader.Create(FEED_ADDRESS))
            {
                var feed = SyndicationFeed.Load(reader);

                if (feed == null) return;

                var current = new Euribors();

                foreach (var item in feed.Items)
                {                    
                    var subject = item.Title.Text;
                    ParseInterestRates(subject, current);
                    _mainForm.AddText(subject + Environment.NewLine, true);
                }
            }
        }

        private void ParseInterestRates(string text, Euribors current)
        {
            var periodPattern = new Regex(@"(\d)(\s\w+\s)");
            var interestPattern = new Regex(@"(\d+,\d+)");
            var datePattern = new Regex(@"(\d+[.]\d+[.]\d+)");            

            var interestValue = interestPattern.Match(text).Value;
            var timePeriod = periodPattern.Match(text);
            var date = datePattern.Match(text).Value;

            var period = ParseTimePeriod(timePeriod);

            switch (period)
            {
                case Enums.TimePeriods.OneWeek:
                    current.OneWeek = Convert.ToDecimal(interestValue);
                    break;
                case Enums.TimePeriods.TwoWeeks:
                    current.TwoWeeks = Convert.ToDecimal(interestValue);
                    break;
                case Enums.TimePeriods.OneMonth:
                    current.OneMonth = Convert.ToDecimal(interestValue);
                    break;
                case Enums.TimePeriods.ThreeMonths:
                    current.ThreeMonths = Convert.ToDecimal(interestValue);
                    break;
                case Enums.TimePeriods.SixMonths:
                    current.SixMonths = Convert.ToDecimal(interestValue);
                    break;
                case Enums.TimePeriods.TwelveMonths:
                    current.TwelveMonths = Convert.ToDecimal(interestValue);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("period");
            };

            current.Date = DateTime.Parse(date, new CultureInfo("fi-FI"), System.Globalization.DateTimeStyles.AssumeLocal);
        }

        private Enums.TimePeriods ParseTimePeriod(Match value)
        {
            var intMatch = Convert.ToInt32(value.Groups[1].Value.ToString());
            var stringMatch = value.Groups[2].Value.ToString().Trim();

            switch (stringMatch)
            {
                case "kk":
                    switch (intMatch)
                    {
                        case 1:
                            return Enums.TimePeriods.OneMonth;
                        case 3:
                            return Enums.TimePeriods.ThreeMonths;
                        case 6:
                            return Enums.TimePeriods.SixMonths;
                        case 12:
                            return Enums.TimePeriods.TwelveMonths;
                        default:
                            return Enums.TimePeriods.Default;
                    };
                case "vko":
                    break;
                default:
                    return Enums.TimePeriods.Default;
            };

            return Enums.TimePeriods.Default;
        }

        private void Save()
        {
            using (var fs = new FileStream("data.xml", FileMode.Append, FileAccess.Write))
            {
                var xs = new XmlSerializer(typeof (List<Euribors>));
                xs.Serialize(fs, TheEuribors.InterestList);
            }
        }

        private void Load()
        {
            using (var fs = new FileStream("data.xml", FileMode.Open, FileAccess.Read))
            {
                var xs = new XmlSerializer(typeof (List<Euribors>));
                TheEuribors.InterestList = (List<Euribors>)xs.Deserialize(fs);
            }
        }
    }
}
