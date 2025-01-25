using System.Xml.Linq;
using Sprout.Web.Data.Entities.Kanji;
using static Sprout.Web.Data.Entities.Kanji.Kanji;

namespace Sprout.Web.Data.Importers
{
    // Used to import a list of Kanji from a source
    public class KanjiImporter
    {
        public KanjiImporter() { }
        public List<Kanji> ImportFromXML(string path)
        {
            XDocument dict = XDocument.Load(path);
            if (dict.Root == null) 
            {
                Console.Error.WriteLine("Could not read dictionary file!");
                return new List<Kanji>();
            }

            // FIXME: Limit meanings to English via m_lang 
            // TODO: Add support for codepoints
            var kanjiList = dict.Root
                    .Descendants("character")
                    .Select(c => new Kanji
                    {
                        Literal = c.Element("literal")?.Value ?? "",
                        StrokeCount = int.TryParse(c.Element("misc")?.Element("stroke_count")?.Value, out int strokes) ? strokes : 0,
                        Frequency = int.TryParse(c.Element("misc")?.Element("freq")?.Value, out int frequency) ? frequency : 0,
                        Grade = int.TryParse(c.Element("misc")?.Element("grade")?.Value, out int grade) ? grade : 0,
                        JLPTLevel = int.TryParse(c.Element("misc")?.Element("jlpt")?.Value, out int jlpt) ? jlpt : 0,
                        //UCSCodepoint = c.Element("codepoint")
                        //    ?.Elements("cp_value")
                        //    .Where(e => e.Attribute("cp_type")?.Value == "ucs")
                        //    .FirstOrDefault()
                        //    ?.Value ?? "",
                        //JISCodepoint = c.Element("codepoint")
                        //    ?.Elements("cp_value")
                        //    .Where(e => e.Attribute("cp_type")?.Value == "jis")
                        //    .FirstOrDefault()
                        //    ?.Value ?? "",
                        //Variants = c.Element("misc")
                        //    ?.Elements("variant")
                        //    .Where(e => e.Attribute("var_type")?.Value == "jis212")
                        //    .Select(e => e.Value)
                        //    .ToList() ?? new List<string>(),
                        Meanings = c.Element("reading_meaning")
                            ?.Elements("rmgroup")
                            ?.Elements("meaning")
                            .Where(e => e.Attribute("m_lang") == null)
                            .Select(e => e.Value)
                            .ToList() ?? new List<string>(),
                        OnReadings = c.Element("reading_meaning")
                            ?.Elements("rmgroup")
                            ?.Elements("reading")
                            .Where(e => e.Attribute("r_type")?.Value == "ja_on")
                            .Select(e => e.Value)
                            .ToList() ?? new List<string>(),
                        KunReadings = c.Element("reading_meaning")
                            ?.Elements("rmgroup")
                            ?.Elements("reading")
                            .Where(e => e.Attribute("r_type")?.Value == "ja_kun")
                            .Select(e => e.Value)
                            .ToList() ?? new List<string>(),
                        NanoriReadings = c.Element("reading_meaning")
                            ?.Elements("nanori")
                            .Select(n => n.Value)
                            .ToList() ?? new List<string>()
                    })
                    .ToList();

            Console.WriteLine("Kanji read from file.");

            return kanjiList;
        }
    }
}
