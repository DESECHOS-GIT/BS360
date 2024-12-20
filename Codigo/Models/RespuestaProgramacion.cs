namespace BS360.Models;

public class RespuestaProgramacion
{
    public bool ok { get; set; }
    public object message { get; set; }
    public DatosPaciente[] data { get; set; }

    public class DatosPaciente
    {
        public string sampleNumber { get; set; }
        public string name { get; set; }
        public string documentType { get; set; }
        public string document { get; set; }
        public string sex { get; set; }
        public DateTime birthDate { get; set; }
        public string medicalDevice { get; set; }
        public string reactive { get; set; }
        public Analyte[] analytes { get; set; }
    }

    public class Analyte
    {
        public string code { get; set; }
        public string name { get; set; }
        public string hostQueryCode { get; set; }
    }
}
