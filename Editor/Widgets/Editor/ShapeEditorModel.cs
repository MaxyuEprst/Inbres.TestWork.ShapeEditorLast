using System;
using System.Collections.ObjectModel;
using Avalonia;
using Editor.Entities.Shapes;

namespace Editor.Widgets.Editor
{
    internal class ShapeEditorModel
    {
        public ObservableCollection<EditorShape> Shapes { get; } = new();

        public void CreateShape(ShapeType shapeType, Point point, double width, double height)
        {
            EditorShape newShape = shapeType switch
            {
                ShapeType.Oval => new OvalShape
                {
                    X = point.X,
                    Y = point.Y,
                    Width = width,
                    Height = height
                },
                ShapeType.BezierCurve => new BezCurShape
                {
                    X = point.X,
                    Y = point.Y,
                    Width = width,
                    Height = height
                },
                _ => throw new NotImplementedException()
            };

            Shapes.Add(newShape);
        }


        public void RemoveShape(EditorShape shape)
        {
            Shapes.Remove(shape);
        }

        public void ClearShapes()
        {
            Shapes.Clear();
        }
    }
}
