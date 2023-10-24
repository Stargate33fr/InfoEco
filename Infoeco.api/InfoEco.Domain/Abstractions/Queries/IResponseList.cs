using System.Collections;

namespace InfoEco.Domain.Abstractions.Queries
{
    public interface IResponseList<T> : IResponse<T>
        where T : IEnumerable
    {
        int Total { get; set; }
    }
}
