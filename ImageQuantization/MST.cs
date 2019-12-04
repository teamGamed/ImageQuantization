using System.Collections.Generic;

namespace ImageQuantization
{
    class MST
    {
        // getting MST with total complexity Elog(V^2) = 2 Elog(V) 

        Dictionary<RGBPixel, RGBPixel> p;
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
            // O(log n) per operation
            p[parent(first)] = parent(second);
        }
        // check if the 2 nodes are already in the same component
        bool isConnected(RGBPixel first, RGBPixel second)
        {
            if (equals(parent(first), parent(second)))
                return true;
            return false;
        }
        void getMST()
        {

        }
    }
}
