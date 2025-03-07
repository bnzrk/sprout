namespace Sprout.Web.Contracts
{
    public class SimpleKanjiDto
    {
        required public string Literal { get; set; }
        required public List<string> Meanings { get; set; }
        required public List<string> KunReadings { get; set; }
        required public List<string> OnReadings { get; set; }
    }
}
