using System;
using System.Globalization;

namespace Parser.Server.Models
{
    public class EventParseModel
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrEmpty(value) || value.Length > Constants.ParserParameters.EventNameMaxLength)
                    ValidationError();
                _name = value;
            }

        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                if (string.IsNullOrEmpty(value) || value.Length > Constants.ParserParameters.EventDescriptionMaxLength)
                    ValidationError();
                _description = value;
            }

        }

        private string _startDate
        {
            set
            {
                StartDate = GetIsoDateTime(value);
            }
        }
        private string _endDate
        {
            set
            {
                EndDate = GetIsoDateTime(value);
            }
        }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public static EventParseModel TryParseToModel(string[] values)
        {
            try
            {
                return new EventParseModel
                {
                    Name = values[0],
                    Description = values[1],
                    _startDate = values[2],
                    _endDate = values[3]
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        private bool IsIsoFormat(string value, out DateTime date)
        {
            return DateTime.TryParseExact(value, Constants.ParserParameters.DateFormat, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out date);
        }

        private Exception ValidationError() => throw new Exception("Validation error");

        private DateTime GetIsoDateTime(string value)
        {
            if (!IsIsoFormat(value, out DateTime date))
                ValidationError();
            return date;
        }
    }
}
