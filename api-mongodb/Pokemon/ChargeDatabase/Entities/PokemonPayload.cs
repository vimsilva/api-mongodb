namespace api_mongodb.ChargeDatabase.Entities;

public class PokemonPayload
{
    public int count { get; set; }
    public string next { get; set; }
    public object previous { get; set; }
    public Result[] results { get; set; }
}

public class Result
{
    public string name { get; set; }
    public string url { get; set; }
}
