using CommunityToolkit.Mvvm.ComponentModel;
using Editor.Entities.Shape.DTOs;
using Editor.Shared;

namespace Editor.Entities.Shape.Models
{
    public abstract partial class EditorShape : ObservableObject, IShapeSerializable
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

        [System.Text.Json.Serialization.JsonIgnore]
        public virtual bool IsPreview { get; set; }

        public abstract ShapeDto ToDto();
    }
}
