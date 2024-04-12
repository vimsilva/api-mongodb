namespace api_mongodb.ChargeDatabase.Entities
{
    public class PokemonDetailsPayload
    {
        public int id { get; set; }
        public int order { get; set; }
        public string name { get; set; }
        public int height { get; set; }
        public int weight { get; set; }
        public Types[] types { get; set; }
    }

    public class Types
    {
        public Type type { get; set; }
    }

    public class Type
    {
        public string name { get; set; }
    }

}
