using System.Threading.Tasks;

namespace WordleHelper
{
    internal class Program
    {
        internal static async Task Main(string[] args)
        {
            var app = new WordleStartingWords();
            await app.Run();
        }
    }
}
