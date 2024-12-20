namespace BS360.Models;

public class ObjResultados
{
    public string CodeDM { get; set; }
    public List<Insertresult> InsertResult { get; set; }
}

public class ResultadosInsertarTextoPlano
{
    public string CodeDM { get; set; }
    public List<Insertresult> ArrayAnalitosInsertadosConError { get; set; }
    public List<Insertresult> ArrayAnalitosInsertadosOk { get; set; }
}

public class Insertresult
{
    public string SampleNumber { get; set; }
    public string NumberDocument { get; set; }
    public string Homologation { get; set; }
    public string Interpretation { get; set; }
    public string Result { get; set; }
    public string UnitsMeasurement { get; set; }
    public string User { get; set; }
    public List<string> Images { get; set; }
}
