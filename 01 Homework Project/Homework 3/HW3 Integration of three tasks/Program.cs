using System;
using System.Collections.Generic;

namespace HW3_Integration_of_three_tasks
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a list of shapes using the factory method
            List<ICalculable> shapes = new List<ICalculable>
            {
                ShapeFactory.CreateShape("Rectangle", 3, 4),
                ShapeFactory.CreateShape("Square", 5),
                ShapeFactory.CreateShape("Triangle", 4, 2)
            };

            // Calculate and display the area of each shape
            foreach (var shape in shapes)
            {
                Console.WriteLine($"Area of {shape.GetType().Name} is {shape.AreaCalculation()}");
            }
        }
    }

    // Interface for area calculation
    public interface ICalculable
    {
        double AreaCalculation();
    }

    // Rectangle class implementing ICalculable
    class Rectangle : ICalculable
    {
        protected int Width;
        protected int Height;

        public Rectangle(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        // Virtual method to calculate area (allows overriding in derived classes)
        public virtual double AreaCalculation()
        {
            return this.Height * this.Width;
        }
    }

    // Square class, inheriting from Rectangle
    class Square : Rectangle
    {
        public Square(int length) : base(length, length)
        {
        }

        // Inherits AreaCalculation() from Rectangle, no need to redefine it
    }

    // Triangle class implementing ICalculable
    class Triangle : ICalculable
    {
        private int Bottom;
        private int Height;

        public Triangle(int bottom, int height)
        {
            this.Bottom = bottom;
            this.Height = height;
        }

        // Method to calculate the area of the triangle
        public double AreaCalculation()
        {
            return this.Bottom * this.Height / 2.0; // Ensure floating-point division
        }
    }

    // Simple Factory class to create shape objects
    class ShapeFactory
    {
        // Factory method to create different shapes based on the provided type
        public static ICalculable CreateShape(string shapeType, params int[] parameters)
        {
            switch (shapeType)
            {
                case "Rectangle":
                    return new Rectangle(parameters[0], parameters[1]);
                case "Square":
                    return new Square(parameters[0]);
                case "Triangle":
                    return new Triangle(parameters[0], parameters[1]);
                default:
                    throw new ArgumentException("Unknown shape type.");
            }
        }
    }
}
