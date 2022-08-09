using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using static WordService_rd.Synset;

/*
 * NOTE: This is my implimentation of a WordNet project.
 * The idea comes from the following princeton assignment
 * https://www.cs.princeton.edu/courses/archive/fall11/cos226/assignments/wordnet.html
 * I searched online for the data files on the english language.
 * Source credit for synsets.txt and hypernyms.txt: https://github.com/arnabgho/Wordnet
 * It uses a directed acyclic bigraph of hypernyms in the English language to find the common parent of 2 nouns.
*/

namespace WordService_rd
{
    public class WordNet
    {
        public List<Synset> wordnet = new List<Synset>();

        public WordNet()
        {
            string _filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);

            StreamReader reader = new StreamReader(_filePath + "/App_Data/synsets.txt");
            string[] synonymLines = reader.ReadToEnd().Split('\n');
            reader = new StreamReader(_filePath + "/App_Data/hypernyms.txt");
            string[] hypernymLines = reader.ReadToEnd().Split('\n');

            for (int i = 0; i < synonymLines.Length; i++)
            {
                wordnet.Add(new Synset(synonymLines[i], hypernymLines[i]));
            }
        }

        //determines the degree of seperation
        public int DOS(string word1, string word2)
        {
            int synsetA = -1;
            int synsetB = -1;
            for(int i = 0; i < wordnet.Count; i++) 
            {
                foreach(noun n in wordnet[i].synonyms)
                {
                    if (word1.Equals(n.name)) synsetA = i;
                    else if (word2.Equals(n.name)) synsetB = i;
                }
            }
            ShortestAncestralPath sap = new ShortestAncestralPath(wordnet);
            return sap.length(synsetA, synsetB);
        }

        //determines the shortest ancestral path
        public int SAP (string word1, string word2)
        {
            int synsetA = -1;
            int synsetB = -1;
            for(int i = 0; i < wordnet.Count; i++)
            {
                foreach(noun n in wordnet[i].synonyms)
                {
                    if(word1.Equals(n.name)) synsetA = i;
                    else if(word2.Equals(n.name)) synsetB = i;
                }
            }
            ShortestAncestralPath sap = new ShortestAncestralPath(wordnet);
            return sap.ancestor(synsetA, synsetB);
        }
    }

    public class Synset
    {
        public List<noun> synonyms = new List<noun>();
        public List<int> hypernyms = new List<int>();

        public Synset(String synonymsLine, String hypernymsLine)
        {
            string[] syn = synonymsLine.Split(',');
            if (syn.Length > 1)
            {
                int syn_id = -1;
                Int32.TryParse(syn[0],out syn_id);
                noun n = new noun(syn_id, syn[1], syn[2]);
                synonyms.Add(n);
            }


            string[] hpnStr = hypernymsLine.Split(',');
            if (hpnStr.Length > 1)
            {
                int hpn_id = -1;
                Int32.TryParse(hpnStr[0], out hpn_id);
                for(int i = 1; i < hpnStr.Length; i++)
                {
                    int temp = -1;
                    Int32.TryParse(hpnStr[i],out temp);
                    hypernyms.Add(temp);
                }
            }
        }
    }

    public class noun
    {
        public int id;
        public string name;
        public string definition;

        public noun(int id, string name, string definition)
        {
            this.id = id;
            this.name = name;
            this.definition = definition;
        }
    }
}