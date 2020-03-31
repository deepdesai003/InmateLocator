namespace PhiladelphiaInmateLocator.WebApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Threading.Tasks;

    public class Inmate
    {
        /// <summary>
        ///     Gets or sets the PrisonerID on this Inmate.
        /// </summary>
        public int Id { get; set; } = 0;

        /// <summary>
        ///     Gets or sets the FirstName on this Inmate.
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the LastName on this Inmate.
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the DOB of this Inmate.
        /// </summary>
        public DateTime DateOfBirth { get; set; } = default;

        /// <summary>
        ///     Gets or sets the Location on this Inmate.
        /// </summary>
        public string Location { get; set; } = string.Empty;
    }
}
