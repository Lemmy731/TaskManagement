namespace TaskManagementSystemApi.Helper.Pagination
{
    public class RequestParameter
    {
        const int MaxPageSize = 10;
        public int PageNumber { get; set; } = 1;
        public int _PageSize { get; set; } = 10;
        public int PageSize { get { return _PageSize; } set { _PageSize = (value > PageSize) ? MaxPageSize : value; } }
    }
}
