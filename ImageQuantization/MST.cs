using System.Collections.Generic;
using System.ComponentModel;

namespace ImageQuantization
{
    public class MST
    {
        private static int V = 0;
        static int getSmallestNode(long [] cost, bool[] visited)
        {
            long mn = 1000000000;
            int idx = -1;
            for (int i = 0; i < V; i++)
            {
                if (!visited[i] && cost[i] < mn)
                {
                    mn = cost[i];
                    idx = i;
                }
            }
            return idx;
        }
        public static void getMST()
        {
            V = Data.colorsNum;
            int[] p = new int[V];
            long[] cost = new long[V];
            bool[] visited = new bool[V];
            for (int i = 0; i < V; i++)
            {
                visited[i] = false;
                cost[i] = 1000000000;
            }

            p[0] = -1;
            cost[0] = 0;
            for (int i = 0; i < V - 1; i++)
            {
                int smallest = getSmallestNode(cost, visited);
                visited[smallest] = true;
                for (int node = 0; node < V; node++)
                {
                    if (visited[node] == true) continue;
                    if (Data.getDis2(smallest, node) < cost[node])
                    {
                        p[node] = smallest;
                        cost[node] = Data.getDis2(smallest, node);
                    }
                }
            }
            Data.MSTList = new List<int>[V];
            for(int i = 0 ; i < V ; i++)
                Data.MSTList[i] = new List<int>();
            for (int i = 1; i < V; i++)
            {
                Data.MSTList[p[i]].Add(i);
                Data.sum += System.Math.Sqrt(Data.getDis2(p[i], i));
            }
        }
    }
}