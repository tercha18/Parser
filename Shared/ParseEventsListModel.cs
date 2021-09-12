using System.Collections.Generic;

namespace Parser.Shared
{
    public class ParseEventsListModel
    {
        public List<EventModel> Events { get; set; }
        public List<string> NotValidLines { get; set; }
    }
}
