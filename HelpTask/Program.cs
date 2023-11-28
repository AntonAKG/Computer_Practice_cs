using System.Text;

public class Indicative 
{
    public List<int> Vertices { get; set; }
    public List<Tuple<int, int>> Edges { get; set; }

    public Indicative(List<int> vertices, List<Tuple<int, int>> edges) 
    {
        Vertices = vertices;
        Edges = edges;
    }

    public Dictionary<int, int> DegreesOfVertices() 
    {
        var numberDict = Enumerable.Range(1, Vertices.Count).ToDictionary(key => key, value => 0);
        foreach (var el in Edges) 
        {
            if (numberDict.ContainsKey(el.Item1)) numberDict[el.Item1]++;
            if (numberDict.ContainsKey(el.Item2)) numberDict[el.Item2]++;
        }
        return numberDict;
    }

    public Dictionary<int, Tuple<int, int>> SemiDegree() 
    {
        var numberDict = Enumerable.Range(1, Vertices.Count).ToDictionary(key => key, value => new Tuple<int, int>(0, 0));
        foreach (var el in Edges) 
        {
            if (numberDict.ContainsKey(el.Item1)) numberDict[el.Item1] = new Tuple<int, int>(numberDict[el.Item1].Item1 + 1, numberDict[el.Item1].Item2);
            if (numberDict.ContainsKey(el.Item2)) numberDict[el.Item2] = new Tuple<int, int>(numberDict[el.Item2].Item1, numberDict[el.Item2].Item2 + 1);
        }
        return numberDict;
    }

    public void PrintAdjacencyMatrix() 
    {
        int n = Vertices.Max();
        int[,] matrix = new int[n, n];
        
        foreach (var edge in Edges) 
        {
            matrix[edge.Item1 - 1, edge.Item2 - 1] = 1;
            matrix[edge.Item2 - 1, edge.Item1 - 1] = 1;
        }
        
        for (int i = 0; i < n; i++) 
        {
            for (int j = 0; j < n; j++) 
            {
                Console.Write(matrix[i, j] + " ");
            }
            Console.WriteLine();
        }
    }

    public void IncidentMatrices() 
    {
        int[,] matrix = new int[Vertices.Count, Edges.Count];
        for (int i = 0; i < Edges.Count; i++) 
        {
            matrix[Vertices.IndexOf(Edges[i].Item1), i] = -1;
            matrix[Vertices.IndexOf(Edges[i].Item2), i] = 1;
        }
        
        for (int i = 0; i < matrix.GetLength(0); i++) 
        {
            for (int j = 0; j < matrix.GetLength(1); j++) 
            {
                Console.Write(matrix[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
}

public class NotIndicative : Indicative 
{
    public NotIndicative(List<int> vertices, List<Tuple<int, int>> edges) : base(vertices, edges) 
    {}
}

public class Program
{
    public static void Main(string[] args)
    {
        List<int> vertices = new List<int>(){1, 2, 3, 4, 5, 6, 7};
        List<Tuple<int, int>> edges = new List<Tuple<int, int>>(){
            new Tuple<int, int>(1, 2),
            new Tuple<int, int>(3, 2),
            new Tuple<int, int>(3, 1),
            new Tuple<int, int>(4, 3),
            new Tuple<int, int>(3, 5),
            new Tuple<int, int>(3, 6),
            new Tuple<int, int>(6, 7),
            new Tuple<int, int>(7, 3)
        };
        
        List<int> not_vertices = new List<int>(){1, 2, 3, 4, 5, 6, 7, 8};
        List<Tuple<int, int>> not_edges = new List<Tuple<int, int>>(){
            new Tuple<int, int>(1, 2),
            new Tuple<int, int>(2, 3),
            new Tuple<int, int>(3, 4),
            new Tuple<int, int>(1, 4),
            new Tuple<int, int>(4, 5),
            new Tuple<int, int>(6, 5),
            new Tuple<int, int>(6, 4),
            new Tuple<int, int>(7, 6),
            new Tuple<int, int>(7, 3),
            new Tuple<int, int>(8, 3),
            new Tuple<int, int>(8, 7),
            
        };
        
        Console.InputEncoding = Encoding.Unicode;
        Console.OutputEncoding = Encoding.Unicode;
        
        // Indicative
        Console.WriteLine("Indicative");
        
        Indicative IndieObj = new Indicative(vertices, edges);
        
        
        Dictionary<int, int> degrees = IndieObj.DegreesOfVertices();
        foreach(var degree in degrees)
        {
            Console.WriteLine($"Степінь {degree.Key} вершини: {degree.Value}");
        }

        Dictionary<int, Tuple<int, int>> semiDegrees = IndieObj.SemiDegree();
        foreach(var semiDegree in semiDegrees)
        {
            Console.WriteLine($"Напівстерінь {semiDegree.Key} вершини: входу {semiDegree.Value.Item1}, виходу {semiDegree.Value.Item2}");
        }
        
        Console.WriteLine("матриця суміжності: ");
        IndieObj.PrintAdjacencyMatrix();
        Console.WriteLine("матриця інцидентності: ");
        IndieObj.IncidentMatrices();
        
        // Not Indicative

        Console.WriteLine("Not Indicative");
        NotIndicative NotObj = new NotIndicative(vertices, edges);
        
        Dictionary<int, int> degreesNot = NotObj.DegreesOfVertices();
        
        foreach(var degree in degreesNot)
        {
            Console.WriteLine($"Степінь {degree.Key} вершини: {degree.Value}");
        }
        
        Console.WriteLine("матриця суміжності: ");
        NotObj.PrintAdjacencyMatrix();
        Console.WriteLine("матриця інцидентності: ");
        NotObj.IncidentMatrices();
    }
}