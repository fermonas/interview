using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTCDictionary
{
    public class LanguageDictionary : ILanguageDictionary
    {
        private Dictionary<string, List<string>> list;

        public LanguageDictionary(Dictionary<string, List<string>> list)
        {
            this.list = list;
        }

        public bool Check(string language, string word)
        {
            var listCheck = list.FirstOrDefault(i => i.Key == language && i.Value.IndexOf(word) >= 0);
            if (listCheck.Key == language && listCheck.Value.IndexOf(word) >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool Add(string language, string word)
        {
            if (list.Any(i => i.Key == language && i.Value.IndexOf(word) >= 0))
            {
                return false;
            }
            else if (list.Any(i => i.Key == language))
            {
                list[language].Add(word);
            }
            else
            {
                List<string> l = new List<string>();
                l.Add(word);
                list.Add(language, l);
            }

            return true;
        }

        public IEnumerable<string> Search(string word) 
        {
            int num = list.Count;
            List<string> finalList = new List<string>();
            for (int n = 0; n < num; n++)
            {
                List<string> value = list.Values.ElementAt(n);
                IEnumerable<string> iList = value.Where(i => i.StartsWith(word)) as IEnumerable<string>;
                finalList.AddRange(iList);
            }
            finalList.Sort();
            return finalList as IEnumerable<string>;
        }
    }
}
