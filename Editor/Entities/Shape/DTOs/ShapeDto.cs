using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia;
using Editor.Entities.Shape.Models;
using Editor.Features.Saving;

namespace Editor.Entities.Shape.DTOs
{
    public class ShapeDto
    {
        public string Type { get; set; } = string.Empty;
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public List<PointDto> Points { get; set; } = new();

        public EditorShape? ToShape()
        {
            return Type switch
            {
                "Oval" => new OvalShape { X = X, Y = Y, Width = Width, Height = Height },
                "Bezier" => new BezCurShape
                {
                    X = X,
                    Y = Y,
                    Points = new ObservableCollection<Point>(Points.Select(p => new Point(p.X, p.Y)))
                },
                _ => null
            };
        }
    }
}