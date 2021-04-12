using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1Classes
{
    public class Vertex  : IComparable
    {
        private int name;
        private int mark;
        private Vertex dad;
        private bool isChecked;

        public Vertex() { }

        public Vertex(int name)
        {
            Name = name;
            Mark = 1000000;
            IsChecked = false;

        }

        public Vertex(int name, int mark)
        {
            Name = name;
            Mark = mark;
            IsChecked = false;
        }


        public int Name { get; set; }
        public int Mark { get; set; }
        public bool IsChecked { get; set; }
        public Vertex Dad { get; set; }

        public override string ToString()
        {
            return this.Name.ToString();
        }



        public static bool operator >(Vertex x, Vertex y)
        {
            return x.Name > y.Name;
        }
        public static bool operator <(Vertex x, Vertex y)
        {
            return x.Name < y.Name;
        }

        public static bool operator >=(Vertex x, Vertex y)
        {
            return x.Name >= y.Name;
        }
        public static bool operator <=(Vertex x, Vertex y)
        {
            return x.Name <= y.Name;
        }

      
        public int CompareTo(object obj)
        {
            Vertex tmp = obj as Vertex;
           
            if (tmp != null)
                return this.Name.CompareTo(tmp.Name);
            else
                throw new ExceptionComparation("Cannot be compared");
        }


        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            Vertex vertex = obj as Vertex;

            if (vertex as Vertex == null)
                return false;

            return vertex.Name == this.Name && vertex.Mark == this.Mark && vertex.IsChecked == this.IsChecked && vertex.Dad == this.Dad;
        }
    }



    class VertexComparer : IComparer<Vertex>
    {
        public int Compare(Vertex x, Vertex y)
        {
            if (x.Name < y.Name)
                return -1;
            else
            {
                if (x.Name > y.Name)
                    return 1;
                else
                    return 0;
            }
        }

    }



}
