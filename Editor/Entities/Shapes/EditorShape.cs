using CommunityToolkit.Mvvm.ComponentModel;

    namespace Editor.Entities.Shapes
    {
        public abstract partial class EditorShape : ObservableObject
        {
            public abstract ShapeType Type { get; }

            [ObservableProperty]
            private double _x;

            [ObservableProperty]
            private double _y;

            [ObservableProperty]
            private double _width;

            [ObservableProperty]
            private double _height;
        }

        public enum ShapeType
        {
            Oval,
            BezierCurve,
            None
        }
    }
