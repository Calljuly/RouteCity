using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("NUnitTestProject")]
namespace BussRoute
{
    //Class that holds on the methods to Connect nodes to each other
    public class RouteConnector
    {
        private Random randomConnectRouts;
        private Random randomAmountsOfConnections;
        public List<Node> Nodes { get; private set; }
        public RouteConnector()
        {
            Nodes = new List<Node>();
            randomConnectRouts = new Random();
            randomAmountsOfConnections = new Random();
            CreateListNodes();
        }

        /*Metod to call if you wanna generate nodes and thier different connections , time O(n)*/
        public void ConnectRouts()
        {
            //Adds one connection to each node. I some case after this one node will have more connections
            for (int i = 0; i < Nodes.Count; i++)
            {
                if (Nodes[i].Connections.Count < 1)
                {
                    AddDiffrentConnections(Nodes[i]);
                }
            }
            //If one Node only have one connection this loop will add one more
            for (int i = 0; i < Nodes.Count; i++)
            {
                if (Nodes[i].Connections.Count == 1)
                {
                    AddDiffrentConnections(Nodes[i]);
                }
            }
            //If one Node only have two connections this loop will add one more
            for (int i = 0; i < Nodes.Count; i++)
            {
                if (Nodes[i].Connections.Count == 2)
                {
                    AddDiffrentConnections(Nodes[i]);
                }
            }

        }
        /*Adds random conections to a node as long as nodeToConnect to is valid. That means that it isnt the same as input node nether it can be found in
        inputs connections, time O(n)*/
        private void AddDiffrentConnections(Node inputNode)
        {
            Node nodetoConnect;
            int countBreakLoop = 0;
            bool nodeToConnectIsntAlreadyConnected = false;

            /*If node to connect to already exist in inputnodes connections this will keep on going until
            a node is found*/
            while (countBreakLoop != 100000)
            {
                nodetoConnect = Nodes[randomConnectRouts.Next(0, Nodes.Count)];

                if (inputNode.Name != nodetoConnect.Name && nodetoConnect.Connections.Count < 3)
                {
                    nodeToConnectIsntAlreadyConnected = CheckIfNodeAlreadyExistAsConnection(inputNode, nodetoConnect);

                    //If true Connection is valid and will be added
                    if (nodeToConnectIsntAlreadyConnected)
                    {
                        inputNode.AddConnections(inputNode, nodetoConnect);
                        break;
                    }
                }
                countBreakLoop++;
            }
        }
        //Checks if the node to connect to isnt already among the other connections to the input node, time O(n)
        private bool CheckIfNodeAlreadyExistAsConnection(Node inputNode, Node nodeToConnect)
        {
            for (int x = 0; x < inputNode.Connections.Count; x++)
            {
                //If input node does not have any connections and isnt the same as node to connect to it is valid
                if (inputNode.Connections.Count == 0 && inputNode.Name != nodeToConnect.Name)
                {
                    return true;
                }
                else if (inputNode.Connections[x].ConnectedNode.Name == nodeToConnect.Name)
                {
                    return false;
                }
            }
            return true;
        }
        /*Create list of nodes */
        internal List<Node> CreateListNodes()
        {
            Nodes.Add(new Node("A"));
            Nodes.Add(new Node("B"));
            Nodes.Add(new Node("C"));
            Nodes.Add(new Node("D"));
            Nodes.Add(new Node("E"));
            Nodes.Add(new Node("F"));
            Nodes.Add(new Node("G"));
            Nodes.Add(new Node("H"));
            Nodes.Add(new Node("I"));
            Nodes.Add(new Node("J"));

            return Nodes;
        }
    }
}
