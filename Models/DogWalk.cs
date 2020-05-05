using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class DogWalk
    {
        public Guid Id { get; set; }

        public long DogId { get; set; }

        public long PetSitterId { get; set; }

        public double WalkDurationInMinutes { get; set; }
    }
}
