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

        public BezCurShape()
        {
            _points.CollectionChanged += (s, e) =>
            {
                OnPropertyChanged(nameof(Points));
            };
        }
    }
}
