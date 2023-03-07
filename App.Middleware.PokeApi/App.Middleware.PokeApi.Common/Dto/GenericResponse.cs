namespace App.Middleware.PokeApi.Common.Dto
{
    public class GenericResponse
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public object Previous { get; set; }
        public List<BaseResponse> Results { get; set; }
    }

    public class BaseResponse
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
