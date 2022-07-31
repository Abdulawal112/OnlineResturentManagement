namespace OnlineResturnatManagement.Client.Helper
{
    public static class ResponseErrorMessage
    {
        public static string GetErrorMessage(int statusCode)
        {
            if (statusCode == 409)
                return "Already Exist";
            if(statusCode == 400)
                return "Not Valid";
            if (statusCode == 500)
                return "Enternal Server Error";
            return "";
        }
    }
}
