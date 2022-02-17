using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WordleHelper
{
    public class WordleStartingWords
    {
        public async Task Run()
        {
            // Load all 5 letter words
            var words = await WordLoader.GetWords(5);
            Console.WriteLine($"Found {words.Count} 5-letter words.");

            await FindPairs(words);

            await FindTriples(words);
        }

        private async Task FindPairs(HashSet<string> words)
        {
            // Find the most common 10 letters
            var commonLetters = await GetMostCommonLetters(words, 10);
            Console.WriteLine($"Most common 10 letters: {string.Join(' ', commonLetters)}");

            // Find all words that have a combination of these letters
            var matchingWords = await GetWordsWithLetters(words, commonLetters);
            Console.WriteLine($"Matching words: {matchingWords.Count}");

            // Find distinct word pairs
            var wordPairs = await GetDistinctWordPairs(matchingWords);
            Console.WriteLine($"Good word pairs ({wordPairs.Count}): {string.Join(' ', wordPairs.Select(x => $"({x.Item1},{x.Item2})"))}");
        }

        private async Task FindTriples(HashSet<string> words)
        {
            // Find the most common 15 letters
            var commonLetters = await GetMostCommonLetters(words, 15);
            Console.WriteLine($"Most common 15 letters: {string.Join(' ', commonLetters)}");

            // Find all words that have a combination of these letters
            var matchingWords = await GetWordsWithLetters(words, commonLetters);
            Console.WriteLine($"Matching words: {matchingWords.Count}");

            // Find distinct word triples
            var wordTriples = await GetDistinctWordTriples(matchingWords);
            Console.WriteLine($"Good word triples ({wordTriples.Count}): {string.Join(' ', wordTriples.Select(x => $"({x.Item1},{x.Item2},{x.Item3})"))}");
        }

        private async Task<List<char>> GetMostCommonLetters(HashSet<string> words, int letterCount)
        {
            await Task.CompletedTask;

            var alphaDictionary = new List<char>
            {
                'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z'
            }.ToDictionary(x => x, y => new long());

            foreach (var word in words)
            {
                // Only increment counter once per letter per word
                var distinctWord = word.ToCharArray().Distinct().ToList();

                foreach (var letter in distinctWord)
                {
                    alphaDictionary[letter]++;
                }
            }

            var mostCommonLetters = alphaDictionary.OrderByDescending(x => x.Value).Select(x => x.Key).ToList();

            return mostCommonLetters.Take(letterCount).ToList();
        }

        private async Task<List<string>> GetWordsWithLetters(HashSet<string> words, List<char> letters)
        {
            await Task.CompletedTask;

            var matchingWords = new List<string>();

            foreach (var word in words)
            {
                var matches = 0;
                foreach (var letter in letters)
                {
                    if (word.Contains(letter))
                    {
                        matches++;
                    }
                }
                if (matches >= 5)
                {
                    matchingWords.Add(word);
                }
            }

            return matchingWords;
        }

        private async Task<List<Tuple<string, string>>> GetDistinctWordPairs(List<string> words)
        {
            await Task.CompletedTask;

            var distinctWordPairs = new List<Tuple<string, string>>();

            foreach (var word1 in words)
            {
                foreach (var word2 in words)
                {
                    if (!word1.Intersect(word2).Any())
                    {
                        distinctWordPairs.Add(Tuple.Create(word1, word2));
                    }
                }
            }

            return distinctWordPairs.Distinct().ToList();
        }

        private async Task<List<Tuple<string, string, string>>> GetDistinctWordTriples(List<string> words)
        {
            await Task.CompletedTask;

            var distinctWordTriples = new List<Tuple<string, string, string>>();

            foreach (var word1 in words)
            {
                foreach (var word2 in words)
                {
                    foreach (var word3 in words)
                    {
                        if (!word1.Intersect(word2).Any() &&
                            !word1.Intersect(word3).Any() &&
                            !word2.Intersect(word3).Any())
                        {
                            Console.WriteLine($"{word1} {word2} {word3}");
                            distinctWordTriples.Add(Tuple.Create(word1, word2, word3));
                        }
                    }
                }
            }

            return distinctWordTriples.Distinct().ToList();
        }
    }
}
