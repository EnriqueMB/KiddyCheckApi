

namespace KiddyCheckApi.Core.Response
{
    public class ListResponse<T> where T : class
    {
        public List<T> Data { get; set; }
        public int Total { get; set; }
    }
}
