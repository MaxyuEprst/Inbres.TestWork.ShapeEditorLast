using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia;
using Editor.Entities.Shape.Models;

namespace Editor.Shared
{
    internal class ShapeEditorModel
    {
        public ObservableCollection<EditorShape> Shapes { get; } = new();

        public EditorShape CreateShape(ShapeType shapeType, Point point, double width = 0, double height = 0)
        {
            EditorShape newShape = shapeType switch
            {
                ShapeType.Oval => new OvalShape {Width = width, Height = height, X = point.X, Y = point.Y },
                ShapeType.BezierCurve => new BezCurShape { X = point.X, Y = point.Y },
                _ => throw new NotImplementedException()
            };

            Shapes.Add(newShape);
            return newShape;
        }

        public void RemoveShape(EditorShape shape)
        {
            Shapes.Remove(shape);
        }

        public void ClearShapes()
        {
            Shapes.Clear();
        }

        public void LoadShapes(IEnumerable<EditorShape> shapes)
        {
            Shapes.Clear();
            foreach (var shape in shapes)
            {
                Shapes.Add(shape);
            }
        }
    }
}
