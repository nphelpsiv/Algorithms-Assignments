using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyQuest
{
    class Star
    {
        public long x;
        public long y;
        public Star(long _x, long _y)
        {
            this.x = _x;
            this.y = _y;
        }
        
    }
    class GalaxyQuest
    {
        public long d = 0;
        public long k = 0;
        private Star xStar;
        private Star yStar;
        private int xStarCount = 0;
        private int yStarCount = 0;
        private static void Main(string[] args)
        {
            GalaxyQuest gq = new GalaxyQuest();
            string line;


            string[] test = { "10 4", "45 46", "90 47", "45 54", "90 43" };
            string[] test2 = { "20 7", "1 1", "100 100", "1 3", "101 101", "3 1", "102 102", "3 3" };

            string[] test4 = { "20 9", "100 100", "1 1", "1 3", "101 101","2 1", "3 1", "102 102", "3 3" ,"5000 3000" };

            string[] test3 = { "20 6", "100 100", "1 1", "1 3", "3 1", "3 3", "101 101"};

            string[] test5 = { "10 2", "1 1", "20 20"};

            string[] test6 = { "10 6", "1 1", "20 20", "2 2", "3 3", "50 50", "70 70"};

            string[] test7 = { "10 1", "1 1"};

            string[] test8 = { "20 7", "100 100", "1 1", "1 3", "1010 1010", "2 1","10200 10200", "3 3" };





            int count = 1;
            List<Star> stars = new List<Star>();
            while ((line = Console.ReadLine()) != null)
            //for(int l = 0; l < test5.Length; l++)
            {
                String[] splitLine = line.Split(null);
                //String[] splitLine = test5[l].Split(null);
                if (count != 1)
                {
                    stars.Add(new Star(long.Parse(splitLine[0]), long.Parse(splitLine[1])));
                }
                else
                {
                    gq.d = long.Parse(splitLine[0]);
                    gq.k = long.Parse(splitLine[1]);
                }
                count++;
            }
            Star answer = gq.findMajority(stars);
            if(answer == stars[0])
            {
                Console.WriteLine(1);
            }
            else if(answer == null)
            {
                Console.WriteLine("NO");
            }
            else if(answer == gq.xStar)
            {
                Console.WriteLine(gq.xStarCount);
            }
            else
            {
                Console.WriteLine(gq.yStarCount);
            }
            //Console.Read();
        }
        private Star findMajority(List<Star> stars)
        {
            
            if(stars.Count == 0)
            {
                return null;
            }
            else if(stars.Count == 1)
            {
                return stars[0];
            }
            else
            {
                int i = 0;
                List<Star> starsPrime = new List<Star>();
                bool isOdd = false;
                while(i <= stars.Count)
                {
                    if(k % 2 != 0)
                    {
                        yStar = stars[stars.Count - 1];
                        //stars.Remove(stars[stars.Count - 1]);
                        isOdd = true;
                    }
                    if((i + 1) >= stars.Count)
                    {
                        break;
                    }
                    long x1 = stars[i].x;
                    long y1 = stars[i].y;

                    long x2 = stars[i + 1].x;
                    long y2 = stars[i + 1].y;

                    if ((((x1 - x2) * (x1 - x2)) + ((y1 - y2) * (y1 - y2))) <= d * d)
                    {
                        starsPrime.Add(stars[i]);
                        //stars.Remove(stars[i + 1]);
                    }
                    else
                    {
                        //stars.Remove(stars[i]);
                        //stars.Remove(stars[i + 1]);
                    }
                    i = i + 2;
                }
                
                xStar = findMajority(starsPrime);
                if(xStar == null)
                {
                    if(k % 2 != 0)
                    {
                        //int yStarCount = 0;
                        for(int j = 0; j < stars.Count; j++)
                        {
                            long x1 = yStar.x;
                            long y1 = yStar.y;

                            long x2 = stars[j].x;
                            long y2 = stars[j].y;

                            if ((((x1 - x2) * (x1 - x2)) + ((y1 - y2) * (y1 - y2))) <= d * d)
                            {
                                yStarCount++;
                            }
                        }
                        if(yStarCount > stars.Count/2)
                        {
                            return yStar;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    
                    for (int j = 0; j < stars.Count; j++)
                    {
                        long x1 = xStar.x;
                        long y1 = xStar.y;

                        long x2 = stars[j].x;
                        long y2 = stars[j].y;

                        if ((((x1 - x2) * (x1 - x2)) + ((y1 - y2) * (y1 - y2))) <= d * d)
                        {
                            xStarCount++;
                        }
                    }
                    if (xStarCount > stars.Count / 2)
                    {
                        return xStar;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
    }
}

