using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WordService
{
    //credit for list of words: Kavita Ganesan, PhD - https://kavita-ganesan.com/what-are-stop-words/#.YqYYhHbMIQ8
    public static class StopWords
    {
        public static readonly string[] words = new string[] { "a", "an", "another", "any", "certain", "each", "every", "her", "his", "its", "its", "my", "no", "our", "some", "that", "the", "their", "this", "and", "but", "or", "yet", "for", "nor", "so", "as", "aboard", "about", "above", "across", "after", "against", "along", "around", "at", "before", "behind", "below", "beneath", "beside", "between", "beyond", "but", "by", "down", "during", "except", "following", "for", "from", "in", "inside", "into", "like", "minus", "minus", "near", "next", "of", "off", "on", "onto", "onto", "opposite", "out", "outside", "over", "past", "plus", "round", "since", "since", "than", "through", "to", "toward", "under", "underneath", "unlike", "until", "up", "upon", "with", "without"};
    }

    public static class Punctuation
    {
        public static readonly char[] characters = new char[] { ' ', '.', '?', '"', '/', ',',  '-', '!', ':', ';', '(', ')', '[', ']', '<', '>', '/'  };  
    }
}