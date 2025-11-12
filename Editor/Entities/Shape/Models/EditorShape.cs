using CommunityToolkit.Mvvm.ComponentModel;
using Editor.Shared;

namespace Editor.Entities.Shape.Models
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
    }
