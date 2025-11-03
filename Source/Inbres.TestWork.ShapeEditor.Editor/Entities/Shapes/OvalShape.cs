using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inbres.TestWork.ShapeEditor.Editor.Entities.Shapes
{
    public class OvalShape : EditorShape
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public override ShapeType Type => ShapeType.Oval;
    }
}
