namespace BS360.Models;

public class objSolicitudHostQuery
{
    public string CodeDM { get; set; }
    public List<Samplenumber> SampleNumbers { get; set; }
    public class Samplenumber
    {
        public string SampleNumber { get; set; }
    }
}
