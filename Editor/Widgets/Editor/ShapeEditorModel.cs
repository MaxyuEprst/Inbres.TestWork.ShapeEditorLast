using System;
using System.Collections.ObjectModel;
using Avalonia;
using Editor.Entities.Shapes;

namespace Editor.Widgets.Editor
{
    internal class ShapeEditorModel
    {
        public ObservableCollection<EditorShape> Shapes { get; } = new();

        public void CreateShape(ShapeType shapeType, Point point)
        {
            EditorShape newShape = shapeType switch
            {
                ShapeType.Oval => new OvalShape {Width=60, Height=60, X = point.X, Y = point.Y },
                ShapeType.BezierCurve => new BezCurShape { X = point.X, Y = point.Y },
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
