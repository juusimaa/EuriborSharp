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

namespace EuriborSharp.Presenters
{
    public class MainFormPresenter
    {
        private const string FEED_ADDRESS = @"http://www.suomenpankki.fi/fi/_layouts/BOF/RSS.ashx/tilastot/Korot/fi";

        private readonly IMainForm _mainForm;

        public MainFormPresenter()
        {
            _mainForm = new MainForm();
            _mainForm.UpdateClicked += _mainForm_UpdateClicked;
            _mainForm.ClearClicked += _mainForm_ClearClicked;
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

                foreach (var item in feed.Items)
                {
                    var subject = item.Title.Text;
                    ParseInterestRates(subject);
                    _mainForm.AddText(subject + Environment.NewLine, true);
                }
            }
        }

        private void ParseInterestRates(string text)
        {
            var periodPattern = new Regex(@"(\d)(\s\w+\s)");
            var interestPattern = new Regex(@"(\d+,\d+)");
            var datePattern = new Regex(@"(\d+[.]\d+[.]\d+)");

            var interestValue = interestPattern.Match(text).Value;
            var timePeriod = periodPattern.Match(text);

            var newItem = new Euribors
            {
                Date = DateTime.Parse(datePattern.Match(text).Value)
            };
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
