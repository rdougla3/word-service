using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WordService_rd
{
    public class ShortestAncestralPath
    {
        int size;
        private List<int>[] graph;
        private List<int>[] bigraph;
        private List<int> path = new List<int>();
        private Queue<int> buf = new Queue<int>();
        private bool[] marked;
        private int[] edges;

        public ShortestAncestralPath(List<Synset> synsets)
        {
            size = synsets.Count;
            graph = new List<int>[size];
            bigraph = new List<int>[size];
            marked = Enumerable.Repeat(false, size).ToArray();
            edges = new int[size];

            //initialize directed digraph
            for(int v = 0; v < size; v++)
            {
                bigraph[v] = new List<int>();
                graph[v] = new List<int>();
                foreach (int hypernym in synsets[v].hypernyms)
                {
                    bigraph[v].Add(hypernym);
                    graph[v].Add(hypernym);
                }
            }
            for (int v = 0; v < size; v++)
            {
                foreach(int hypernym in synsets[v].hypernyms)
                {
                    bigraph[hypernym].Add(v);
                }
            }
        }

        public void discover(int origin, int destination)
        {
            //breadth-first search
            marked[origin] = true;
            buf.Enqueue(origin);
            int node = origin;
            //explore the graph
            while (node != destination)
            {
                node = buf.Dequeue();
                foreach (int hypernym in bigraph[node])
                {
                    if (!marked[hypernym])
                    {
                        marked[hypernym] = true;
                        buf.Enqueue(hypernym);
                        edges[hypernym] = node;
                    }
                }
            }
        }

        public int length(int origin, int destination)
        {
            discover(origin, destination);
            //retrace and count the steps
            int distance = 1;
            while (edges[destination] != origin)
            {
                distance++;
                destination = edges[destination];
            }
            return distance;
        }

        public int ancestor(int childA, int childB)
        {
            discover(childA, childB);
            //trace back the path that reached
            path.Add(childB);
            while (edges[childB] != childA)
            {
                childB = edges[childB];
                path.Add(childB);
            }
            foreach( int p in path)
            {
                //check that this is a nontrivial solution
                if(isParent(childA, p) && isParent(childB, p))
                {
                    return p;
                }
            }
            //in case of no hypernym for the word pair
            return -1;
        }

        public Boolean isParent(int child, int parent)
        {
            buf.Enqueue(child);
            marked = Enumerable.Repeat(false, size).ToArray();
            while(buf.Count > 0)
            {
                int node = buf.Dequeue();
                if (node == parent) return true;
                foreach (int hypernym in graph[node])
                {
                    if (!marked[hypernym])
                    {
                        marked[hypernym] = true;
                        buf.Enqueue(hypernym);
                    }
                }
            }
            return false;
        }
    }
}