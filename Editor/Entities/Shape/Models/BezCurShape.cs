using Avalonia;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using Editor.Entities.Shape.DTOs;
using Editor.Shared;
using System.Collections.ObjectModel;
using System.Linq;

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

        public override ShapeDto ToDto()
        {
            return new ShapeDto
            {
                Type = "Bezier",
                X = X,
                Y = Y,
                Points = Points.Select(p => new PointDto { X = p.X, Y = p.Y }).ToList()
            };
        }

        public static BezCurShape FromDto(ShapeDto dto)
        {
            return new BezCurShape
            {
                X = dto.X,
                Y = dto.Y,
                Points = new ObservableCollection<Point>(dto.Points.Select(p => new Point(p.X, p.Y)))
            };
        }
    }
}
