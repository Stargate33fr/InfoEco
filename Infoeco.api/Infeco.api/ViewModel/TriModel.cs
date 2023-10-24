using Microsoft.AspNetCore.Mvc;

namespace Infeco.Api.ViewModels
{
    /// <summary>
    /// Représente un tri de données.
    /// </summary>
    public sealed class TriModel
    {
        /// <summary>
        /// Valeur par défaut pour le nom du champ: "Id".
        /// </summary>
        public static readonly string ChampParDefaut = "Id";

        /// <summary>
        /// Valeur par défaut pour indiquer si le tri est ascendant: true.
        /// </summary>
        public static readonly bool AscendantParDefaut = true;

        /// <summary>
        /// Le nom du champ à trier (par défaut: "Id").
        /// </summary>
        [FromQuery(Name = "champ")]
        public string Champ { get; set; }

        /// <summary>
        /// Indique si le tri est ascendant ou pas.
        /// </summary>
        [FromQuery(Name = "ascendant")]
        public bool Ascendant { get; set; }

        /// <summary>
        /// Initialise une nouvelle instance de <see cref="TriMOdel"/> avec les valeurs par défaut.
        /// See <see cref="ChampParDefaut"/> and <see cref="AscendantParDefaut"/>.
        /// </summary>
        public TriModel() : this(ChampParDefaut, AscendantParDefaut) { }

        /// <summary>
        /// Initialise une nouvelle instance de <see cref="TriMOdel" />.
        /// </summary>
        /// <param name="champ">Le nom du champ à trier.</param>
        /// <param name="ascendant">Indique si le tri est ascendant.</param>
        public TriModel(string champ, bool ascendant)
        {
            Champ = champ;
            Ascendant = ascendant;
        }
    }
}