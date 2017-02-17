using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSink
{

    class Node
    {
        public int toll;
        public string name;
        public int postVisit;
        public int preVisit;
        public bool visited;
        public int dist;
        public LinkedList<Node> edges = new LinkedList<Node>();
        public Node(string name, int toll)
        {
            this.name = name;
            this.toll = toll;

        }
    }
    class Program
    {
        int num = 1;
        List<Node> topoList = new List<Node>();
        
        static void Main(string[] args)
        {
            Program p = new Program();
            HashSet<Node> adList = new HashSet<Node>();
            List<Tuple<string, string>> tripsList = new List<Tuple<string, string>>();

            string[] test = { "4","Sourceville 5","SinkCity 10","Easton 20","Weston 15", "4", "Sourceville Easton", "Sourceville Weston",
            "Weston SinkCity", "Easton SinkCity", "6", "Sourceville SinkCity", "Easton SinkCity", "SinkCity SinkCity", "Weston Weston", "Weston Sourceville", "SinkCity Sourceville"};

            string[] test2 = {"2", "Here 10", "There 20", "0", "2", "Here There", "There There"};

            string[] test3 = { "7", "A 5", "B 10", "D 15", "E 20", "C 25", "G 40", "F 30", "6", "A B", "B D", "B E", "A C", "C G", "F G", "5", "A D", "A F", "B E", "A G", "B C"};

            string[] test4 = { "8", "A 5", "B 10", "D 15", "E 10", "C 25", "G 40", "F 30","K 5", "8", "A B", "B D", "B E", "A C", "C G", "F G","C K","E K", "5", "A D", "A F", "B E", "A G", "B C", "A K" };

            string[] test5 = { "10", "A 5", "B 10", "D 15", "E 10", "C 25", "G 40", "F 30", "K 5","Z 100", "Y 200", "9", "A B", "B D", "B E", "A C", "C G", "F G", "C K", "E K","Z Y", "11", "A D", "A F", "B E", "A G", "B C", "A K", "Z Y", "K Y", "A Z", "Z Z", "B A"};


            string line;
            int lineCount = 0;
            int section = 0;
            int nodesNumber = -1;
            int edgesNumber = -1;
            int tripsNumber = -1;

            //for(int i = 0; i < test.Length; i ++)
            while((line = Console.ReadLine()) != null)
            {
                //line = test[i];
                int d = 0;
                if (int.TryParse(line, out d))
                {
                    if (section == 0)
                    {
                        nodesNumber = d;
                    }
                    else if (section == 1)
                    {
                        edgesNumber = d;
                    }
                    else if (section == 2)
                    {
                        tripsNumber = d;
                    }
                    section++;
                }
                else
                {
                    string[] splitline = line.Split(null);
                    if (section == 1)
                    {
                        adList.Add(new Node(splitline[0], int.Parse(splitline[1])));
                    }
                    else if(section == 2)
                    {
                        
                        foreach (Node n in adList)
                        {
                            if(n.name.Equals(splitline[0]))
                            {
                                foreach(Node v in adList)
                                {
                                    if (v.name.Equals(splitline[1]))
                                    {
                                        n.edges.AddLast(v);
                                    }
                                }
                            }
                        }
                    }
                    else if(section == 3 && lineCount < (tripsNumber + edgesNumber + nodesNumber + 3))
                    {
                        tripsList.Add(new Tuple<string, string>(splitline[0], splitline[1]));
                    }
                }
                lineCount++;
             }
            p.depthFirstSearch(adList);
            foreach(Tuple<string, string> item in tripsList)
            {

                Node source = null;
                foreach(Node n in adList)
                {
                    if (item.Item1.Equals(n.name))
                    {
                        source = n;
                    }
                }
                Node dest = null;
                foreach (Node n in adList)
                {
                    if (item.Item2.Equals(n.name))
                    {
                        dest = n;
                    }
                }
                if (source != null)
                {
                    p.findPath(source, adList);
                }
                else
                {
                    //Console.WriteLine("ahhhhh");
                }
                
                if (dest.dist == Int32.MaxValue)
                {
                    Console.WriteLine("NO");
                }
                else
                {
                    Console.WriteLine(dest.dist);
                }
                //Console.WriteLine("Distance from: " + source.name + " and " + dest.name + " = " + result.ToString());
            }
           //Console.Read();
        }
        private void depthFirstSearch(HashSet<Node> graph)
        {
            foreach(Node n in graph)
            {
                n.visited = false;
            }
            foreach(Node n in graph)
            {
                if(!n.visited)
                {
                    explore(graph, n);
                }
            }
        }
        private void explore(HashSet<Node> graph, Node n)
        {
            n.visited = true;
            n.preVisit = num;
            num++;
            foreach (Node u in n.edges)
            {
                if(!u.visited)
                {
                    explore(graph, u);
                }
            }
            n.postVisit = num;
            num++;
            topoList.Add(n);
        }
        private void findPath(Node s, HashSet<Node> adList)
        {
            //int value = -1;
            Stack<Node> stack = new Stack<Node>();

            foreach(Node u in topoList)
            {
                //reverse?
                stack.Push(u);
                //u.dist = Int32.MaxValue;

            }
            foreach (Node u in adList)
            {
                //reverse?
                //stack.Push(u);
                u.dist = Int32.MaxValue;

            }
            s.dist = 0;

            while(stack.Count != 0)
            {
                Node u = stack.Pop();
                if(u.dist != Int32.MaxValue)
                {
                    foreach (Node v in u.edges)
                    {

                        if (v.dist > u.dist + v.toll)
                        {
                            v.dist = u.dist + v.toll;
                        }
                    }
                }


            }
            //foreach (Node n in topoList)
            //{
            //    if (n.name.Equals(dest.name))
            //    {
            //        value = n.dist;
            //    }
            //}
            //return value;
        }

    }
}
