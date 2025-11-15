using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using Editor.Entities.Shape.DTOs;
using Editor.Entities.Shape.Models;

namespace Editor.Features.Saving
{
    public class ShapeSerializer
    {
        private readonly JsonSerializerOptions _options;

        public ShapeSerializer()
        {
            _options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters = { new PointJsonConverter() }
            };
        }

        public string Serialize(IEnumerable<EditorShape> shapes)
        {
            var shapeDtos = shapes.Select(shape => shape.ToDto()).ToList();
            return JsonSerializer.Serialize(shapeDtos, _options);
        }

        public List<EditorShape> Deserialize(string json)
        {
            var shapeDtos = JsonSerializer.Deserialize<List<ShapeDto>>(json, _options)
                ?? new List<ShapeDto>();

            return shapeDtos.Select(dto => dto.ToShape()).Where(shape => shape != null).ToList()!;
        }
    }
}