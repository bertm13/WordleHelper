using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WordleHelper
{
    public static class WordLoader
    {
        private const string AllWordsFile = "words_alpha.txt";
        private const string WordleFile = "wordle-allowed-guesses.txt";

        private static HashSet<string> _allWords;

        public static async Task<HashSet<string>> GetWords()
        {
            if (_allWords == null)
            {
                _allWords = await LoadWords();
            }

            return _allWords;
        }

        public static async Task<HashSet<string>> GetWords(int length)
        {
            var words = await GetWords();
            return words.Where(x => x.Length == length).ToHashSet();
        }

        private static async Task<HashSet<string>> LoadWords()
        {
            var words = await File.ReadAllLinesAsync(WordleFile);
            var wordsHash = words.Select(x => x.ToLower()).ToHashSet();
            return wordsHash;
        }
    }
}
