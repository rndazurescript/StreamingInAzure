using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class PetSitter: Human
    {
        public const long GENERATION_MIN_ID = 1000;
        public const long GENERATION_MAX_ID = 1200;

        /// <summary>
        /// The current rating of this pet sitter
        /// </summary>
        public double Rating { get; set; }

        public double AverageWalkTimeInMinutes { get; set; }

    }
}
