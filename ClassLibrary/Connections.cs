using System;
using System.Collections.Generic;
using System.Text;

namespace BussRoute
{
    public class Connections
    {
        public Node NodeName { get; private set; }
        public Node ConnectedNode { get; private set; }
        public int Weight { get; private set; }
        public Connections(Node connect, Node toConnectTo, int lengthBetweenNodes)
        {
            NodeName = connect;
            ConnectedNode = toConnectTo;
            Weight = lengthBetweenNodes;
        }
    }
}
