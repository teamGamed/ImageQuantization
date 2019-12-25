using System;
using System.Collections.Generic;
using System.Windows.Forms.VisualStyles;
using Microsoft.SqlServer.Server;

namespace ImageQuantization
{
    class MSTCr
    {
        private static int[] p = new int[Data.colorsNum];
        private static int[] rnk = new int[Data.colorsNum];
        
        private static int parent(int node)
        {
            if (node == p[node])
                return node;
            return p[node] = parent(p[node]);
        }
        // connect 2 nodes
        private static void connect(int first, int second)
        {
            int a = parent(first), b = parent(second);
            if (a == b) return;
            p[b] = a;
           /* if (rnk[a] < rnk[b])
                p[a] = b;
            else if (rnk[b] < rnk[a])
                p[b] = a;
            else
            {
                rnk[a]++;
                p[b] = a;
            }
            */
        }
        // check if the 2 nodes are already in the same component
        private static bool isConnected(int first, int second)
        {
            if (parent(first) == parent(second))
                return true;
            return false;
        }
        public static void getMST()
        {
            Data.MSTList = new List<int>[Data.colorsNum];
            for(int i = 0; i < Data.colorsNum; i++)
            {
                Data.MSTList[i] = new List<int>();
            }
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
            //Edge.Sort();
            int c = 0;
            foreach(Tuple<double, int, int> edge in Edge)
            {
                c++;
                if (c == Data.colorsNum)
                    break;
                /*if (isConnected(edge.Item2, edge.Item3)) continue;
                connect(edge.Item2, edge.Item3);*/
                Data.MSTList[edge.Item2].Add(edge.Item3);
                Data.MSTList[edge.Item3].Add(edge.Item3);
            }
        }
    }
}
