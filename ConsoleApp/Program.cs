using BussRoute;
using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        public static RouteConnector connector = new RouteConnector();
        public static RouteCalculator roadSearch = null;
        static void Main(string[] args)
        {
            connector.ConnectRouts();
            roadSearch = new RouteCalculator(connector.Nodes);
            Console.WriteLine("Hello and welcome to RoutCity!");
            Console.WriteLine("Here you can find the fastest way too you choosen part of the city.");
            Console.WriteLine("Given the options below make your choise : ");
            Console.WriteLine("Enter 1 : Show all Connections ");
            Console.WriteLine("Enter 2 : Choose your own path ");
            Console.WriteLine("Enter 3 : Exit the program ");

            while (true)
            {
                Console.WriteLine("Make a choise !");
                string userAnswer = Console.ReadLine();
                switch (userAnswer)
                {
                    case "1":
                        ShowAllConnections(connector);
                        break;
                    case "2":
                        ChoosePath();
                        break;

                    case "3":
                        Console.WriteLine("Thanks for visiting and welcome back!");
                        return;
                    default:
                        Console.WriteLine("Sorry, you have entered something invalid, try again!");
                        break;
                }
            }
        }
        /*This method is called when a user wants to search for a path, from start to end*/
        public static void ChoosePath()
        {
            //Local variables
            string startInput;
            string endInput;
            Node startNode ;
            Node endNode;
            Vertex resultShortestRoute;

            Console.WriteLine("Enter start position : ");
            startInput = Console.ReadLine();
            Console.WriteLine("Enter end position : ");
            endInput = Console.ReadLine();

            //Checks if input exist or not. If it does we will call  the method to calculate shortest route , if not user will be noticed
            if (connector.Nodes.Contains(connector.Nodes.Find(x => x.Name == startInput)) && connector.Nodes.Contains(connector.Nodes.Find(x => x.Name == endInput)))
            {
                startNode = connector.Nodes.Find(x => x.Name == startInput);
                endNode = connector.Nodes.Find(x => x.Name == endInput);

                resultShortestRoute = roadSearch.FindShortestPath(connector.Nodes[connector.Nodes.IndexOf(startNode)], connector.Nodes[connector.Nodes.IndexOf(endNode)], 0);

                Console.WriteLine("This is the way we can recomend as it appears to be the fastest : ");
                ShowChoosenPath(endInput, startInput);

                Console.WriteLine("The weight of this route is " + resultShortestRoute.GoldenNumber);
            }
            else
            {
                Console.WriteLine("Any of your choosen destinations does not exist");
            }
        }
        /*Method is called when you want to se what road is folloew in a choosen path*/
        public static void ShowChoosenPath(string startNode, string endNode)
        {
            Vertex vertexStart = roadSearch.VertexList.Find(y => y.VertexName == startNode);
            Vertex vertexOfStartParent = roadSearch.VertexList.Find(y => y.VertexName == vertexStart.ParentVertex);

            if (startNode == endNode)
            {
                Console.Write(startNode + ", End Destination ");
            }
            else
            {
                Console.Write(startNode + " , ");

                ShowChoosenPath(vertexOfStartParent.VertexName, endNode);
            }
        }
        
        /*Writes out all connections, time O(n2)*/
        public static void ShowAllConnections(RouteConnector a)
        {
            for (int x = 0; x < a.Nodes.Count; x++)
            {
                for (int y = 0; y < a.Nodes[x].Connections.Count; y++)
                {
                    Console.WriteLine(a.Nodes[x].Connections[y].NodeName.Name + a.Nodes[x].Connections[y].ConnectedNode.Name + a.Nodes[x].Connections[y].Weight);
                }
                Console.WriteLine();
            }
        }
    }
}
