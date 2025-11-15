using System.Text.Json;
using System.Text.Json.Serialization;
using Avalonia;
using Editor.Entities.Shape.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.Linq;

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
                Converters = {
                    new PointJsonConverter(),
                }
            };
        }

        public string Serialize(IEnumerable<EditorShape> shapes)
        {
            var shapeDtos = shapes.Select(shape => ShapeDto.FromShape(shape)).ToList();
            return JsonSerializer.Serialize(shapeDtos, _options);
        }

        public List<EditorShape> Deserialize(string json)
        {
            var shapeDtos = JsonSerializer.Deserialize<List<ShapeDto>>(json, _options)
                ?? new List<ShapeDto>();

            return shapeDtos.Select(dto => dto.ToShape()).Where(shape => shape != null).ToList()!;
        }
    }

    public class ShapeDto
    {
        public string Type { get; set; } = string.Empty;
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public List<PointDto> Points { get; set; } = new();

        public static ShapeDto FromShape(EditorShape shape)
        {
            var dto = new ShapeDto
            {
                X = shape.X,
                Y = shape.Y
            };

            switch (shape)
            {
                case OvalShape oval:
                    dto.Type = "Oval";
                    dto.Width = oval.Width;
                    dto.Height = oval.Height;
                    break;
                case BezCurShape bezier:
                    dto.Type = "Bezier";
                    dto.Points = bezier.Points.Select(p => new PointDto { X = p.X, Y = p.Y }).ToList();
                    break;
            }

            return dto;
        }

        public EditorShape? ToShape()
        {
            return Type switch
            {
                "Oval" => new OvalShape { X = X, Y = Y, Width = Width, Height = Height },
                "Bezier" => new BezCurShape
                {
                    X = X,
                    Y = Y,
                    Points = new ObservableCollection<Point>(Points.Select(p => new Point(p.X, p.Y)))
                },
                _ => null
            };
        }
    }

    public class PointDto
    {
        public double X { get; set; }
        public double Y { get; set; }
    }

    public class PointJsonConverter : JsonConverter<Point>
    {
        public override Point Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var doc = JsonDocument.ParseValue(ref reader);
            var root = doc.RootElement;

            return new Point(
                root.GetProperty("x").GetDouble(),
                root.GetProperty("y").GetDouble()
            );
        }

        public override void Write(Utf8JsonWriter writer, Point value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteNumber("x", value.X);
            writer.WriteNumber("y", value.Y);
            writer.WriteEndObject();
        }
    }


}