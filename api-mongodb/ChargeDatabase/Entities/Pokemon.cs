using System.Runtime.CompilerServices;

namespace api_mongodb.ChargeDatabase.Entities
{
    public class Pokemon
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int Order { get; set; }
        public string Name { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public IList<string> Type { get; set; }

        public void Copy(Pokemon poke)
        {
            this.Id = poke.Id;
            this.Order = poke.Order;
            this.Height = poke.Height;
            this.Weight = poke.Weight;
            this.Type = poke.Type;
        }
    }

   
}
