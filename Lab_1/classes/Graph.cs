using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab1Classes
{
    public class Graph
    {
        SortedSet<Vertex> vertexes;
        List<Rib> ribs;



        public Graph() {  }
        public Graph(SortedSet<Vertex> vertexes, List<Rib> ribs) 
        {
            Vertexes = vertexes;
            Ribs = ribs;
        }

        public SortedSet<Vertex> Vertexes { get; set; }
        public List<Rib> Ribs { get; set; }


        //возвращает кол-во вершин
        public int CountVertexes()
        {
            return this.Vertexes.Count;
        }


        //возвращает кол-во ребер
        public int CountRibs()
        {
            return this.Ribs.Count;
        }
        

        //добавляет вершину
        public void AddVertex(Vertex vertex)
        {
            if (!this.Vertexes.Contains(vertex))
                this.Vertexes.Add(vertex);
            else
                throw new ExceptionAlreadyExist("Vertex " + vertex.ToString() + " has already exist");
        }


        //добавляет ребро если есть соответствующие вершины
        public void AddRib(Rib rib)
        {
            if (this.Vertexes.Contains(rib.Start) && this.Vertexes.Contains(rib.End))
            {
                if (this.Ribs.BinarySearch(rib)<0)
                    this.Ribs.Add(rib);
                else
                    throw new ExceptionAlreadyExist("Rib " + rib.ToString() + " has already exist");
            }
            else
                throw new ExceptionDoesNotExist("One of vertexes does not exist");
        }


        //получает ребро с заданным кначалом и концом
        public Rib GetRib((Vertex, Vertex) rib)
        {
            Rib unit = new Rib(rib, 0);

            if (this.Ribs.BinarySearch(unit)<0)
                throw new ExceptionDoesNotExist("Rib does not exist");

            return this.Ribs[this.Ribs.BinarySearch(unit)];
        }


        //возвращает список исходящих ребер из вершины
        public List<Rib> GetOutGoingRibs(Vertex vertex)
        {
            List<Rib> result = new List<Rib>();

            foreach (Rib rib in this.Ribs)
            {
                if (rib.Start.Name == vertex.Name)
                    result.Add(rib);
            }

            return result;
        }


        //возвращает матрицу смежности, если есть хотя бы одно ребро 
        public MatrixAdjacency GetMatrixAdjacency()
        {
           
            MatrixAdjacency result =   new MatrixAdjacency(  new int[this.Vertexes.Count(), this.Vertexes.Count()] );

            if (this.Ribs != null)
            {
                int i = 0;
                int j = 0;
                

                foreach (Rib rib in this.Ribs)
                {
                    i = this.Vertexes.ToList().BinarySearch(rib.Start);
                    j = this.Vertexes.ToList().BinarySearch(rib.End);

                    result[i, j] = rib.Value;
                }
            }
            else
                throw new ExceptionDoesNotExist("Graph does not exist or does not have ribs");

            return result;
        }

    }
}
