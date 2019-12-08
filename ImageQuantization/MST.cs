using System;
using System.Collections.Generic;
using System.Windows.Forms.VisualStyles;
using Microsoft.SqlServer.Server;

namespace ImageQuantization
{
    class MST
    {
        static private Dictionary<int ,int> p = new Dictionary<int, int>();

        static private Dictionary<int, int> rnk = new Dictionary<int, int>();
        
        int parent(int node)
        {
            if (node == p[node])
                return node;
            return p[node] = parent(p[node]);
        }
        // connect 2 nodes
        void connect(int first, int second)
        {
            int a = parent(first), b = parent(second);
            if (a == b) return;
            if (rnk[a] < rnk[b])
                p[a] = b;
            else if (rnk[b] < rnk[a])
                p[b] = a;
            else
            {
                rnk[a]++;
                p[b] = a;
            }
        }
        // check if the 2 nodes are already in the same component
        bool isConnected(int first, int second)
        {
            if (parent(first) == parent(second))
                return true;
            return false;
        }
        void getMST()
        {
            int v = Data.colorsNum;
            for (int cur = 0; cur < v; cur++)
            {
                p[cur] = cur;
                rnk[cur] = 0;
            }
            List < Tuple < double, int, int > > Edge = new List<Tuple<double, int, int>>();
            for (int i = 0; i < v; i++)
            {
                for (int j = 0; j < v; j++)
                {
                    Edge.Add(Tuple.Create(Data.distances[i,j], i, j));
                }
            }
            Edge.Sort();
            foreach(Tuple<double, int, int> edge in Edge)
            {
                if (isConnected(edge.Item2, edge.Item3)) continue;
                connect(edge.Item2, edge.Item3);
                Data.MSTList[edge.Item2].Add(edge.Item3);
                Data.MSTList[edge.Item3].Add(edge.Item3);
            }
        }
    }
}
