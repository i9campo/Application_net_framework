using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;

namespace WEBAPI.Auxiliar.Teste
{
    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }
    }

    public class PolygonCoordinates
    {
        public double[,] Coordinates { get; set; }

        public PolygonCoordinates(double[,] coordinates)
        {
            Coordinates = coordinates;
        }

        public double[] GetTopLeftCoordinate()
        {
            double minX = Coordinates[0, 0];
            double maxY = Coordinates[0, 1];

            for (int i = 1; i < Coordinates.GetLength(0); i++)
            {
                double x = Coordinates[i, 0];
                double y = Coordinates[i, 1];

                if (x < minX)
                {
                    minX = x;
                }

                if (y > maxY)
                {
                    maxY = y;
                }
            }

            return new double[] { minX, maxY };
        }
    }

    public class Polygon
    {
        public List<Point> Points { get; set; }

        public Polygon(List<Point> points)
        {
            Points = points;
        }

        public List<Polygon> Subdivide(int n)
        {
            // Calcula a área total do polígono
            double area = 0;
            int j = Points.Count - 1;
            for (int i = 0; i < Points.Count; i++)
            {
                area += Points[j].X * Points[i].Y - Points[j].Y * Points[i].X;
                j = i;
            }
            area /= 2;

            // Calcula a área de cada subpolígono
            double subarea = area / n;

            // Calcula o comprimento do lado de cada subpolígono
            double sublength = Math.Sqrt(subarea);

            List<Polygon> subpolygons = new List<Polygon>();

            // Adiciona pontos intermediários ao longo das bordas do polígono
            List<Point> newPoints = new List<Point>();
            j = Points.Count - 1;
            for (int i = 0; i < Points.Count; i++)
            {
                Point p1 = Points[j];
                Point p2 = Points[i];
                newPoints.Add(p1);

                double segmentLength = Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
                double tStep = sublength / segmentLength;
                double t = tStep;
                while (t < 1.0)
                {
                    double x = p1.X + t * (p2.X - p1.X);
                    double y = p1.Y + t * (p2.Y - p1.Y);
                    newPoints.Add(new Point(x, y));

                    t += tStep;
                }

                j = i;
            }
            newPoints.Add(Points[0]);
            Points = newPoints;

            // Divide o polígono em subpolígonos
            int m = (int)Math.Floor(Points.Count / (double)n);

            for (int i = 0; i < n; i++)
            {
                List<Point> subpoints = new List<Point>();
                for (int x = 0; x < m; x++)
                {
                    int index = i * m + x;
                    if (index < Points.Count)
                        subpoints.Add(Points[index]);
                }
                if (subpoints.Count > 0)
                {
                    Polygon subpolygon = new Polygon(subpoints);
                    if (IsConvex(subpolygon.Points) && IsSimple(subpolygon.Points))
                        subpolygons.Add(subpolygon);
                    else
                        subpolygons.AddRange(subpolygon.Triangulate());
                }
            }

            // Remove espaços entre subpolígonos
            for (int i = 0; i < subpolygons.Count - 1; i++)
            {
                Polygon p1 = subpolygons[i];
                Polygon p2 = subpolygons[i + 1];

                int attempts = 0;
                const int maxAttempts = 100;

                while (p1.Points[p1.Points.Count - 1] != p2.Points[0] && attempts < maxAttempts)
                {
                    double x = (p1.Points[p1.Points.Count - 1].X + p2.Points[0].X) / 2;
                    double y = (p1.Points[p1.Points.Count - 1].Y + p2.Points[0].Y) / 2;

                    p1.Points.Add(new Point(x, y));
                    p2.Points.Insert(0, new Point(x, y));

                    attempts++;
                }
            }

            // Remove intersecções entre subpolígonos
            for (int i = 0; i < subpolygons.Count; i++)
            {
                Polygon polygon = subpolygons[i];

                for (int x = 0; x < subpolygons.Count; x++)
                {
                    if (i == x)
                        continue;

                    Polygon other = subpolygons[x];

                    for (int k = 0; k < other.Points.Count; k++)
                    {
                        int next = (k + 1) % other.Points.Count;
                        Point intersection = FindIntersection(polygon, other.Points[k], other.Points[next]);
                        if (intersection != null)
                        {
                            polygon.Points.Add(intersection);
                        }
                    }
                }
            }

            string teste = "MULTILINESTRING(";
            foreach (Polygon polygon in subpolygons)
            {
                teste += "(";
                foreach (Auxiliar.Teste.Point point in polygon.Points)
                {
                    teste += point.X.ToString().Replace(",", ".") + " " + point.Y.ToString().Replace(",", ".") + ",";

                }
                teste = teste.Remove(teste.Length - 1) + "," + polygon.Points[0].X.ToString().Replace(",", ".") + " " + polygon.Points[0].Y.ToString().Replace(",", ".") + "),";
            }
            teste = teste.Remove(teste.Length - 1) + ")";


            return subpolygons;
        }

        private Point FindIntersection(Polygon polygon, Point a, Point b)
        {
            for (int i = 0; i < polygon.Points.Count; i++)
            {
                int next = (i + 1) % polygon.Points.Count;
                Point intersection = Intersection(a, b, polygon.Points[i], polygon.Points[next]);
                if (intersection != null)
                    return intersection;
            }

            return null;
        }

        private Point Intersection(Point a1, Point a2, Point b1, Point b2)
        {
            double ua = ((b2.X - b1.X) * (a1.Y - b1.Y) - (b2.Y - b1.Y) * (a1.X - b1.X)) / ((b2.Y - b1.Y) * (a2.X - a1.X) - (b2.X - b1.X) * (a2.Y - a1.Y));
            double ub = ((a2.X - a1.X) * (a1.Y - b1.Y) - (a2.Y - a1.Y) * (a1.X - b1.X)) / ((b2.Y - b1.Y) * (a2.X - a1.X) - (b2.X - b1.X) * (a2.Y - a1.Y));

            if (ua >= 0 && ua <= 1 && ub >= 0 && ub <= 1)
            {
                double x = a1.X + ua * (a2.X - a1.X);
                double y = a1.Y + ua * (a2.Y - a1.Y);
                return new Point(x, y);
            }

            return null;
        }

        private bool IsConvex(List<Point> points)
        {
            if (points.Count < 3)
                return false;

            bool sign = false;
            int n = points.Count;
            for (int i = 0; i < n; i++)
            {
                double dx1 = points[(i + 2) % n].X - points[(i + 1) % n].X;
                double dy1 = points[(i + 2) % n].Y - points[(i + 1) % n].Y;
                double dx2 = points[i].X - points[(i + 1) % n].X;
                double dy2 = points[i].Y - points[(i + 1) % n].Y;
                double cross = dx1 * dy2 - dy1 * dx2;

                if (i == 0)
                    sign = cross > 0;
                else if (sign != (cross > 0))
                    return false;
            }

            return true;
        }

        private bool IsSimple(List<Point> points)
        {
            for (int i = 0; i < points.Count; i++)
            {
                int next1 = (i + 1) % points.Count;
                for (int j = i + 1; j < points.Count; j++)
                {
                    int next2 = (j + 1) % points.Count;
                    if (next2 == i)
                        continue;

                    Point intersection = Intersection(points[i], points[next1], points[j], points[next2]);
                    if (intersection != null)
                        return false;
                }
            }

            return true;
        }

        private List<Polygon> Triangulate()
        {
            List<Polygon> triangles = new List<Polygon>();
            Triangulate(Points, triangles);
            return triangles;
        }

        private void Triangulate(List<Point> points, List<Polygon> triangles)
        {
            if (points.Count < 3)
                return;

            int n = points.Count;
            int[] V = new int[n];
            if (Area() > 0)
            {
                for (int v = 0; v < n; v++)
                    V[v] = v;
            }
            else
            {
                for (int v = 0; v < n; v++)
                    V[v] = (n - 1) - v;
            }

            int nv = n;
            int count = 2 * nv;
            for (int m = 0, v = nv - 1; nv > 2;)
            {
                if ((count--) <= 0)
                    return;

                int u = v;
                if (nv <= u)
                    u = 0;

                v = u + 1;
                if (nv <= v)
                    v = 0;

                int w = v + 1;
                if (nv <= w)
                    w = 0;

                if (Snip(points, u, v, w, nv, V))
                {
                    int a, b, c, s, t;
                    a = V[u];
                    b = V[v];
                    c = V[w];
                    triangles.Add(new Polygon(new List<Point>() { points[a], points[b], points[c] }));

                    for (s = v, t = v + 1; t < nv; s++, t++)
                        V[s] = V[t];
                    nv--;
                    count = 2 * nv;
                    m = 0;
                }
            }
        }

        private double Area()
        {
            double area = 0;
            int j = Points.Count - 1;
            for (int i = 0; i < Points.Count; i++)
            {
                area += Points[j].X * Points[i].Y - Points[j].Y * Points[i].X;
                j = i;
            }
            return area / 2;
        }

        private bool Snip(List<Point> points, int u, int v, int w, int n, int[] V)
        {
            Point A = points[V[u]];
            Point B = points[V[v]];
            Point C = points[V[w]];

            if (Math.Abs((B.X - A.X) * (C.Y - A.Y) - (B.Y - A.Y) * (C.X - A.X)) < double.Epsilon)
                return false;

            for (int p = 0; p < n; p++)
            {
                if (p == u || p == v || p == w)
                    continue;

                Point P = points[V[p]];

                if (InsideTriangle(A, B, C, P))
                    return false;
            }

            return true;
        }

        private bool InsideTriangle(Point A, Point B, Point C, Point P)
        {
            double ax = C.X - B.X;
            double ay = C.Y - B.Y;
            double bx = A.X - C.X;
            double by = A.Y - C.Y;
            double cx = B.X - A.X;
            double cy = B.Y - A.Y;
            double apx = P.X - A.X;
            double apy = P.Y - A.Y;
            double bpx = P.X - B.X;
            double bpy = P.Y - B.Y;
            double cpx = P.X - C.X;
            double cpy = P.Y - C.Y;

            double aCROSSbp = ax * bpy - ay * bpx;
            double cCROSSap = cx * apy - cy * apx;
            double bCROSScp = bx * cpy - by * cpx;

            return aCROSSbp >= 0 && bCROSScp >= 0 && cCROSSap >= 0;
        }
    }
}


