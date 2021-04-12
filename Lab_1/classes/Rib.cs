using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1Classes
{
    public class Rib : IComparable
    {
        private Vertex start;
        private Vertex end;
        private int value;

        public Rib() { }

        public Rib((Vertex, Vertex) path, int value)
        {
            Start = path.Item1;
            End = path.Item2;
            Value = value;
        }

        public Vertex Start { get; set; }
        public Vertex End { get; set; }
        public int Value { get; set; }



        public override string ToString()
        {
            return this.Start.ToString() + '-' + this.End.ToString();
        }

        public int CompareTo(object obj)
        {
            Rib tmp = obj as Rib;
            if (tmp != null)
                return this.ToString().CompareTo(tmp.ToString());
            else
                throw new ExceptionComparation("Cannot be compared");
        }


        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            Rib rib = obj as Rib;

            if (rib as Rib == null)
                return false;

            return rib.Start.Name == this.Start.Name && rib.End.Name == this.End.Name && rib.Value == this.Value;
        }

    }
}
        
        

