using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Розробити абстрактний клас «Геометрична Фігура» з методами:
//GetArea – обчислення площі
//GetPerimeter – обчислення периметра
//Описати похідні класи:
//Трикутник
//Квадрат
//Ромб
//Прямокутник
//Паралелограм
//Трапеція
//Коло
//Еліпс.
//Класи повинні містити характеристики певної фігури та конструктори, які їх встановлять.
//Також реалізувати клас «Складена Фігура», який буде складатися з будь-якої кількості «Геометричних фігур» (міститиме масив фігур). Класі повинен містити конструктор, який використовуючи params прийматиме перелік фігур з який він буде складатися.

namespace ПР_InheritShapes
{
    abstract class GeoShape
    {
        public abstract double GetArea();
        public abstract double GetPerimeter();
    }
    class Triangle : GeoShape
    {
        private double sideA;
        private double sideB;
        private double sideC;
        private double height;
        public Triangle(double sideA, double sideB, double sideC, double height)
        {
            this.sideA = sideA;
            this.sideB = sideB;
            this.sideC = sideC;
            this.height = height;
        }
        public override double GetArea()
        {
            return 0.5 * sideA * height;
        }
        public override double GetPerimeter()
        {
            return sideA + sideB + sideC;
        }
    }
    class Square : GeoShape
    {
        private double side;
        public Square(double side)
        {
            this.side = side;
        }
        public override double GetArea()
        {
            return side * side;
        }
        public override double GetPerimeter()
        {
            return 4 * side;
        }
    }
    class Romb : GeoShape
    {
        private double side;
        private double height;
        public Romb(double side, double height)
        {
            this.side = side;
            this.height = height;
        }
        public override double GetArea()
        {
            return side * height;
        }
        public override double GetPerimeter()
        {
            return 4 * side;
        }
    }
    class Rectangle : GeoShape
    {
        private double length;
        private double width;
        public Rectangle(double length, double width)
        {
            this.length = length;
            this.width = width;
        }
        public override double GetArea()
        {
            return length * width;
        }
        public override double GetPerimeter()
        {
            return 2 * (length + width);
        }
    }
    class Parallelogram : GeoShape
    {
        private double baseLength;
        private double sideLength;
        private double height;
        public Parallelogram(double baseLength, double sideLength, double height)
        {
            this.baseLength = baseLength;
            this.sideLength = sideLength;
            this.height = height;
        }
        public override double GetArea()
        {
            return baseLength * height;
        }
        public override double GetPerimeter()
        {
            return 2 * (baseLength + sideLength);
        }
    }
    class Trapezoid : GeoShape
    {
        private double baseA;
        private double baseB;
        private double sideC;
        private double sideD;
        private double height;
        public Trapezoid(double baseA, double baseB, double sideC, double sideD, double height)
        {
            this.baseA = baseA;
            this.baseB = baseB;
            this.sideC = sideC;
            this.sideD = sideD;
            this.height = height;
        }
        public override double GetArea()
        {
            return 0.5 * (baseA + baseB) * height;
        }
        public override double GetPerimeter()
        {
            return baseA + baseB + sideC + sideD;
        }
    }
    class Circle : GeoShape
    {
        private double radius;
        public Circle(double radius)
        {
            this.radius = radius;
        }
        public override double GetArea()
        {
            return Math.PI * radius * radius;
        }
        public override double GetPerimeter()
        {
            return 2 * Math.PI * radius;
        }
    }
    class Oval: GeoShape
    {
        private double semiMajorAxis;
        private double semiMinorAxis;
        public Oval(double semiMajorAxis, double semiMinorAxis)
        {
            this.semiMajorAxis = semiMajorAxis;
            this.semiMinorAxis = semiMinorAxis;
        }
        public override double GetArea()
        {
            return Math.PI * semiMajorAxis * semiMinorAxis;
        }
        public override double GetPerimeter()
        {
            return Math.PI * (3 * (semiMajorAxis + semiMinorAxis) - Math.Sqrt((3 * semiMajorAxis + semiMinorAxis) * (semiMajorAxis + 3 * semiMinorAxis)));
        }
    }
    class CompositeShape : GeoShape
    {
        private GeoShape[] shapes;
        public CompositeShape(params GeoShape[] shapes)
        {
            this.shapes = shapes;
        }
        public override double GetArea()
        {
            double totalArea = 0;
            foreach (var shape in shapes)
            {
                totalArea += shape.GetArea();
            }
            return totalArea;
        }
        public override double GetPerimeter()
        {
            double totalPerimeter = 0;
            foreach (var shape in shapes)
            {
                totalPerimeter += shape.GetPerimeter();
            }
            return totalPerimeter;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            GeoShape triangle = new Triangle(3, 4, 5, 4);
            GeoShape square = new Square(4);
            GeoShape rectangle = new Rectangle(4, 6);
            GeoShape circle = new Circle(5);
            CompositeShape compositeShape = new CompositeShape(triangle, square, rectangle, circle);
            Console.WriteLine("Площа складеної фігури: " + compositeShape.GetArea());
            Console.WriteLine("Периметр складеної фігури: " + compositeShape.GetPerimeter());
        }
    }
}
