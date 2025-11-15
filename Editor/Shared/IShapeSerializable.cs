using Editor.Entities.Shape.DTOs;

namespace Editor.Shared
{
    public interface IShapeSerializable
    {
        ShapeDto ToDto();
    }
}
