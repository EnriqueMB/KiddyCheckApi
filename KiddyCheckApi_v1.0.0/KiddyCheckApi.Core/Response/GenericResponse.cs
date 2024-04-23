

namespace KiddyCheckApi.Core.Response
{
    public class GenericResponse
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public string CreatedId { get; set; }
        public string UpdatedId { get; set; }
        public string DeletedId { get; set; }
    }
}
