using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                ShapeType.Oval => new OvalShape {Width=20, Height=20, X = point.X + 50, Y = point.Y - 30 },
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
