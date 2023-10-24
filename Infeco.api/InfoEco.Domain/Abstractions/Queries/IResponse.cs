namespace InfoEco.Domain.Abstractions.Queries
{
    public interface IResponse<T>
    {
        T Contenu { get; set; }
    }
}
