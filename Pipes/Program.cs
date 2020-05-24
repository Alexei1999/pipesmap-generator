using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Pipes
{
    class Program
    {
        static Random srand = new Random();
        static void Print()
        {
            Console.Clear();
            if (Map.map == null) { Console.ForegroundColor = ConsoleColor.Red; return; }
            for (int i = 0; i < Map.map.GetLength(0); i++)
            {
                for (int l = 0; l < Map.map.Length / Map.map.GetLength(0); l++)
                {
                    if (Map.map[i, l].IsItEmpty())
                        if (Map.map[i, l].IsAgressive()) Console.ForegroundColor = ConsoleColor.Yellow;
                        else Console.ForegroundColor = ConsoleColor.Red;
                    else if (Map.map[i, l].IsAgressive()) Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.Write(Map.map[i, l].ToString());
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }
        class Pipe
        {
            enum PipeTypes { Agressive, Passive };
            PipeTypes type;
            public BitArray lurd;
            public BitArray watr;
            bool watered = false;
            public Pipe() {
                lurd = new BitArray(4, false);
                watr = new BitArray(4, false);
                type = PipeTypes.Passive;
            }
            public Pipe(bool l, bool u, bool r, bool d) : this()
            {
                lurd[0] = l;
                lurd[1] = u;
                lurd[2] = r;
                lurd[3] = d;
            }
            public Pipe(BitArray temp) : this()
            {
                lurd = temp;
            }
            public Pipe(string a) : this()
            {
                if (a.Length > 4 || a.Length < 4) return;
                lurd[0] = Convert.ToBoolean(char.GetNumericValue(a[0]));
                lurd[1] = Convert.ToBoolean(char.GetNumericValue(a[1]));
                lurd[2] = Convert.ToBoolean(char.GetNumericValue(a[2]));
                lurd[3] = Convert.ToBoolean(char.GetNumericValue(a[3]));
            }
            public Pipe(int[] a) : this()
            {
                if (a.Length > 4 || a.Length < 4) return;
                lurd[0] = Convert.ToBoolean(a[0]);
                lurd[1] = Convert.ToBoolean(a[1]);
                lurd[2] = Convert.ToBoolean(a[2]);
                lurd[3] = Convert.ToBoolean(a[3]);
            }
            public bool IsAgressive()
            {
                if (type == PipeTypes.Passive) return false;
                return true;
            }
            public bool IsItEmpty()
            {
                if (lurd[0] | lurd[1] | lurd[2] | lurd[3])
                    return false;
                return true;
            }
            public bool Up()
            {
                if (lurd[1]) return true;
                return false;
            }
            public bool Left()
            {
                if (lurd[0]) return true;
                return false;
            }
            public bool Right()
            {
                if (lurd[2]) return true;
                return false;
            }
            public bool Down()
            {
                if (lurd[3]) return true;
                return false;
            }

            public bool WaterUp()
            {
                if (watr[1]) return true;
                return false;
            }
            public bool WaterLeft()
            {
                if (watr[0]) return true;
                return false;
            }
            public bool WaterRight()
            {
                if (watr[2]) return true;
                return false;
            }
            public bool WaterDown()
            {
                if (watr[3]) return true;
                return false;
            }

            public void SetAgressive()
            {
                type = PipeTypes.Agressive;
            }
            public void CalmDown()
            {
                type = PipeTypes.Passive;
            }
            public void SetUp()
            {
                lurd[1] = true;
            }
            public void SetLeft()
            {
                lurd[0] = true;
            }
            public void SetRight()
            {
                lurd[2] = true;
            }
            public void SetDown()
            {
                lurd[3] = true;
            }

            public void UnSet()
            {
                lurd = new BitArray(4, false);
                type = PipeTypes.Passive;
            }
            public void UnUp()
            {
                lurd[1] = false;
            }
            public void UnLeft()
            {
                lurd[0] = false;
            }
            public void UnRight()
            {
                lurd[2] = false;
            }
            public void UnDown()
            {
                lurd[3] = false;
            }
            public override string ToString()
            {
                if (IsItEmpty())
                    if (type == PipeTypes.Passive) return "·";
                    else return "°";
                if (Up() && Right() && Down() && Left())
                {
                    if ((WaterUp() || WaterDown()) && (WaterLeft() || WaterRight()))
                        return "╬";
                    if (WaterUp() || WaterDown())
                        return "╫";
                    if (WaterLeft() || WaterRight())
                        return "╪";
                    return "┼";
                }

                if (Up() && Right() && Down())
                {
                    if ((WaterUp() || WaterDown()) && WaterRight())
                        return "╠";
                    if (WaterUp() || WaterDown())
                        return "╟";
                    if (WaterRight())
                        return "╞";
                    return "├";
                }
                if (Left() && Right() && Up())
                {
                    if ((WaterLeft() || WaterRight()) && WaterUp())
                        return "╩";
                    if (WaterLeft() || WaterRight())
                        return "╧";
                    if (WaterUp())
                        return "╨";
                    return "┴";
                }
                if (Left() && Up() && Down())
                {
                    if ((WaterUp() || WaterDown()) && WaterLeft())
                        return "╣";
                    if (WaterUp() || WaterDown())
                        return "╢";
                    if (WaterLeft())
                        return "╡";
                    return "┤";
                }
                if (Left() && Right() && Down())
                {
                    if ((WaterLeft() || WaterRight()) && WaterDown())
                        return "╦";
                    if (WaterLeft() || WaterRight())
                        return "╤";
                    if (WaterDown())
                        return "╥";
                    return "┬";
                }

                if (Up() && Down())
                {
                    if (WaterUp() || WaterDown())
                        return "║";
                    return "│";
                }
                if (Right() && Left())
                {
                    if (WaterRight() || WaterLeft())
                        return "═";
                    return "─";
                }

                if (Right() && Down())
                {
                    if (WaterRight() && WaterDown())
                        return "╔";
                    if (WaterRight())
                        return "╒";
                    if (WaterDown())
                        return "╓";
                    return "┌";
                }
                if (Right() && Up())
                {
                    if (WaterRight() && WaterUp())
                        return "╚";
                    if (WaterRight())
                        return "╘";
                    if (WaterUp())
                        return "╙";
                    return "└";
                }
                if (Left() && Up())
                {
                    if (WaterLeft() && WaterUp())
                        return "╝";
                    if (WaterLeft())
                        return "╛";
                    if (WaterUp())
                        return "╜";
                    return "┘";
                }
                if (Left() && Down())
                {
                    if (WaterLeft() && WaterDown())
                        return "╗";
                    if (WaterLeft())
                        return "╕";
                    if (WaterDown())
                        return "╖";
                    return "┐";
                }

                if (Left())
                {
                    if (WaterLeft())
                        return "{";
                    return "<";
                }
                if (Up())
                {
                    if (WaterUp())
                        return "n";
                    return "^";
                }
                if (Right())
                {
                    if (WaterRight())
                        return "}";
                    return ">";
                }
                if (Down())
                {
                    if (WaterDown())
                        return "u";
                    return "v";
                }
                return "X";
            }
        }
        class cord
        {
            int A;
            int B;
            public int a
            {
                get
                {
                    return A;
                }
                set
                {
                    if (value >= 0)
                        A = value;
                    else
                        A = 0;
                }
            }
            public int b
            {
                get
                {
                    return B;
                }
                set
                {
                    if (value >= 0)
                        B = value;
                    else
                        B = 0;
                }
            }
            public cord()
            {
                a = 0;
                b = 0;
            }
            public cord(int a, int b)
            {
                this.a = a;
                this.b = b;
            }
            public cord(cord c)
            {
                a = c.a;
                b = c.b;
            }
        }
        class Path
        {
            public int[] path;
            public List<cord[]> blocks = new List<cord[]>();
            public Path(int[] path)
            {
                this.path = path;
                List<cord> coords = PathInCord(path).ToList();
                bool horz = true;
                List<cord> temp = new List<cord>();
                blocks = new List<cord[]>();
                for (int i = 0; i < coords.Count; i++)
                {
                    cord dum = coords[i];
                    if (i == 0)
                    {
                        temp.Add(coords[i]);
                        continue;
                    }
                    if (horz == true)
                    {
                        if (dum.b != temp.Last().b || i == coords.Count - 1)
                        {
                            horz = false;
                            if (i != 0 && (temp.Count != 1 || i == coords.Count - 1))
                            {
                                if (temp[0] != coords[i] && i == coords.Count - 1)
                                    if (coords[i].a == temp[0].a || coords[i].b == temp[0].b)
                                        temp.Add(coords[i]);
                                    else i--;
                                blocks.Add(temp.ToArray());
                                temp = new List<cord>();
                            }
                        }
                    }
                    else
                    {
                        if (dum.a != temp.Last().a || i == coords.Count - 1)
                        {
                            horz = true;
                            if (temp.Count != 1 || i == coords.Count - 1)
                            {
                                if (temp[0] != coords[i] && i == coords.Count - 1)
                                    if (coords[i].a == temp[0].a || coords[i].b == temp[0].b)
                                        temp.Add(coords[i]);
                                    else i--;
                                blocks.Add(temp.ToArray());
                                temp = new List<cord>();
                            }
                        }
                    }
                    temp.Add(dum);
                }
            }
            public static cord[] PathInCord(int[] foo)
            {
                List<cord> cord = new List<cord>();
                List<int> path = foo.ToList();
                for (int i = 0; i < 2; i++)
                    path.Insert(0, 0);
                for (int i = 0; i < path.Count - 1; i++)
                    if (i % 2 == 0) cord.Add(new cord(path[i + 1], path[i]));
                    else cord.Add(new cord(path[i], path[i + 1]));
                for (int i = 0; i < cord.Count - 1; i++)
                {
                    int k = 0;
                    if (cord[i].a == cord[i + 1].a)
                        for (int j = cord[i].b + 1; j < cord[i + 1 + k].b; j++, k++)
                            cord.Insert(i + k + 1, new cord(cord[i].a, j));
                    if (cord[i].b == cord[i + 1].b)
                        for (int j = cord[i].a + 1; j < cord[i + 1 + k].a; j++, k++)
                            cord.Insert(i + k + 1, new cord(j, cord[i].b));
                    i += k;
                }
                if (cord[0].a == cord[1].a && cord[1].b == cord[1].b && cord[0].a == 0 && cord[1].b == 0)
                    cord.RemoveAt(0);
                return cord.ToArray();
            }
            public void InsertBlockList(List<cord[]> temp)
            {
                if (temp == null) return;
                for (int i = 0; i < temp.Count; i++)
                    if (temp[i] == null) temp.RemoveAt(i--);
                blocks=blocks.Concat(temp).ToList();
            }
            public void RemoveBlock(int i)
            {
                if (i < 0 || i > blocks.Count)
                    return;
                blocks.RemoveAt(i);
            }
            public int GetLength()
            {
                int i = 0;
                foreach (cord[] element in blocks)
                    i += element.Length;
                return i;
            }
            public void GetBlocks()
            {
                foreach (cord[] element in blocks)
                {
                    foreach (cord elm in element)
                        Console.Write("{0}-{1};", elm.a, elm.b);
                    Console.WriteLine();
                }
            }
        }
        static class Map
        {
            public static Pipe[,] map;
            
            static bool IsItEmpty(int a, int b)
            {
                if (a < 0 || b < 0 || b >= map.Length / map.GetLength(0) || a >= map.GetLength(0))
                    return false;
                if (map[a, b].IsItEmpty()) return true;
                return false;
            }
            static void Inicialize(int a, int b)
            {
                map = new Pipe[a, b];
                for (int i = 0; i < a; i++)
                    for (int j = 0; j < b; j++)
                        map[i, j] = new Pipe();
            }
            static void SetTube(int a, int b)
            {
                if (a == 0 && b == 0) map[a, b].SetUp();
                if (a == map.GetLength(0) - 1 && b == map.Length / map.GetLength(0) - 1) map[a, b].SetDown();
                if (b > 0)
                {
                    if (map[a, b - 1].Right() || map[a, b - 1].IsAgressive())
                        map[a, b].SetLeft();
                    if (map[a, b - 1].IsAgressive())
                        map[a, b - 1].SetRight();
                }
                if (b < map.Length / map.GetLength(0) - 1)
                {
                    if (map[a, b + 1].Left() || map[a, b + 1].IsAgressive())
                        map[a, b].SetRight();
                    if (map[a, b + 1].IsAgressive())
                        map[a, b + 1].SetLeft();
                }
                if (a > 0)
                {
                    if (map[a - 1, b].Down() || map[a - 1, b].IsAgressive())
                        map[a, b].SetUp();
                    if (map[a - 1, b].IsAgressive())
                        map[a - 1, b].SetDown();
                }
                if (a < map.GetLength(0) - 1)
                {
                    if (map[a + 1, b].Up() || map[a + 1, b].IsAgressive())
                        map[a, b].SetDown();
                    if (map[a + 1, b].IsAgressive())
                        map[a + 1, b].SetUp();
                }
            }
            static void UnTube(int a, int b)
            {
                if (b < 0 || a >= map.GetLength(0) || a < 0 || b > map.Length / map.GetLength(0))
                    return;
                if (a > 0)
                    map[a - 1, b].UnDown();
                if (a < map.GetLength(0) - 1)
                    map[a + 1, b].UnUp();
                if (b > 0)
                    map[a, b - 1].UnRight();
                if (b < map.Length / map.GetLength(0) - 1)
                    map[a, b + 1].UnLeft();
                map[a, b].UnSet();
            }
            static void CalmDown(int a, int b)
            {
                if (b < 0 || a >= map.GetLength(0) || a < 0 || b > map.Length / map.GetLength(0))
                    return;
                if (a > 0)
                    map[a - 1, b].CalmDown();
                if (a < map.GetLength(0) - 1)
                    map[a + 1, b].CalmDown();
                if (b > 0)
                    map[a, b - 1].CalmDown();
                if (b < map.Length / map.GetLength(0) - 1)
                    map[a, b + 1].CalmDown();
                map[a, b].CalmDown();
            }
            static void DrowMap(cord[] arr)
            {
                if (arr == null|| arr.Length == 0) return;
                int a = map.GetLength(0);
                int b = map.Length / map.GetLength(0);
                bool[,] nMap = new bool[a, b];
                for (int i = 0; i < arr.Length; i++)
                {
                    if (i == 0) {
                        SetTube(arr[i].a, arr[i].b);
                        map[arr[i].a, arr[i].b].SetAgressive();
                        continue;
                    }
                    SetTube(arr[i].a, arr[i].b);
                    CalmDown(arr[i].a, arr[i].b);
                    map[arr[i].a, arr[i].b].SetAgressive();
                }
                CalmDown(arr[arr.Length - 1].a, arr[arr.Length - 1].b);
            }
            public static void SetMap(int a, int b)
            {
                
                Inicialize(a, b);
                int[] path = GetMinPath();
                Path defPath = new Path(path.ToArray());
                AddToPath(defPath,10000);
                Print();
                Console.WriteLine(defPath.GetLength());
                Console.ReadKey();
            }
            public static int[] GetMinPath()
            {
                Random srand = new Random();
                List<int> path = new List<int>();
                int a = map.GetLength(0);
                int b = map.Length / map.GetLength(0);
                bool exit = true;
                while (exit)
                {
                    if (path.Count < 2)
                    {
                        path.Add(srand.Next(0, b - 1));
                        path.Add(srand.Next(0, a - 1));
                    }
                    if (path[path.Count - 2] == b - 1)
                    {
                        exit = false;
                        path[path.Count - 1] = a - 1;
                        break;
                    }
                    if (path[path.Count - 1] == a - 1)
                    {
                        exit = false;
                        path.Add(b - 1);
                        break;
                    }
                    path.Add(srand.Next(path[path.Count - 2] + 1, b - 1));
                    path.Add(srand.Next(path[path.Count - 2] + 1, a - 1));
                }
                if (path[0] == 0 && path[1] == 0)
                {
                    path.RemoveAt(0);
                    path.RemoveAt(0);
                }
                if (path.Count > 2 && path[1] == 0 && path[3] != 0)
                {
                    path.RemoveAt(0);
                    path.RemoveAt(0);
                }
                DrowMap(Path.PathInCord(path.ToArray()));
                return path.ToArray();
            }
            //public static int[] GetAnotherPath(int[] fPath)
            //{
            //    Random srand = new Random();
            //    List<int> path = new List<int>();
            //    int a = map.GetLength(0);
            //    int b = map.Length / map.GetLength(0);
            //    bool exit = true;
            //}
            public static void AddToPath(Path path, ulong c = 0)
            {
                if (c == 0) return;
                ulong min = (ulong)(map.GetLength(0) + map.Length / map.GetLength(0) - 1);
                ulong max = (ulong)(map.GetLength(0) * map.Length / map.GetLength(0) - (1 - map.GetLength(0) & 1));
                if (c > max-min) c = max-min;

                bool fExit = true;
                List<cord[]> Blocks = new List<cord[]>(path.blocks);
                path.blocks = new List<cord[]>();
                while (fExit)
                {
                    if (Blocks.Count == 0)
                    {
                        fExit = false;
                        continue;
                    }
                    Console.Clear();
                    Console.WriteLine(Blocks.Count());
                    cord[] cArr = Blocks[srand.Next(0, Blocks.Count - 1)];
                    if (cArr.Length < 2) {
                        Blocks.Remove(cArr);
                        path.blocks.Add(cArr);
                        continue;
                    }

                    List<List<int>> CordRandom = new List<List<int>>();
                    for (int g = 0; g < cArr.Length - 1; g++)
                    {
                        List<int> temp = new List<int>();
                        for (int i = g; i < cArr.Length; i++)
                            temp.Add(i);
                        CordRandom.Add(temp);
                    }
                    bool sExit = true;
                    while(sExit)
                    {
                        int s = srand.Next(0, CordRandom.Count - 1);
                        if (CordRandom[s].Count < 2) {
                            CordRandom.Remove(CordRandom[s]);
                            if (CordRandom.Count == 0)
                                return;
                            continue;
                        }
                        int z = srand.Next(1, CordRandom[s].Count - 1);
                        int a = CordRandom[s][0];
                        int b = CordRandom[s][z];

                        List<KeyValuePair<int, string>> plurd = new List<KeyValuePair<int, string>> {
                            new KeyValuePair<int, string>(-1, "left"),
                            new KeyValuePair<int, string>(-1, "up"),
                            new KeyValuePair<int, string>(-1, "right"),
                            new KeyValuePair<int, string>(-1, "down")
                        };

                        for (int i = a; i <= b; i++)
                        {
                            int[] lurd = { 0, 0, 0, 0 };
                            while (IsItEmpty(cArr[i].a, cArr[i].b - 1 - lurd[0])) lurd[0]++;
                            while (IsItEmpty(cArr[i].a - 1 - lurd[1], cArr[i].b)) lurd[1]++;
                            while (IsItEmpty(cArr[i].a, cArr[i].b + 1 + lurd[2])) lurd[2]++;
                            while (IsItEmpty(cArr[i].a + 1 + lurd[3], cArr[i].b)) lurd[3]++;
                            for (int j = 0; j < plurd.Count; j++)
                                if (plurd[j].Key == -1 || lurd[j] < plurd[j].Key)
                                    plurd[j] = new KeyValuePair<int, string>(lurd[j], plurd[j].Value);
                        }
                        if (!Convert.ToBoolean(plurd[0].Key | plurd[1].Key | plurd[2].Key | plurd[3].Key))
                        {
                            CordRandom[s].RemoveAt(z);
                            if (CordRandom[s].Count <= 1)
                            {
                                Blocks.Remove(cArr);
                                path.blocks.Add(cArr);
                                break;
                            }
                            continue;
                        }

                        Blocks.Remove(cArr);
                        if (a >= 0) Blocks.Add(cArr.Take(a + 1).ToArray());
                        if (b < cArr.Length) Blocks.Add(cArr.Skip(b).ToArray());
                        for (int i = 0; i < plurd.Count; i++)
                            if (plurd[i].Key == 0)
                                plurd.RemoveAt(i--);

                        bool div = false;
                        for (int i = 0; i < plurd.Count; i++)
                            if ((ulong)plurd[i].Key > c)
                                div = true;
                        if (div == true)
                            for (int i = 0; i < plurd.Count; i++)
                                if ((ulong)plurd[i].Key < c) {
                                    plurd.RemoveAt(i);
                                    i--;
                                }
                        s = srand.Next(0, plurd.Count);
                        if ((ulong)plurd[s].Key > c)
                            plurd[s] = new KeyValuePair<int, string>((int)c, plurd[s].Value);

                        plurd[s] = new KeyValuePair<int, string>(srand.Next(1,plurd[s].Key), plurd[s].Value);
                        c -= (ulong)plurd[s].Key;

                        if (plurd[s].Value == "left")
                        {
                            cord[] temp;
                            temp = Connect(cArr[a], new cord(cArr[a].a, cArr[a].b - plurd[s].Key));
                            DrowMap(temp);
                            if (temp.Length > 2)
                                Blocks.Add(temp.Skip(1).Take(temp.Length - 2).ToArray());
                            temp = Connect(cArr[b], new cord(cArr[b].a, cArr[b].b - plurd[s].Key));
                            DrowMap(temp);
                            if (temp.Length > 2)
                                Blocks.Add(temp.Skip(1).Take(temp.Length - 2).ToArray());
                            temp = Connect(new cord(cArr[a].a, cArr[a].b - plurd[s].Key), new cord(cArr[b].a, cArr[b].b - plurd[s].Key));
                            DrowMap(temp);
                            Blocks.Add(temp);
                            sExit = false;
                        }
                        if (plurd[s].Value == "up")
                        {
                            cord[] temp;
                            temp = Connect(cArr[a], new cord(cArr[a].a - plurd[s].Key, cArr[a].b));
                            DrowMap(temp);
                            if (temp.Length > 2) Blocks.Add(temp.Skip(1).Take(temp.Length - 2).ToArray());
                            temp = Connect(cArr[b], new cord(cArr[b].a - plurd[s].Key, cArr[b].b));
                            DrowMap(temp);
                            if (temp.Length > 2) Blocks.Add(temp.Skip(1).Take(temp.Length - 2).ToArray());
                            temp = Connect(new cord(cArr[a].a - plurd[s].Key, cArr[a].b), new cord(cArr[b].a - plurd[s].Key, cArr[b].b));
                            DrowMap(temp);
                            Blocks.Add(temp);
                            sExit = false;
                        }
                        if (plurd[s].Value == "right")
                        {
                            cord[] temp;
                            temp = Connect(cArr[a], new cord(cArr[a].a, cArr[a].b + plurd[s].Key));
                            DrowMap(temp);
                            if (temp.Length > 2) Blocks.Add(temp.Skip(1).Take(temp.Length - 2).ToArray());
                            temp = Connect(cArr[b], new cord(cArr[b].a, cArr[b].b + plurd[s].Key));
                            DrowMap(temp);
                            if (temp.Length > 2) Blocks.Add(temp.Skip(1).Take(temp.Length - 2).ToArray());
                            temp = Connect(new cord(cArr[a].a, cArr[a].b + plurd[s].Key), new cord(cArr[b].a, cArr[b].b + plurd[s].Key));
                            DrowMap(temp);
                            Blocks.Add(temp);
                            sExit = false;
                        }
                        if (plurd[s].Value == "down")
                        {
                            cord[] temp;
                            temp = Connect(cArr[a], new cord(cArr[a].a + plurd[s].Key, cArr[a].b));
                            DrowMap(temp);
                            if (temp.Length > 2) Blocks.Add(temp.Skip(1).Take(temp.Length - 2).ToArray());
                            temp = Connect(cArr[b], new cord(cArr[b].a + plurd[s].Key, cArr[b].b));
                            DrowMap(temp);
                            if (temp.Length > 2) Blocks.Add(temp.Skip(1).Take(temp.Length - 2).ToArray());
                            temp = Connect(new cord(cArr[a].a + plurd[s].Key, cArr[a].b), new cord(cArr[b].a + plurd[s].Key, cArr[b].b));
                            DrowMap(temp);
                            Blocks.Add(temp);
                            sExit = false;
                        }
                        Unconnect(cArr[a], cArr[b]);
                        if (Blocks.Count > 4)
                        {
                            List<cord[]> foo = new List<cord[]>();
                            if (a == 0 &&!(Blocks[Blocks.Count - 5].Last().a != Blocks[Blocks.Count - 3].Last().a && Blocks[Blocks.Count - 5].Last().b != Blocks[Blocks.Count - 3].Last().b))
                            {
                                foo.Add(Blocks[Blocks.Count - 5]);
                                foo.Add(Blocks[Blocks.Count - 3]);
                            }
                            if (b == cArr.Length && !(Blocks[Blocks.Count - 4].Last().a != Blocks[Blocks.Count - 2].Last().a && Blocks[Blocks.Count - 4].Last().b != Blocks[Blocks.Count - 2].Last().b))
                            {
                                foo.Add(Blocks[Blocks.Count - 4]);
                                foo.Add(Blocks[Blocks.Count - 2]);
                            }
                            if (foo.Count != 0)
                                foreach (cord[] element in foo)
                                    Blocks.Remove(element);
                            while (foo.Count != 0)
                            {
                                Blocks.Add(foo[0].Concat(foo[1]).ToArray());
                                foo.RemoveAt(0);
                                foo.RemoveAt(0);
                            }
                        }

                    }
                    if (Blocks.Count == 0 || c == 0)
                        fExit = false;
                }
                path.InsertBlockList(Blocks);
            }
            public static cord[] Connect(cord a, cord b)
            {
                List<cord> temp = new List<cord>();
                if (a.a != b.a && a.b != b.b)
                    return null;
                if (a.a > b.a || a.b > b.b)
                {
                    cord c = new cord(b);
                    b = a;
                    a = c;
                }
                if (a.b < b.b)
                    for (int i = a.b; i <= b.b; i++)
                        temp.Add(new cord(a.a, i));
                if (a.a < b.a)
                    for (int i = a.a; i <= b.a; i++)
                        temp.Add(new cord(i, a.b));
                return temp.ToArray();
            }
            public static void Unconnect(cord a, cord b)
            {
                if (a.a != b.a && a.b != b.b)
                    return;
                if (a.a > b.a || a.b > b.b)
                {
                    cord c = new cord(b);
                    b = a;
                    a = c;
                }
                if (a.b < b.b)
                {
                    map[a.a, a.b].UnRight();
                    map[b.a, b.b].UnLeft();
                    for (int i = a.b + 1; i < b.b; i++)
                        UnTube(a.a, i);
                    return;
                }
                if (a.a < b.a)
                {
                    map[a.a, a.b].UnDown();
                    map[b.a, b.b].UnUp();
                    for (int i = a.a + 1; i < b.a; i++)
                        UnTube(i, a.b);
                    return;
                }
            }
        }
        static void Main(string[] args)
        {
            while (true) Map.SetMap(10, 10);

        }
    }
}