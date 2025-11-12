using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace Editor.Widgets.Editor
{
    public class PointsToGeometryConverter : IValueConverter
    {
        public static PointsToGeometryConverter Instance { get; } = new();

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is not ObservableCollection<Point> points || points.Count < 2)
                return null;

            var geometry = new StreamGeometry();

            using (var ctx = geometry.Open())
            {
                ctx.BeginFigure(points[0], false);
                for (int i = 0; i < points.Count; i++)
                {
                    Debug.WriteLine($"Point[{i}] = ({points[i].X:F2}, {points[i].Y:F2})");
                }
                Debug.WriteLine($"Total points: {points.Count}");
                Debug.WriteLine("===============================");
                if (points.Count == 2)
                {
                    ctx.LineTo(points[1]);
                }
                else
                {
                    int i = 1;
                    for (; i + 1 < points.Count; i += 2)
                    {
                        var control = points[i];
                        var end = points[i + 1];
                        ctx.QuadraticBezierTo(control, end);
                    }

                    if (i < points.Count)
                    {
                        ctx.LineTo(points[i]);
                    }
                }

                ctx.EndFigure(false);
            }

            return geometry;
        }


        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
