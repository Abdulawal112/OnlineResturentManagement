using OnlineResturnatManagement.Shared.DTO;

namespace OnlineResturnatManagement.Client.Helper
{
    public static class ResponseErrorMessage
    {
        public static StatusResult GetErrorMessage(int statusCode)
        {
            if (statusCode == 409)
                return new StatusResult { StatusCode = statusCode, Message = "Already Exist" };
            if(statusCode == 400)
                return new StatusResult { StatusCode = statusCode, Message = "Not Valid" };
            if (statusCode == 500)
                return new StatusResult { StatusCode = statusCode, Message = "Enternal Server Error" };
            return new StatusResult { StatusCode = statusCode, Message = "" }; ;
        }
    }
}
