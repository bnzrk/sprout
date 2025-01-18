using Microsoft.Identity.Client;

namespace Sprout.Web.Data.Entities.Kanji
{
    public class Kanji
    {
        public int Id { get; set; }
        public string Literal { get; set; } = ""; // Character in UTF8
        public List<string> Meanings { get; set; } = new List<string>();
        public List<string> KunReadings { get; set; } = new List<string>(); // Chinese readings
        public List<string> OnReadings { get; set; } = new List<string>(); // Japanese readings
        public List<string> NanoriReadings { get; set; } = new List<string>(); // Japanese name readings
        //public string JISCodepoint { get; set; } = ""; // JIS code
        //public string UCSCodepoint { get; set; } = ""; // UCS code
        public int? Grade { get; set; } // Japanese grade level
        public int? JLPTLevel { get; set; }
        public int? Frequency { get; set; } // Use frequency ranking
        public int StrokeCount { get; set; }
        //public List<string> Variants { get; set; } = new List<string>(); // JIS codes of variants
    }
}
