using System;

namespace ClassLibrary
{
    //Object of this class is going to hold data about 
    public class Vertex
    {
        public string VertexName { get; private set; }
        public int Distance { get; private set; } = 0;
        public string ParentVertex { get; private set; }
        public int GoldenNumber { get; private set; } = 0;
        public Vertex(string nameVertex)
        {
            VertexName = nameVertex;
        }
        public void AddDistans(int distance)
        {
            Distance = distance;
        }
        public void AddParent(string parent)
        {
            ParentVertex = parent;
        }
        public void AddGolden(int golden)
        {
            GoldenNumber = golden;
        }
    }
}
