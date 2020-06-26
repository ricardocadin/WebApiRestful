using System.Net.Http.Headers;

namespace TCC.MyBooksRead.Infra.CrossCutting.Cache
{
    /// <summary>
    /// Veja dentro do projeto TCC.MyBooksRead.Infra.CrossCutting.Filters a classe CacheAttribute para mais detalhes
    /// </summary>
    public class WebApiCacheItem
    {
        public string ContentType { get; private set; }
        public byte[] Content { get; private set; }

        public WebApiCacheItem(MediaTypeHeaderValue contentType, byte[] content)
        {
            Content = content;
            ContentType = contentType.MediaType;
        }

        public bool IsValid()
        {
            return Content != null && ContentType != null;
        }
    }
}
