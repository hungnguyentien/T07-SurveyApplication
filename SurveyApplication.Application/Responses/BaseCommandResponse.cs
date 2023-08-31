using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyApplication.Application.Responses
{
    public class BaseCommandResponse
    {
        public int Id { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; }
        public List<string> Errors { get; set; }

        public BaseCommandResponse()
        {
        }

        public BaseCommandResponse(string message)
        {
            Errors = new List<string>();
            Message = message;
            Success = false;
        }

        public BaseCommandResponse(string message, List<string> errors)
        {
            Errors = errors;
            Message = message;
            Success = false;
        }
    }
}
