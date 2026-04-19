namespace WP.BusinessLogic.Models
{
    public class PagedList<T> : List<T>
    {
        public PagedList(IEnumerable<T> items, int count, int pageNumber, int   )
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            AddRange(items);
        }

        public int CurrentPage { get; private set; }

        public int TotalCount { get; private set; }

        public int PageSize { get; private set; }
        
        public int TotalPages { get; private set; }

        public bool HasPrevious => CurrentPage > 1;

        public bool HasNext => CurrentPage < TotalPages;

        public static PagedList<T> ToPagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize) => 
            new PagedList<T>(items, count, pageNumber, pageSize);
    }
}
