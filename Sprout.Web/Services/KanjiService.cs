using Sprout.Web.Contracts;
using Sprout.Web.Data.Entities.Kanji;
using Sprout.Web.Data.Importers;
using Sprout.Web.Mappings;

namespace Sprout.Web.Services
{
    public class KanjiService : IKanjiService
    {

        private readonly IKanjiRepository _repository;
        private readonly IWebHostEnvironment _environment;
        private readonly IMapper _mapper;

        public KanjiService(IWebHostEnvironment environment, IKanjiRepository repository, IMapper mapper)
        {
            _repository = repository;
            _environment = environment;
            _mapper = mapper;
        }

        public async Task CreateKanjiAsync(Kanji kanji)
        {
            await _repository.SaveKanjiAsync(kanji);
        }

        public async Task DeleteKanjiAsync(Kanji kanji)
        {
            await _repository.DeleteKanjiAsync(kanji);
        }

        public async Task DeleteKanjiByLiteralAsync(string literal)
        {
            var kanji = await _repository.GetKanjiByLiteralAsync(literal);
            await _repository.DeleteKanjiAsync(kanji);
        }

        public async Task<IEnumerable<Kanji>> GetAllKanjiAsync()
        {
            return await _repository.GetAllKanjiAsync();
        }

        public async Task<Kanji> GetKanjiByIdAsync(int id)
        {
            return await _repository.GetKanjiByIDAsync(id);
        }

        public async Task<Kanji> GetKanjiByLiteralAsync(string literal)
        {
            return await _repository.GetKanjiByLiteralAsync(literal);
        }

        public async Task<List<SimpleKanjiDto>> GetKanjiByLiteralsAsync(string[] literals)
        {
            var kanjis = await _repository.GetKanjiByLiteralsAsync(literals);
            List<SimpleKanjiDto> kanjiDtos = kanjis.Select(kanji => _mapper.MapKanjiToSimpleDto(kanji)).ToList();
            return kanjiDtos;
        }

        public async Task UpdateKanjiAsync(Kanji kanji)
        {
            await _repository.UpdateKanjiAsync(kanji);
        }

        public async Task PopulateDbAsync()
        {
            var existingKanji = await _repository.GetAllKanjiAsync();
            if (existingKanji.Any())
            {
                Console.WriteLine("Kanji table already populated. Skipping.");
                return;
            }

            var root = _environment.WebRootPath;
            string dictPath = Path.Combine(root, "files/dictionary/kanjidic", "kanjidic2.xml");
            if (!System.IO.File.Exists(dictPath))
            {
                Console.Error.WriteLine("Dictionary file not found!");
                return;
            }           

            var kanjiList = ReadKanjiFromFile(dictPath);
            if (kanjiList.Count == 0)
            {
                Console.Error.WriteLine("Could not load kanji from file! Database unchanged.");
                return;
            }
            Console.WriteLine("Attempting to populate database...");
            await _repository.SaveKanjiListAsync(kanjiList);
        }

        public List<Kanji> ReadKanjiFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine("Dictionary file not found!");
                return [];
            }

            KanjiImporter kanjiImporter = new();
            List<Kanji> kanjiList = kanjiImporter.ImportFromXML(filePath);

            return kanjiList;
        }
    }
}
