using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Parser.Server.Models;
using Parser.Server.Services;
using Parser.Shared;
using System;
using System.Collections.Generic;

namespace Parser.Server.Controllers
{
    [ApiController]
    [Route("file")]
    public class FileController : ControllerBase
    {
        private IEventParser _eventParser { get; set; }
        public FileController(IEventParser parser)
        {
            _eventParser = parser;
        }

        [HttpPost]
        [Route("parser")]
        public ActionResult<ParseEventsListModel> Parser(UploadedFile uploadedFile)
        {
            var response = _eventParser.ParseToEvents(uploadedFile.FileContent);
            return new ParseEventsListModel
            {
                Events = response.Item1,
                NotValidLines = response.Item2
            };
        }
    }
}
