using BussRoute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibrary
{
    /*Class that has all methods it needs to handle the search functionallity and to create the vertexes*/
    public class RouteCalculator
    {
        //Global variables
        public List<Vertex> VertexList { get; private set; }
        List<Vertex> checkParent;
        List<Node> visitedNodes;
        List<Node> unVisitedNodes;
        Vertex vertexToCheck = null;
        List<Node> unIdentifiedNodes;
        Dictionary<string, List<Vertex>> savedListsOfVertex;
        public RouteCalculator(List<Node> list)
        {
            unVisitedNodes = new List<Node>();
            visitedNodes = new List<Node>();
            checkParent = new List<Vertex>();
            VertexList = new List<Vertex>();
            unIdentifiedNodes = new List<Node>();
            savedListsOfVertex = new Dictionary<string, List<Vertex>>();
            
            CreateVertexList();

            list.ForEach((node) =>
            {
                unVisitedNodes.Add(node);
            });
        }
        /*This method is the one you call for when you want to find the shortest path. It will return
         * the Vertex ofthe end node and its shortest path from start*/
        public Vertex FindShortestPath(Node start, Node end, int sum)
        {
            if (!savedListsOfVertex.ContainsKey(start.Name))
            {
                visitedNodes = new List<Node>();

                //Following lightest weight to endnode
                FollowLighestWeight(start, end, sum);

                //Cheching unvisited nodes for shortest path
                for (int x = 0; x < unVisitedNodes.Count; x++)
                {
                    vertexToCheck = VertexList.Find(y => y.VertexName == unVisitedNodes[x].Name);

                    if (vertexToCheck.VertexName != start.Name)
                    {
                        CheckNodesThatWasntVisited(unVisitedNodes[x], vertexToCheck, start);
                    }
                }
                //Checking the nodes that did not have GoldenNumber the first turn
                if (unIdentifiedNodes.Count != 0)
                {
                    for (int i = 0; i < unIdentifiedNodes.Count; i++)
                    {
                        vertexToCheck = VertexList.Find(y => y.VertexName == unIdentifiedNodes[i].Name);
                        CheckNodesThatWasntVisited(unIdentifiedNodes[i], vertexToCheck, start);
                    }
                }

                savedListsOfVertex.Add(start.Name, VertexList);

                return VertexList.Find(x => x.VertexName == end.Name); 
            }
            else
            {
                VertexList = savedListsOfVertex.GetValueOrDefault(start.Name);
                return VertexList.Find(x => x.VertexName == end.Name);
            }

        }
        /*Checks if it can find end node and savs data for other nodes til Vertex. This does not 
         garante the the shortest path is found.*/
        private void FollowLighestWeight(Node startNode, Node endNode, int sumOfDistance)
        {
            visitedNodes.Add(startNode);

            int number = 11;
            int index = 0;
            bool endNodeFound = false;

            for (int x = 0; x < startNode.Connections.Count; x++)
            {
                vertexToCheck = VertexList.Find(y => y.VertexName == startNode.Connections[x].ConnectedNode.Name);
                if (endNode.Name == startNode.Connections[x].ConnectedNode.Name)
                {
                    AddInfoToVertex(vertexToCheck, startNode, sumOfDistance, x);
                    endNodeFound = true;
                }
                else if (unVisitedNodes.Contains(startNode.Connections[x].ConnectedNode))
                {
                    if (vertexToCheck.GoldenNumber == 0)
                    {
                        AddInfoToVertex(vertexToCheck, startNode, sumOfDistance, x);
                    }
                    else if (vertexToCheck.GoldenNumber > sumOfDistance + startNode.Connections[x].Weight)
                    {
                        AddInfoToVertex(vertexToCheck, startNode, sumOfDistance, x);
                    }
                    if (startNode.Connections[x].Weight < number && !visitedNodes.Contains(startNode.Connections[x].ConnectedNode))
                    {
                        number = startNode.Connections[x].Weight;
                        index = x;
                    }
                }

            }
            if (endNodeFound)
            {
                unVisitedNodes.Remove(startNode);
                return;
            }
            else
            {
                unVisitedNodes.Remove(startNode);
                FollowLighestWeight(startNode.Connections[index].ConnectedNode, endNode, sumOfDistance + startNode.Connections[index].Weight);
            }
        }
        /*This method is called when end node is found and are going to complete the Vertex list
         and probably fins the chortest path is it isent already found. Its going to be visiting all
         nodes that was never visited.
         If a shorter path is found it will be change and it will check agains already existing data*/
        private void CheckNodesThatWasntVisited(Node startNode, Vertex vertexOfStartNode, Node startName)
        {
            if (!visitedNodes.Contains(visitedNodes.Find(x => x.Name == startNode.Name)))
            {
                for (int i = 0; i < startNode.Connections.Count; i++)
                {
                    vertexToCheck = VertexList.Find(y => y.VertexName == startNode.Connections[i].ConnectedNode.Name);

                    if (vertexToCheck.GoldenNumber == 0 && vertexOfStartNode.GoldenNumber == 0)
                    {
                        //Console.WriteLine("Failar");
                        unIdentifiedNodes.Add(startNode);
                        unIdentifiedNodes.Add(startNode.Connections[i].ConnectedNode);
                    }
                    else
                    {
                        if (vertexOfStartNode.GoldenNumber == 0 && vertexToCheck.VertexName != vertexOfStartNode.ParentVertex)
                        {
                            vertexOfStartNode.AddGolden(startNode.Connections[i].Weight + vertexToCheck.GoldenNumber);
                            vertexOfStartNode.AddParent(startNode.Connections[i].ConnectedNode.Name);
                            vertexOfStartNode.AddDistans(startNode.Connections[i].Weight);
                        }
                        else if (vertexToCheck.GoldenNumber == 0 && vertexToCheck.VertexName != vertexOfStartNode.ParentVertex && vertexToCheck.VertexName != startName.Name)
                        {
                            vertexToCheck.AddDistans(startNode.Connections[i].Weight);
                            vertexToCheck.AddParent(startNode.Name);
                            vertexToCheck.AddGolden(vertexOfStartNode.GoldenNumber + startNode.Connections[i].Weight);
                        }
                        if (vertexToCheck.GoldenNumber > vertexOfStartNode.GoldenNumber + startNode.Connections[i].Weight)
                        {
                            vertexToCheck.AddGolden(startNode.Connections[i].Weight + vertexOfStartNode.GoldenNumber);
                            vertexToCheck.AddParent(startNode.Name);
                            
                            checkParent = VertexList.FindAll(x => x.ParentVertex == vertexToCheck.VertexName);
                            CheckParentNodes(checkParent, vertexToCheck);
                        }
                        if (vertexOfStartNode.GoldenNumber > vertexToCheck.GoldenNumber + startNode.Connections[i].Weight)
                        {
                            vertexOfStartNode.AddGolden(startNode.Connections[i].Weight + vertexToCheck.GoldenNumber);
                            vertexOfStartNode.AddParent(startNode.Connections[i].ConnectedNode.Name);
                            vertexOfStartNode.AddDistans(startNode.Connections[i].Weight);

                            checkParent = VertexList.FindAll(x => x.ParentVertex == vertexOfStartNode.VertexName);
                            CheckParentNodes(checkParent, vertexOfStartNode);
                        }
                    }
                }
            }
        }
        /*If a vertex gets changed in method above this methis is called to check the vertexes the current vertex
         is parent for*/
        public void CheckParentNodes(List<Vertex> list, Vertex input)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].GoldenNumber > list[i].Distance + input.GoldenNumber)
                {
                    list[i].AddGolden(list[i].Distance + input.GoldenNumber);
                    list[i].AddParent(input.VertexName);
                }
            }
        }
        //Create list
        private void CreateVertexList()
        {
            VertexList.Add(new Vertex("A"));
            VertexList.Add(new Vertex("B"));
            VertexList.Add(new Vertex("C"));
            VertexList.Add(new Vertex("D"));
            VertexList.Add(new Vertex("E"));
            VertexList.Add(new Vertex("F"));
            VertexList.Add(new Vertex("G"));
            VertexList.Add(new Vertex("H"));
            VertexList.Add(new Vertex("I"));
            VertexList.Add(new Vertex("J"));
        }
        /*Adding data to Vertex*/
        private Vertex AddInfoToVertex(Vertex vertex, Node startNode, int sumOfDistance, int indexOfConnection)
        {
            if (vertex.GoldenNumber == 0)
            {
                vertex.AddGolden(startNode.Connections[indexOfConnection].Weight + sumOfDistance);
                vertex.AddParent(startNode.Name);
                vertex.AddDistans(startNode.Connections[indexOfConnection].Weight);
            }
            else
            {
                if (startNode.Connections[indexOfConnection].Weight + sumOfDistance < vertex.GoldenNumber)
                {
                    vertex.AddGolden(startNode.Connections[indexOfConnection].Weight + sumOfDistance);
                    vertex.AddParent(startNode.Name);
                    vertex.AddDistans(startNode.Connections[indexOfConnection].Weight);
                }
            }
            return vertex;
        }

    }
}
