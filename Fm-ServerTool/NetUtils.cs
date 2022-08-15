namespace Fm_ServerTool
{
    public static class NetUtils
    {
        public static string DownloadString(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                return client.GetStringAsync(url).Result;
            }
        }

        public static string? TryDownloadString(string url, out string? errorMessage)
        {
            try
            {
                string @string = DownloadString(url);
                errorMessage = null;

                return @string;
            }
            catch (AggregateException exception)
            {
                if (exception.InnerException != null)
                    errorMessage = exception.InnerException.Message;
                else
                    errorMessage = "";

                return null;
            }
        }
    }
}
