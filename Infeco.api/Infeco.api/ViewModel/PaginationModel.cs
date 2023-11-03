using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Infeco.Api.ViewModels
{
    /// <summary>
    /// Représente une pagination.
    /// </summary>
    public sealed class PaginationModel
    {
        /// <summary>
        /// Valeur par défaut pour le nombre d'éléments par page: 10.
        /// </summary>
        public static readonly int NombreElementParDefaut = 10;

        /// <summary>
        /// Valeur par défaut pour l'index de la page: 0.
        /// </summary>
        public static readonly int IndexParDefaut = 0;

        /// <summary>
        /// Le nombre d'élément dans la page (par défaut: 10).
        /// </summary>
        [FromQuery(Name = "limite")]
        public int Limite { get; set; }

        /// <summary>
        /// L'index de la page (default: 0).
        /// </summary>
        [FromQuery(Name = "index")]
        public int Index { get; set; }

        /// <summary>
        /// Le nombre d'élément à sauter dans le retour.
        /// </summary>
        [BindNever]
        public int Skip => Index * Limite;

        /// <summary>
        /// Initialise une nouvelle instance de <see cref="PaginationModel"/> avec les valeurs par défaut.
        /// See <see cref="NombreElementParDefaut"/> and <see cref="IndexParDefaut"/>.
        /// </summary>
        public PaginationModel() : this(IndexParDefaut, NombreElementParDefaut) { }

        /// <summary>
        /// Initialise une nouvelle instance de <see cref="PaginationModel" />.
        /// </summary>
        /// <param name="index">L'index de la page.</param>
        /// <param name="limite">Le nombre maximum d'élément.</param>
        public PaginationModel(int index, int limite)
        {
            Index = index;
            Limite = limite;
        }
    }
}