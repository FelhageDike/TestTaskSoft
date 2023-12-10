namespace Mvc.Extensions.Mvc;

public class PagedList<T> : List<T>
{
    public MetaData MetaData { get; set; }

    public PagedList(List<T> items, int totalCount, int pageNumber, int pageSize)
    {
        MetaData = new MetaData
        {
            TotalCount = totalCount,
            PageSize = pageSize,
            CurrentPage = pageNumber,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
        };

        AddRange(items);
    }
}