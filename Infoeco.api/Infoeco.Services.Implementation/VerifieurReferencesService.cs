using FluentValidation.Results;
using Infoeco.Services;

namespace Infoeco.Services.Implementation
{
    public class VerifieurReferencesService: IVerifieurReferencesService
    {
       

        public VerifieurReferencesService()
        {
            
        }

        public async Task<List<ValidationFailure>> VerifieAsync(params Func<Task<ValidationFailure>>[] verifieursAsync)
        {
            var failures = new List<ValidationFailure>();

            foreach (var verifieurAsync in verifieursAsync)
            {
                var failure = await verifieurAsync();
                if (failure != null)
                    failures.Add(failure);
            }

            return failures;
        }
    }
}
