namespace PhiladelphiaInmateLocator.WebApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class Location
    {
        /// <summary>
        ///     Gets or sets the PrisonerID on this Inmate.
        /// </summary>
        public int Id { get; set; } = 0;

        public string Name { get; set; } = string.Empty;
    }
}
