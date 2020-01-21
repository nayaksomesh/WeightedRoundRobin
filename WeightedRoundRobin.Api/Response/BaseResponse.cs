using System.Collections.Generic;

namespace Assignment.Api.Response
{
    internal class BaseResponse
    {
        public List<string> Errors { get; } = new List<string>();
        public List<string> Messages { get; } = new List<string>();
    }
}