using System;

namespace HabrPostApi.Settings
{
    public class HabrParserSettings : IPageParserSettings
    {
        private int _startPageNumber;
        private int _endPageNumber;

        public int StartPageNumber
        {
            get => _startPageNumber;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(StartPageNumber),
                        "Must be a positive number");
                }

                _startPageNumber = value;
                CurrentPageNumber = value;
            }
        }

        public int CurrentPageNumber { get; set; }

        public int EndPageNumber
        {
            get => _endPageNumber;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(EndPageNumber),
                        "Must be a positive number");
                }
                if (value < _startPageNumber)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(EndPageNumber),
                        $"Must be grater then {nameof(StartPageNumber)}");
                }

                _endPageNumber = value;
            }
        }

        public string BaseUrl { get; }
        public string Url => $"{BaseUrl}/page{CurrentPageNumber}/";
        public bool CanParse => CurrentPageNumber <= EndPageNumber;

        public HabrParserSettings()
        {
            BaseUrl = "https://habr.com/ru/all";
        }
    }
}