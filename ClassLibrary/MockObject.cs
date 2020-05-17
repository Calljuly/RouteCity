using BussRoute;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    //Class to hold methods we need to test that are the same every time
    class MockObject
    {
        public List<Node> mockObject = new List<Node>();
        public MockObject()
        {
            CreateListNodes();
            CreateConnections();
        }
        public void CreateListNodes()
        {
            mockObject.Add(new Node("A"));
            mockObject.Add(new Node("B"));
            mockObject.Add(new Node("C"));
            mockObject.Add(new Node("D"));
            mockObject.Add(new Node("E"));

        }
        public void CreateConnections()
        {
            mockObject[0].Connections.Add(new Connections(mockObject[0], mockObject[1], 8));
            mockObject[0].Connections.Add(new Connections(mockObject[0], mockObject[3], 3));

            mockObject[1].Connections.Add(new Connections(mockObject[1], mockObject[2], 3));
            mockObject[1].Connections.Add(new Connections(mockObject[1], mockObject[0], 8));
            mockObject[1].Connections.Add(new Connections(mockObject[1], mockObject[3], 1));
            mockObject[1].Connections.Add(new Connections(mockObject[1], mockObject[4], 1));

            mockObject[2].Connections.Add(new Connections(mockObject[2], mockObject[1], 3));
            mockObject[2].Connections.Add(new Connections(mockObject[2], mockObject[4], 1));

            mockObject[3].Connections.Add(new Connections(mockObject[3], mockObject[0], 3));
            mockObject[3].Connections.Add(new Connections(mockObject[3], mockObject[1], 1));
            mockObject[3].Connections.Add(new Connections(mockObject[3], mockObject[4], 3));

            mockObject[4].Connections.Add(new Connections(mockObject[4], mockObject[3], 3));
            mockObject[4].Connections.Add(new Connections(mockObject[4], mockObject[1], 1));
            mockObject[4].Connections.Add(new Connections(mockObject[4], mockObject[2], 1));
        }
    }
}
