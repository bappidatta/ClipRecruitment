using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace ClipRecruitment.Web.HelperClasses
{
    public class VideoStream
    {
        private readonly string _root = @"C:\FTP\files\";
        private readonly string _fileName;

        public VideoStream(string filename)
        {
            _fileName = _root + filename;
        }

        public async Task WriteToStream(Stream outputStream, HttpContent content, TransportContext context)
        {
            try
            {
                var buffer = new byte[65536];
                using (var video = File.Open(_fileName, FileMode.Open, FileAccess.Read))
                {
                    var length = (int)video.Length;
                    var bytesRead = 1;
                    while(length > 0 && bytesRead > 0)
                    {
                        bytesRead = video.Read(buffer, 0, Math.Min(length, buffer.Length));
                        await outputStream.WriteAsync(buffer, 0, bytesRead);
                        length -= bytesRead;
                    }
                }
            }
            catch(HttpException ex)
            {
                return;
            }
            finally
            {
                outputStream.Close();
            }
        }
    }
}