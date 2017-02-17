using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumorMill
{
    class Node
    {
        public string _name;
        public int _distance;
        private Node prev;
        public LinkedList<Node> edges = new LinkedList<Node>();
        public Node(string name, int distance)
        {
            _name = name;
            _distance = distance;
        }
        public void setPrev(Node v)
        {
            prev = v;
        }
    }
    class Program
    {

        static void Main(string[] args)
        {

            string[] test = { "5", "Cam", "Art", "Edy", "Bea", "Dan", "3", "Bea Edy", "Dan Bea", "Art Dan", "2", "Dan", "Cam" };
            string[] test6 = { "5", "Cam", "Art", "Edy", "Bea", "Dan", "3", "Bea Edy", "Dan Bea", "Art Dan", "5", "Bea", "Art", "Dan", "Edy", "Cam" };
            string[] test2 = { "5", "Cam", "Art", "Edy", "Bea", "Dan", "0", "5", "Bea", "Art", "Dan", "Edy", "Cam" };
            string[] test5 = { "5", "Cam", "Art", "Edy", "Bea", "Dan","0","2", "Dan", "Cam" };
            string[] test7 = { "1","Cam", "0", "0"};
            string[] test8 = { "0", "0", "0" };
            string[] test9 = { "2", "Cam", "Bea", "0", "1", "Cam" };
            string[] test10 = { "1", "Cam", "0", "1", "Cam" };
            string[] test3 = { "3", "Cassandra", "Alberforth", "Buttrick", "1", "Cassandra Alberforth", "1", "Alberforth" };
            string[] test4 = {"11","V", "D", "J", "N", "A", "B", "C", "Z", "W", "X", "Y", "9","B J", "B N", "A B", "A C", "B Z", "B W", "C X", "C Y", "Z V", "1", "A"};

            Program p = new Program();
            string line;
            HashSet<Node> graph = new HashSet<Node>();
            List<Node> topoSortedArr = new List<Node>();
            int section = 0;
            int d;
            int count = 0;
            for (int i = 0; i < test4.Length; i ++)
            //while ((line = Console.ReadLine()) != null)
            {
                line = test4[i];
                if (int.TryParse(line, out d))
                {
                    section++;
                }
                else
                {
                    //we are in section where we are gettting all the students
                    if(section == 1)
                    {
                        graph.Add(new Node(line, int.MaxValue));
                    }
                    //we are in entering section where we are getting the pair of friends
                    else if(section == 2)
                    {
                        string[] pair = line.Split(null);

                        foreach (Node n in graph)
                        {
                            if (n._name.Equals(pair[0]))
                            {
                                foreach (Node v in graph)
                                {
                                    if (v._name.Equals(pair[1]))
                                    {
                                        n.edges.AddLast(v);
                                        v.edges.AddLast(n);
                                    }
                                }
                            }
                        }

                        //graph[pair[0]].Add(new Node(pair[1], int.MaxValue));
                        //graph[pair[1]].Add(new Node(pair[0], int.MaxValue));
                    }
                    //we are in the section of how many reports we need to make based on how many students started the rumor
                    else if(section == 3)
                    {
                        count++;
                        foreach(Node n in graph)
                        {
                           
                            if(n._name.Equals(line))
                            {
                                p.BFS(graph, n);
                                break;
                            }
                        }
                        List<Node> sortList = graph.OrderBy(n => n._distance).ThenBy(n =>n._name).ToList();
                        for(int j = 0; j < sortList.Count; j++)
                        {
                            if (j == sortList.Count - 1)
                            {
                                Console.Write(sortList[j]._name);
                            }
                            else
                            {
                                Console.Write(sortList[j]._name + " ");
                            }
                            
                        }
                        if((count >= d))
                        {
                            Console.Write("\n");
                        }
                        
                    }
                    
                }
            }
            Console.Read();
        }
        public void BFS(HashSet<Node> G, Node s)
        {
            foreach(Node u in G)
            {
                u._distance = int.MaxValue;
                u.setPrev(null);
            }
            s._distance = 0;

            Queue <Node> q = new Queue<Node>();
            q.Enqueue(s);

            while(q.Count != 0)
            {
                Node u = q.Dequeue();
                foreach(Node v in u.edges)
                {
                    if(v._distance == int.MaxValue)
                    {
                        q.Enqueue(v);
                        v._distance = u._distance + 1;
                        v.setPrev(u);
                    }
                }
            }
            
        }
    }
}
