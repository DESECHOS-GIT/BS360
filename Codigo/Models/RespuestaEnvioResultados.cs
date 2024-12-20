namespace BS360.Models;

public class RespuestaEnvioResultados
{
    public bool ok { get; set; }
    public string message { get; set; }
    public Data data { get; set; }


    public class Data
    {
        public Error[] errors { get; set; }
        public Success[] success { get; set; }
    }

    public class Success
    {
        public string sampleNumber { get; set; }
        public string message { get; set; }
        public string homologation { get; set; }
        public object interpretacion { get; set; }
        public string result { get; set; }
    }

    public partial class Error
    {
        public string SampleNumber { get; set; }
        public string Message { get; set; }
        public string Homologation { get; set; }
        public object Interpretacion { get; set; }
        public string Result { get; set; }
    }
}
