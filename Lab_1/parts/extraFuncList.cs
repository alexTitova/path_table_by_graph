using System;
using System.Collections.Generic;
using System.Text;
using Lab1Classes;

namespace Lab1Parts
{
    public static class ListExtension
    {
        public static Vertex FindMinMark(this List<Vertex> data)
        {
            Vertex result = new Vertex(0, int.MaxValue);

            foreach(Vertex vertex in data)
            {
                if (result.Mark > vertex.Mark && !vertex.IsChecked)
                    result = vertex;
            }

            return result;
        }


        public static int FindIndexForNotSorted(this List<Vertex> data, Vertex unit)
        {
            int result = -1;
            foreach (Vertex vertex in data)
            {
                result++;

                if (vertex.Name == unit.Name)
                    break;
            }

            return result;
        }
    }

}
