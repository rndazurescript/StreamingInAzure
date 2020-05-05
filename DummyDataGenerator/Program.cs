using Bogus;
using Models;
using Newtonsoft.Json;
using System;
using System.IO;

namespace DummyDataGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Generating datasets: ");
            GenerateDogs();
            GenerateOwners();
            GeneratePetSitters();
            Console.WriteLine("done.");
        }

        public static void GenerateDogs()
        {
            var idSeed = Dog.GENERATION_MIN_ID;
            var datasetRules = new Faker<Dog>()
                //Ensure all properties have rules. By default, StrictMode is false
                //Set a global policy by using Faker.DefaultStrictMode
                .StrictMode(true)
                //OrderId is deterministic
                .RuleFor(o => o.Id, f => idSeed++)
                .RuleFor(o => o.Height, f => (double)f.Random.Decimal(0.2M, 1.0M))
                .RuleFor(o => o.Length, f => (double)f.Random.Decimal(0.2M, 1.5M))
                .RuleFor(o => o.Weight, f => (double)f.Random.Decimal(10M, 120M))
                .RuleFor(o => o.OwnerId, f => f.Random.Long( Owner.GENERATION_MIN_ID, Owner.GENERATION_MAX_ID))
                .RuleFor(o => o.Name, f => f.Name.LastName())
                ;
           
            var dataset = datasetRules.Generate((int) (Dog.GENERATION_MAX_ID - Dog.GENERATION_MIN_ID));
            File.WriteAllText("dogs.json", JsonConvert.SerializeObject(dataset));
        }

        public static void GenerateOwners()
        {
            var idSeed = Owner.GENERATION_MIN_ID;
            var datasetRules = new Faker<Owner>()
                //Ensure all properties have rules. By default, StrictMode is false
                //Set a global policy by using Faker.DefaultStrictMode
                .StrictMode(true)
                //OrderId is deterministic
                .RuleFor(o => o.Id, f => idSeed++)
                .RuleFor(o => o.FirstName, f => f.Name.FirstName())
                .RuleFor(o => o.LastName, f => f.Name.LastName())
                .RuleFor(o => o.BirthDay, f => f.Date.Between(DateTime.Now.AddYears(-100), DateTime.Now.AddYears(-12)))
                ;


            var dataset = datasetRules.Generate((int)(Owner.GENERATION_MAX_ID - Owner.GENERATION_MIN_ID));
            File.WriteAllText("owners.json", JsonConvert.SerializeObject(dataset));
        }

        public static void GeneratePetSitters()
        {
            var idSeed = PetSitter.GENERATION_MIN_ID;
            var datasetRules = new Faker<PetSitter>()
                //Ensure all properties have rules. By default, StrictMode is false
                //Set a global policy by using Faker.DefaultStrictMode
                .StrictMode(true)
                //OrderId is deterministic
                .RuleFor(o => o.Id, f => idSeed++)
                .RuleFor(o => o.FirstName, f => f.Name.FirstName())
                .RuleFor(o => o.LastName, f => f.Name.LastName())
                .RuleFor(o => o.BirthDay, f => f.Date.Between(DateTime.Now.AddYears(-100), DateTime.Now.AddYears(-12)))
                .RuleFor(o => o.Rating, f => f.Random.Double(0,5))
                .RuleFor(o => o.AverageWalkTimeInMinutes, f => f.Random.Double(1,25))
                ;

            var dataset = datasetRules.Generate((int)(PetSitter.GENERATION_MAX_ID - PetSitter.GENERATION_MIN_ID));
            File.WriteAllText("petsitters.json", JsonConvert.SerializeObject(dataset));
        }

    }
}
