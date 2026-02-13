using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ПР_SqRt
{
    internal class Program
    {
        class Square
        {
            private double sideLength;
            public Square() : this(0) { }
            public Square(double sideLength)
            {
                this.sideLength = sideLength < 0 ? 0 : sideLength;
            }
            public double CalculateArea()
            {
                return sideLength * sideLength;
            }
            public double CalculatePerimeter()
            {
                return 4 * sideLength;
            }

            public static Square operator ++(Square s)
            {
                s.sideLength++;
                return s;
            }
            public static Square operator --(Square s)
            {
                s.sideLength--;
                return s;
            }
            public static Square operator +(Square s1, Square s2)
            {
                return new Square(s1.sideLength + s2.sideLength);
            }
            public static Square operator -(Square s1, Square s2)
            {
                return new Square(s1.sideLength - s2.sideLength);
            }
            public static Square operator *(Square s1, Square s2)
            {
                return new Square(s1.sideLength * s2.sideLength);
            }
            public static Square operator /(Square s1, Square s2)
            {
                return new Square(s1.sideLength / s2.sideLength);
            }
            public static bool operator >(Square s1, Square s2)
            {
                return s1.sideLength > s2.sideLength;
            }
            public static bool operator <(Square s1, Square s2)
            {
                return s1.sideLength < s2.sideLength;
            }
            public static bool operator <=(Square s1, Square s2)
            {
                return s1.sideLength <= s2.sideLength;
            }
            public static bool operator >=(Square s1, Square s2)
            {
                return s1.sideLength >= s2.sideLength;
            }
            public static bool operator ==(Square s1, Square s2)
            {
                return s1.sideLength == s2.sideLength;
            }
            public static bool operator !=(Square s1, Square s2)
            {
                return s1.sideLength != s2.sideLength;
            }
            public static bool operator true(Square s)
            {
                return s.sideLength != 0;
            }
            public static bool operator false(Square s)
            {
                return s.sideLength == 0;
            }
            public override bool Equals(object obj)
            {
                if (obj is Square)
                {
                    Square s = (Square)obj;
                    return this.sideLength == s.sideLength;
                }
                return false;
            }
            public override string ToString()
            {
                return $"Square with side length: {sideLength}";
            }
            public override int GetHashCode()
            {
                return sideLength.GetHashCode();
            }
            public static implicit operator int(Square s)
            {
                return (int)s.sideLength;
            }
            public static implicit operator Rectangle(Square s)
            {
                return new Rectangle(s.sideLength, s.sideLength);
            }
        }

        class Rectangle{
            private double length;
            private double width;
            public Rectangle() : this(0,0) { }
            public Rectangle(double length, double width)
            {
                this.length = length < 0 ? 0 : length;
                this.width = width < 0 ? 0 : width;
            }

            public double CalculateArea()
            {
                return length * width;
            }
            public double CalculatePerimeter()
            {
                return 2 * (length + width);
            }
            public override string ToString()
            {
                return $"Rectangle with length: {length} and width: {width}";
            }
            public override bool Equals(object obj)
            {
                if (obj is Rectangle)
                {
                    Rectangle r = (Rectangle)obj;
                    return this.length == r.length && this.width == r.width;
                }
                return false;
            }
            public override int GetHashCode()
            {
                return length.GetHashCode() ^ width.GetHashCode();
            }
            public static Rectangle operator ++ (Rectangle r)
            {
                r.length++;
                r.width++;
                return r;
            }
            public static Rectangle operator --(Rectangle r)
            {
                r.length--;
                r.width--;
                return r;
            }
            public static Rectangle operator +(Rectangle r1, Rectangle r2)
            {
                return new Rectangle(r1.length + r2.length, r1.width + r2.width);
            }
            public static Rectangle operator -(Rectangle r1, Rectangle r2)
            {
                return new Rectangle(r1.length - r2.length, r1.width - r2.width);
            }
            public static Rectangle operator *(Rectangle r1, Rectangle r2)
            {
                return new Rectangle(r1.length * r2.length, r1.width * r2.width);
            }
            public static Rectangle operator /(Rectangle r1, Rectangle r2)
            {
                return new Rectangle(r1.length / r2.length, r1.width / r2.width);
            }
            public static bool operator >(Rectangle r1, Rectangle r2)
            {
                return r1.length > r2.length && r1.width > r2.width;
            }
            public static bool operator <(Rectangle r1, Rectangle r2)
            {
                return r1.length < r2.length && r1.width < r2.width;
            }
            public static bool operator >=(Rectangle r1, Rectangle r2)
            {
                return r1.length > r2.length && r1.width > r2.width;
            }
            public static bool operator <=(Rectangle r1, Rectangle r2)
            {
                return r1.length < r2.length && r1.width < r2.width;
            }
            public static bool operator ==(Rectangle r1, Rectangle r2)
            {
                return r1.length == r2.length && r1.width == r2.width;
            }
            public static bool operator !=(Rectangle r1, Rectangle r2)
            {
                return r1.length != r2.length && r1.width != r2.width;
            }
            public static bool operator true(Rectangle r)
            {
                return r.length != 0 && r.width != 0;
            }
            public static bool operator false(Rectangle r)
            {
                return r.length == 0 && r.width == 0;
            }
            public static explicit operator int(Rectangle r)
            {
                return (int)(r.length + r.width) / 2;
            }
            public static explicit operator Square(Rectangle r)
            {
                return new Square((r.length + r.width) / 2);
            }


        }
        static void Main(string[] args)
        {
            Square s1 = new Square();
            Square s2 = new Square(5);

            Console.WriteLine($"s1: {s1}");
            Console.WriteLine($"s2: {s2}");

            Console.WriteLine("\n++ and --");
            s2++;
            Console.WriteLine($"s2++ {s2}");
            s2--;
            Console.WriteLine($"s2-- {s2}");

            Console.WriteLine("\n+ - * /");
            Square s3 = new Square(3);
            Square s4 = new Square(2);

            Console.WriteLine($"{s3} + {s4} = {s3 + s4}");
            Console.WriteLine($"{s3} - {s4} = {s3 - s4}");
            Console.WriteLine($"{s3} * {s4} = {s3 * s4}");
            Console.WriteLine($"{s3} / {s4} = {s3 / s4}");

            Console.WriteLine("\n< > <= >= == !=");
            Console.WriteLine($"{s3} > {s4}: {s3 > s4}");
            Console.WriteLine($"{s3} < {s4}: {s3 < s4}");
            Console.WriteLine($"{s3} >= {s4}: {s3 >= s4}");
            Console.WriteLine($"{s3} <= {s4}: {s3 <= s4}");
            Console.WriteLine($"{s3} == {s4}: {s3 == s4}");
            Console.WriteLine($"{s3} != {s4}: {s3 != s4}");

            Console.WriteLine("\n true / false");
            if (s3)
                Console.WriteLine($"{s3} TRUE");
            else
                Console.WriteLine($"{s3} FALSE");

            Console.WriteLine("\nimplicit Square -> Rectangle");
            Rectangle r_from_s = s3;
            Console.WriteLine($"Rectangle from {s3}: {r_from_s}");

            Console.WriteLine("\nimplicit Square -> int");
            int sqInt = s3;
            Console.WriteLine($"int from {s3} = {sqInt}");



            Rectangle r1 = new Rectangle();
            Rectangle r2 = new Rectangle(4, 7);

            Console.WriteLine($"r1: {r1}");
            Console.WriteLine($"r2: {r2}");

            Console.WriteLine("\n++ and --");
            r2++;
            Console.WriteLine($"r2++  {r2}");
            r2--;
            Console.WriteLine($"r2--  {r2}");

            Console.WriteLine("\n+ - * /");
            Rectangle r3 = new Rectangle(3, 5);
            Rectangle r4 = new Rectangle(2, 1);

            Console.WriteLine($"{r3} + {r4} = {r3 + r4}");
            Console.WriteLine($"{r3} - {r4} = {r3 - r4}");
            Console.WriteLine($"{r3} * {r4} = {r3 * r4}");
            Console.WriteLine($"{r3} / {r4} = {r3 / r4}");

            Console.WriteLine("\n< > <= >= == !=");
            Console.WriteLine($"{r3} > {r4}: {r3 > r4}");
            Console.WriteLine($"{r3} < {r4}: {r3 < r4}");
            Console.WriteLine($"{r3} >= {r4}: {r3 >= r4}");
            Console.WriteLine($"{r3} <= {r4}: {r3 <= r4}");
            Console.WriteLine($"{r3} == {r4}: {r3 == r4}");
            Console.WriteLine($"{r3} != {r4}: {r3 != r4}");

            Console.WriteLine("\ntrue / false");
            if (r3)
                Console.WriteLine($"{r3} TRUE");
            else
                Console.WriteLine($"{r3} FALSE");

            Console.WriteLine("\n-- explicit Rectangle -> Square");
            Square sq_from_r = (Square)r3;
            Console.WriteLine($"Square from {r3}: {sq_from_r}");

            Console.WriteLine("\n-- explicit Rectangle -> int");
            int rectInt = (int)r3;
            Console.WriteLine($"int from {r3} = {rectInt}");

        }
    }
    
}
