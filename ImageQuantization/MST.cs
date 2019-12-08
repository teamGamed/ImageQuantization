using System;
using System.Collections.Generic;
using System.Windows.Forms.VisualStyles;
using Microsoft.SqlServer.Server;

namespace ImageQuantization
{
    class MST
    {
        // getting MST with total complexity Elog(V^2) = 2 Elog(V) 
        
        Dictionary<RGBPixel, RGBPixel> p;

        private Dictionary<RGBPixel, int> rnk;
        // check if the 2 nodes are equal
        bool equals(RGBPixel first, RGBPixel second)
        {
            if (first.red == second.red && first.blue == second.blue && first.green == second.green)
                return true;
            return false;
        }
        RGBPixel parent(RGBPixel node)
        {
            if (equals(node, p[node]))
                return node;
            return p[node] = parent(p[node]);
        }
        // connect 2 nodes
        void connect(RGBPixel first, RGBPixel second)
        {
            RGBPixel a = parent(first), b = parent(second);
            if (equals(a, b)) return;
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
        bool isConnected(RGBPixel first, RGBPixel second)
        {
            if (equals(parent(first), parent(second)))
                return true;
            return false;
        }
        void getMST(int v)
        {
            for (int i = 1; i <= v; i++)
            {
                RGBPixel cur = Data.colors[i];
                p[cur] = cur;
                rnk[cur] = 0;
            }
                List<Tuple<double, RGBPixel, RGBPixel>> Edge = new List<Tuple<double, RGBPixel, RGBPixel>>();
            for (int i = 1; i <= v; i++)
            {
                for (int j = 1; j <= v; j++)
                {
                    Edge.Add(Tuple.Create(Data.distances[i][j], Data.colors[i], Data.colors[j]));
                }
            }
            Edge.Sort();
            foreach(Tuple<double, RGBPixel, RGBPixel> edge in Edge)
            {
                if (isConnected(edge.Item2, edge.Item3)) continue;
                connect(edge.Item2, edge.Item3);
                Data.MSTList[get_id(edge.Item2)].Add(get_id(edge.Item3));
                Data.MSTList[get_id(edge.Item3)].Add(get_id(edge.Item3));
            }
        }
    }
}
