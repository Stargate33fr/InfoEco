using Infeco.Api.Infrastructure.MediatR;

namespace Infeco.Api.Commands.Bilan
{
    public abstract class EvtBilanCommand: Command
    {
        public string? Mail { get; set; }
        public DateTime? Date { get; set; }
    }
}
