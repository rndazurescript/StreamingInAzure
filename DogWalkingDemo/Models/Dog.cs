using System;

namespace Models
{
    public class Dog
    {
        public const long GENERATION_MIN_ID = 1;
        public const long GENERATION_MAX_ID = 1000;

        public long Id { get; set; }

        public long OwnerId { get; set; }

        public string Name { get; set; }

        public double Height { get; set; }
        public double Weight { get; set; }

        public double Length { get; set; }
    }
}
