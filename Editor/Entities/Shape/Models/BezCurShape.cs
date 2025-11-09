using Avalonia;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace Editor.Entities.Shape.Models
{
    public partial class BezCurShape : EditorShape
    {
        public override ShapeType Type => ShapeType.BezierCurve;

        [ObservableProperty]
        private ObservableCollection<Point> _points = new();

        [ObservableProperty]
        private Geometry? _bezierGeometry;
    }
}
