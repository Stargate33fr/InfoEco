using FluentValidation.Results;

namespace Infoeco.Services
{
    public interface IVerifieurReferencesService
    {
        Task<List<ValidationFailure>> VerifieAsync(params Func<Task<ValidationFailure>>[] verifieursAsync);
    }
}
