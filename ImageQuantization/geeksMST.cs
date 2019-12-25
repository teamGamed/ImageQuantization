using System.Collections.Generic;
using System.ComponentModel;

namespace ImageQuantization
{
    public class geeksMST
    {
        // Number of vertices in the graph 
        // A utility function to find 
        // the vertex with minimum key 
        // value, from the set of vertices 
        // not yet included in MST 
        static int V = Data.colorsNum;
        static int minKey(double[] key, bool[] mstSet)
        {
            // Initialize min value 
            double min = double.MaxValue;
            int min_index = -1;

            for (int v = 0; v < V; v++)
                if (mstSet[v] == false && key[v] < min)
                {
                    min = key[v];
                    min_index = v;
                }

            return min_index;
        }

        // A utility function to print 
        // the constructed MST stored in 
        // parent[] 
        static void printMST(int[] parent)
        {
            Data.MSTList = new List<int>[Data.colorsNum];
            for(int i = 0; i < Data.colorsNum; i++)
            {
                Data.MSTList[i] = new List<int>();
            }
            for (int i = 1; i < V; i++)
            {
                Data.MSTList[parent[i]].Add(i);
                Data.MSTList[i].Add(parent[i]);
            }
        }

        // Function to construct and 
        // print MST for a graph represented 
        // using adjacency matrix representation 
        public static void primMST(double[,] graph)
        {
            V = Data.colorsNum;
            // Array to store constructed MST 
            int[] parent = new int[V];

            // Key values used to pick 
            // minimum weight edge in cut 
            double[] key = new double[V];

            // To represent set of vertices 
            // not yet included in MST 
            bool[] mstSet = new bool[V];

            // Initialize all keys 
            // as INFINITE 
            for (int i = 0; i < V; i++)
            {
                key[i] = double.MaxValue;
                mstSet[i] = false;
            }

            // Always include first 1st vertex in MST. 
            // Make key 0 so that this vertex is 
            // picked as first vertex 
            // First node is always root of MST 
            key[0] = 0;
            parent[0] = -1;

            // The MST will have V vertices 
            for (int count = 0; count < V - 1; count++)
            {
                // Pick thd minimum key vertex 
                // from the set of vertices 
                // not yet included in MST 
                int u = minKey(key, mstSet);

                // Add the picked vertex 
                // to the MST Set 
                mstSet[u] = true;

                // Update key value and parent 
                // index of the adjacent vertices 
                // of the picked vertex. Consider 
                // only those vertices which are 
                // not yet included in MST 
                for (int v = 0; v < V; v++)

                    // graph[u][v] is non zero only 
                    // for adjacent vertices of m 
                    // mstSet[v] is false for vertices 
                    // not yet included in MST Update 
                    // the key only if graph[u][v] is 
                    // smaller than key[v] 
                    if (graph[u, v] != 0 && mstSet[v] == false
                                         && graph[u, v] < key[v])
                    {
                        parent[v] = u;
                        key[v] = graph[u, v];
                    }
            }

            // print the constructed MST 
            printMST(parent);
        }
    }
}