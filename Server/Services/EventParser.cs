using CsvHelper;
using CsvHelper.Configuration;
using Parser.Server.Models;
using Parser.Shared;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Parser.Server.Services
{
    public class EventParser : IEventParser
    {
        public (List<EventModel>, List<string>) ParseToEvents(byte[] fileContent)
        {
            List<string> notValidLines = new List<string>();

            var events = new List<EventParseModel>();

            using (var ms = new MemoryStream(fileContent))
            {
                using (var reader = new StreamReader(ms, true))
                {
                    while (reader.Peek() >= 0)
                    {
                        var line = reader.ReadLine();
                        var model = EventParseModel.TryParseToModel(line.Split(";"));
                        if(model == null)
                        {
                            notValidLines.Add(line);
                            continue;
                        }
                        events.Add(model);
                    }
                }
            }
            return (events
                .Select(p => new EventModel(p.Name, p.Description, p.StartDate, p.EndDate))
                .ToList(),
                notValidLines);
        }
    }
}
