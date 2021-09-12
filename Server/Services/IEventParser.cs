using Parser.Shared;
using System.Collections.Generic;

namespace Parser.Server.Services
{
    public interface IEventParser
    {
        (List<EventModel>, List<string>) ParseToEvents(byte[] fileContent);
    }
}
