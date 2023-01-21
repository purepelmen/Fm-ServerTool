namespace Fm_ServerTool
{
    public class BuildDownloader
    {
        public static bool Download(string pathToFile, string downloadUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage responseMessage = client.GetAsync(downloadUrl, HttpCompletionOption.ResponseHeadersRead).Result;
                if (!responseMessage.IsSuccessStatusCode)
                {
                    string response = $"{(int)responseMessage.StatusCode} {responseMessage.StatusCode}";
                    Console.WriteLine($"GET request failed. Response status: {response}.");
                    return false;
                }

                long lengthInBytes = responseMessage.Content.Headers.ContentLength ?? 0;
                float lengthInMBytes = lengthInBytes / (1024f * 1024f);
                Console.Write($"Donwloading: {lengthInMBytes:F2} MB");

                Stream readStream = responseMessage.Content.ReadAsStream();
                FileStream writeStream = new FileStream(pathToFile, FileMode.Create, FileAccess.Write);

                DownloadFile(lengthInBytes, readStream, writeStream);

                writeStream.Dispose();
                readStream.Dispose();

                responseMessage.Dispose();
                return true;
            }
        }

        private static void DownloadFile(long lengthInBytes, Stream readStream, FileStream writeStream)
        {
            int printPositionX = Console.CursorLeft;
            int printPositionY = Console.CursorTop;

            byte[] buffer = new byte[8192];
            long totalBytesRead = 0;
            int lastPrintedProgress = 0;

            int bytesRead = 0;
            while ((bytesRead = readStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                writeStream.Write(buffer, 0, bytesRead);
                totalBytesRead += bytesRead;

                double progress = (double)totalBytesRead / lengthInBytes;
                int progressPercent = (int)(progress * 100);

                if (progressPercent != lastPrintedProgress)
                {
                    lastPrintedProgress = progressPercent;

                    Console.CursorLeft = printPositionX;
                    Console.CursorTop = printPositionY;
                    Console.Write($" [{progressPercent}%]");
                }
            }

            Console.WriteLine();
        }
    }
}
