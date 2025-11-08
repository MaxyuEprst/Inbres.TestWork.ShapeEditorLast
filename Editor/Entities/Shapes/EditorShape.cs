using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Editor.Entities.Shapes
{
    public abstract class EditorShape
    {
        public abstract ShapeType Type { get; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        public bool IsPreview;
    }

    public enum ShapeType
    {
        Oval,
        BezierCurve,
        None
    }
}
