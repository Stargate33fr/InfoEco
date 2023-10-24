using System.Runtime.Serialization;

namespace InfoEco.Domain.Request
{
    public class TriRequest
    {
        /// <summary>
        /// Le nom du champ à trier.
        /// </summary>
        [DataMember(Order = 1)]
        public string? Champ { get; set; }

        /// <summary>
        /// Indique si le tri est ascendant ou pas.
        /// </summary>
        [DataMember(Order = 2)]
        public bool? Ascendant { get; set; }
    }
}
