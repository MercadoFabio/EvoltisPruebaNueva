namespace Service.Dtos.Common
{
    public class PaginacionRequest
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int? IdCategory { get; set; }
        public string? Name { get; set; }
    }
}
