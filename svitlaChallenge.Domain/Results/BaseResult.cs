using System.Net;

namespace svitlaChallenge.Domain.Results
{
    public class BaseResult
    {
        public string Error { get; set; } = "";
        public bool Ok { get; set; } = true;
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;

        public void SetError()
        {
            Ok = false;
            Error = "Error";
            StatusCode = HttpStatusCode.InternalServerError;
        }

        public void SetHttpStatusCode(HttpStatusCode statusCode, bool ok)
        {
            StatusCode = statusCode;
            Ok = ok;
        }

        public void SetError(string error, HttpStatusCode statusCode)
        {
            Ok = false;
            Error = error;
            StatusCode = statusCode;
        }
    }
}
