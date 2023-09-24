namespace TaskManagementSystemApi.Helper.Pagination
{
    public class PageList<T>: List<T>
    {
        public MetaData metaData { get; set; }
        public PageList(List<T> items, int count, int pageNumber, int pageSize)
        {
            metaData = new MetaData
            {
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalCount = count,
                TotalPage = (int)Math.Ceiling(count / (double)pageSize)
            };
            AddRange(items);
        }
        public static PageList<T> ToPageList(IEnumerable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return new PageList<T>(items, count, pageNumber, pageSize);
        }
    }
}
