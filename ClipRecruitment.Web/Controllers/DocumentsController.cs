using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ClipRecruitment.Web.Controllers
{
    public class DocumentsController : ApiController
    {
        #region FlowJS

        string root = @"C:\FTP\files";

        [HttpGet]
        [AllowAnonymous]
        [Route("api/documents/upload")]
        public object Upload(int flowChunkNumber, string flowIdentifier)
        {
            if (ChunkIsHere(flowChunkNumber, flowIdentifier))
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/documents/upload")]
        public async Task<object> Upload()
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            if (!Directory.Exists(root)) Directory.CreateDirectory(root);
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);
                int chunkNumber = Convert.ToInt32(provider.FormData["flowChunkNumber"]);
                int totalChunks = Convert.ToInt32(provider.FormData["flowTotalChunks"]);
                string identifier = provider.FormData["flowIdentifier"];
                string filename = provider.FormData["flowFilename"];
                // Rename generated file
                MultipartFileData chunk = provider.FileData[0]; // Only one file in multipart message
                RenameChunk(chunk, chunkNumber, identifier);
                // Assemble chunks into single file if they're all here
                TryAssembleFile(identifier, totalChunks, filename);
                // Success
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }

        }

        private string GetChunkFileName(int chunkNumber, string identifier)
        {
            return Path.Combine(root, string.Format("{0}_{1}", identifier, chunkNumber.ToString()));
        }

        private void RenameChunk(MultipartFileData chunk, int chunkNumber, string identifier)
        {
            string generatedFileName = chunk.LocalFileName;
            string chunkFileName = GetChunkFileName(chunkNumber, identifier);
            if (File.Exists(chunkFileName)) File.Delete(chunkFileName);
            File.Move(generatedFileName, chunkFileName);

        }

        private string GetFileName(string identifier)
        {
            return Path.Combine(root, identifier);
        }

        private bool ChunkIsHere(int chunkNumber, string identifier)
        {
            string fileName = GetChunkFileName(chunkNumber, identifier);
            return File.Exists(fileName);
        }

        private bool AllChunksAreHere(string identifier, int totalChunks)
        {
            for (int chunkNumber = 1; chunkNumber <= totalChunks; chunkNumber++)
                if (!ChunkIsHere(chunkNumber, identifier)) return false;
            return true;
        }

        private void TryAssembleFile(string identifier, int totalChunks, string filename)
        {
            if (AllChunksAreHere(identifier, totalChunks))
            {
                // Create a single file
                var consolidatedFileName = GetFileName(identifier);
                using (var destStream = File.Create(consolidatedFileName, 15000))
                {
                    for (int chunkNumber = 1; chunkNumber <= totalChunks; chunkNumber++)
                    {
                        var chunkFileName = GetChunkFileName(chunkNumber, identifier);
                        using (var sourceStream = File.OpenRead(chunkFileName))
                        {
                            sourceStream.CopyTo(destStream);
                        }
                    }
                    destStream.Close();
                }
                // Rename consolidated with original name of upload
                filename = Path.GetFileName(filename); // Strip to filename if directory is specified (avoid cross-directory attack)
                string realFileName = Path.Combine(root, filename);
                if (File.Exists(filename)) File.Delete(realFileName);
                File.Move(consolidatedFileName, realFileName);
                // Delete chunk files
                for (int chunkNumber = 1; chunkNumber <= totalChunks; chunkNumber++)
                {
                    var chunkFileName = GetChunkFileName(chunkNumber, identifier);
                    File.Delete(chunkFileName);
                }
            }
        }

        #endregion
    }
}
