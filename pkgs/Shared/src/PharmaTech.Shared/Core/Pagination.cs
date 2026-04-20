namespace PharmaTech.Shared.Core;

public record Pagination<T>
{
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 25;
    public int TotalPages => (int)Math.Ceiling(TotalItems / (double)PageSize);
    public int TotalItems { get; init; } = 0;
    public IEnumerable<T> Items { get; init; } = Enumerable.Empty<T>();
}
