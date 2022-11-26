using System.Text;

namespace HemtestRESTAPI.Services
{
    public static class CountWordsService
    {
        

        public static string CountWords(string text)
        {
            //Remove special characters (except spaces and swedish characters) and then place them in a dictionary with the word as key and the number of times it appears as value
            Dictionary<string,int> countedwords = SumWords(RemoveSpecialCharacters(text));

            //Use the dictionary to return a string with the 10 words that occurs most frequently and their number of appearances
            return MergeDictionary(countedwords);
        }


        //method to remove special characters from a string while keeping å ä and ö
        public static string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || c == 'å' || c == 'ä' || c == 'ö' || c == 'Å' || c == 'Ä' || c == 'Ö' || c == ' ')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }





        //Method that counts the words in a string and returns the result as a dictionary with each word and its count
        public static Dictionary<string, int> SumWords(string text)
        {
            Dictionary<string, int> wordCount = new Dictionary<string, int>();

            //Split the string into words
            string[] words = text.Split(' ');

            //Loop through the words and add them to the dictionary
            foreach (string word in words)
            {
                if (wordCount.ContainsKey(word))
                {
                    wordCount[word]++;
                }
                else
                {
                    wordCount.Add(word, 1);
                }
            }

            return wordCount;
        }


        //Method to merge a dictionary with string and int, the string between quotes,into a single string
        public static string MergeDictionary(Dictionary<string, int> wordCount)
        {
            var ordered = wordCount.OrderByDescending(x => x.Value).ThenBy(x => x.Key);
            string result = "";

            //We only want the 10 most frequent words
            for (int i = 0; i < 10; i++)
            {
                result += "\"" + ordered.ElementAt(i).Key + "\": " + ordered.ElementAt(i).Value + ", ";
            }

            //Remove the last comma and space
            result = result.Remove(result.Length - 2);
            
            return result;
        }







        /*
         *  The following method is a suggestion from a colleague, it is a bit more complicated but it is faster and uses less memory //According to GitHub CoPilot :)))))
         */

        //Not yet tested for efficency (answers seems correct)

       
        //A method to count each word in a string and return a string with the 10 most frequent words and their number of appearances, the words are sorted by number of appearances, without using a dictionary
        public static string SumWordsCoPilot(string text)
        {
            //Split the string into words
            string[] words = text.Split(' ');

            //Create a list of strings to hold the words
            List<string> wordList = new List<string>();

            //Create a list of ints to hold the number of appearances of each word
            List<int> countList = new List<int>();

            //Loop through the words and add them to the list
            foreach (string word in words)
            {
                if (wordList.Contains(word))
                {
                    countList[wordList.IndexOf(word)]++;
                }
                else
                {
                    wordList.Add(word);
                    countList.Add(1);
                }
            }

            //Create a list of strings to hold the result
            List<string> resultList = new List<string>();

            //Loop through the words and add them to the result list
            for (int i = 0; i < wordList.Count; i++)
            {
                resultList.Add("\"" + wordList[i] + "\": " + countList[i]);
            }

            //Sort the result list by number of appearances
            resultList.Sort((x, y) => int.Parse(y.Split(':')[1]) - int.Parse(x.Split(':')[1]));

            //Create a string to hold the result
            string result = "";

            //We only want the 10 most frequent words
            for (int i = 0; i < 10; i++)
            {
                result += resultList[i] + ", ";
            }

            //Remove the last comma and space
            result = result.Remove(result.Length - 2);

            return result;
        }







    }
}
