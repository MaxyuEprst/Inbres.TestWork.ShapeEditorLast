namespace Editor.Entities.Shapes
{
    public class BezCurShape : EditorShape
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public override ShapeType Type => ShapeType.BezierCurve;
    }
}
