
using Editor.Entities.Shape.DTOs;
using Editor.Shared;

namespace Editor.Entities.Shape.Models
{
    public class OvalShape : EditorShape
    {
        public override ShapeType Type => ShapeType.Oval;

        public override ShapeDto ToDto()
        {
            return new ShapeDto
            {
                Type = "Oval",
                X = X,
                Y = Y,
                Width = Width,
                Height = Height
            };
        }

        public static OvalShape FromDto(ShapeDto dto)
        {
            return new OvalShape
            {
                X = dto.X,
                Y = dto.Y,
                Width = dto.Width,
                Height = dto.Height
            };
        }
    }
}
