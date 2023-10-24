using System.Runtime.Serialization;

namespace InfoEco.Domain.Request
{
    public class PaginationRequest
    {
        /// <summary>
        /// Le nombre d'élément dans la page.
        /// </summary>
        [DataMember(Order = 1)]
        public int Limite { get; set; }

        /// <summary>
        /// L'index de la page.
        /// </summary>
        [DataMember(Order = 2)]
        public int Index { get; set; }

        /// <summary>
        /// Le nombre d'élément à sauter dans le retour.
        /// </summary>
        [DataMember(Order = 3)]
        public int Skip { get; set; }
    }
}
