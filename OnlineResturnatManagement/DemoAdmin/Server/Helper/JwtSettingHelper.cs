namespace OnlineResturnatManagement.Server.Helper
{
    public static class JwtSettingHelper
    {
        public static string SecurityKey = "CodeMazeSecretKey";
        public static string ValidIssuer = "CodeMazeAPI";
        public static string ValidAudience = "https://localhost:5011";
        public static int EexpiryInMinutes = 4;

    }
}
