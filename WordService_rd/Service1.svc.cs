using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Xml;
using HtmlAgilityPack;
using System.Net.Http;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;
using System.Text.RegularExpressions;
using WordService_rd;
using static WordService_rd.Synset;

namespace WordService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public string WordFilter(string str)
        {
            //initialize word array
            string[] strArr = str.Split(' ');
            List<string> list = strArr.ToList();
            List<string> output = list.ToList();

            //remove stop words
            foreach (string word in list)
            {
                if (StopWords.words.Contains(word)) output.Remove(word);
            }

            return string.Join(" ", output);
        }

        public string[] Top10Words(string url)
        {
            //download web page
            HttpClient client = new HttpClient();
            var response = client.GetStringAsync(url).Result;
            var doc = new HtmlDocument();
            doc.LoadHtml(response);
            var textNodes = new List<string>();
            foreach (HtmlNode node in doc.DocumentNode.DescendantsAndSelf())
            {
                if ( node.NodeType == HtmlNodeType.Text)
                {
                    string nodeText = node.InnerText.Trim();
                    if (nodeText != "") textNodes.Add(nodeText);
                }
            }

            //initialize word array, isolating alphebetic text
            string content = "";
            foreach (string node in textNodes)
            {
                content += node.ToString();
            }
            content = Regex.Replace(content, "[^a-zA-Z]", " ");
            string[] contentWords = content.Split(' ');

            //count the frequency of each word in a dictionary
            Dictionary<string, int> wordCounts = new Dictionary<string, int>();
            foreach (string word in contentWords)
            {
                if (word != "")
                {
                    if (wordCounts.ContainsKey(word)) wordCounts[word]++;
                    else wordCounts.Add(word, 1);
                }
            }

            //sort the words in descending order of frequency using a lambda expression
            var sorted = wordCounts.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

            //return the 10 most frequent 'words' in a string array
            string[] result = sorted.Keys.ToArray();
            string[] top10 = new string[10];
            for (int i = 0; i < 10; i++)
            {
                top10[i] = result[i];    
            }
            return top10;
        }
        
        public string Hypernym(string word1, string word2)
        {
            //find lowest common ancestors
            WordNet wordnet = new WordNet();
            int shortestAncestralPath = wordnet.SAP(word1, word2);
            List<noun> ancestors = wordnet.wordnet[shortestAncestralPath].synonyms;
            string output = ancestors[0].name + "; " + ancestors[0].definition;
            return output;
        }
    }
}
