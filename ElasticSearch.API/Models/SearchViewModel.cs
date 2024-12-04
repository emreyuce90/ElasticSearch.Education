namespace ElasticSearch.API.Models {
    public class SearchViewModel {
        public DateTime? StartDate{ get; set; }
        public DateTime? EndDate { get; set; }
        public string? Gender { get; set; }
        public string? CustomerFullName { get; set; }
        public string? CategoryName { get; set; }

        public int? PageSize { get; set; }
        public int? CurrentPage { get; set; }
    }
}
