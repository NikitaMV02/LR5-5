using System.Collections.Generic;

namespace ApiClientExample
{
    public class WordObject
    {
        public int Id { get; set; }
        public string CanonicalForm { get; set; }
        public string OriginalWord { get; set; }
        public List<string> Suggestions { get; set; }
        public string Vulgar { get; set; }
        public string Word { get; set; }
    }
}
