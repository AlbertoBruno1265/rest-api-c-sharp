namespace WebServiceFiap.ViewModels.Response
{
    /// <summary>
    /// Resposta paginada genérica usada em todos os endpoints de listagem.
    /// Inclui totalPages calculado para o cliente navegar corretamente.
    /// </summary>
    public class PagedResponse<T>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
        public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();
 
        public PagedResponse() { }
 
        public PagedResponse(IEnumerable<T> items, int page, int pageSize, int totalItems)
        {
            Items = items;
            Page = page;
            PageSize = pageSize;
            TotalItems = totalItems;
        }
    }
}