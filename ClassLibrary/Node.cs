using System;
using System.Collections.Generic;

namespace BussRoute
{
    public class Node
    {
        public string Name { get; private set; }
        public List<Connections> Connections { get; private set; }
        private Random randomWeight;
        public Node(string name)
        {
            Name = name;
            Connections = new List<Connections>();
            randomWeight = new Random();
        }
        public void AddConnections(Node node, Node connectNode)
        {
            int weight = GenerateRandomWeight();
            Connections.Add(new Connections(node, connectNode, weight));
            connectNode.Connections.Add(new Connections(connectNode, node, weight));
        }
        private int GenerateRandomWeight()
        {
            return randomWeight.Next(1, 10);
        }
    }
}
