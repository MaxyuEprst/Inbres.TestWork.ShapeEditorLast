using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Editor.Entities.Shape.Models;
using Editor.Shared;
using Editor.Widgets.Editor;
using System;
using System.Collections.ObjectModel;

namespace Editor.ViewModels
{
    public partial class EditorViewModel : ViewModelBase
    {
        private readonly ShapeEditorModel _model;
        private Point _startPoint;
        private EditorShape? _currentShape;

        [ObservableProperty]
        private bool _isDrawing = false;

        [ObservableProperty]
        private ShapeType _currentShapeType = ShapeType.None;

        [ObservableProperty]
        private EditorShape? _selectedShape;

        [RelayCommand]
        private void ChangeMod(ShapeType shapeType)
        {
            CurrentShapeType = shapeType;
        }

        public EditorViewModel()
        {
            _model = new ShapeEditorModel();        
        }

        public ObservableCollection<EditorShape> Shapes => _model.Shapes;

        public void StartDrawing(Point position)
        {
            if (CurrentShapeType == ShapeType.None) return;

            _startPoint = position;
            IsDrawing = true;

            if (CurrentShapeType == ShapeType.Oval)
            {
                _currentShape = _model.CreateShape(ShapeType.Oval, position, 0, 0);
            }
            else if (CurrentShapeType == ShapeType.BezierCurve)
            {
                _currentShape = new BezCurShape();
                Shapes.Add(_currentShape);
                ((BezCurShape)_currentShape).Points.Add(position);
            }
        }

        public void UpdateDrawing(Point position)
        {
            if (!IsDrawing || _currentShape == null) return;

            if (CurrentShapeType == ShapeType.Oval)
            {
                UpdateOval(position);
            }
            else if (CurrentShapeType == ShapeType.BezierCurve)
            {
                UpdateBezier(position);
            }
        }

        public void FinishDrawing(Point position)
        {
            if (!IsDrawing) return;

            if (CurrentShapeType == ShapeType.Oval)
            {
                if (_currentShape?.Width < 2 || _currentShape?.Height < 2)
                {
                    _model.RemoveShape(_currentShape);
                }
            }
            else if (CurrentShapeType == ShapeType.BezierCurve)
            {
                if (_currentShape is BezCurShape bezier && bezier.Points.Count < 2)
                {
                    _model.RemoveShape(_currentShape);
                }
            }

            IsDrawing = false;
            _currentShape = null;
        }

        private void UpdateOval(Point position)
        {
            var x = Math.Min(_startPoint.X, position.X);
            var y = Math.Min(_startPoint.Y, position.Y);
            var width = Math.Abs(position.X - _startPoint.X);
            var height = Math.Abs(position.Y - _startPoint.Y);

            _currentShape.X = x;
            _currentShape.Y = y;
            _currentShape.Width = width;
            _currentShape.Height = height;
        }

        private void UpdateBezier(Point position)
        {
            if (_currentShape is BezCurShape bezier && bezier.Points.Count > 0)
            {
                bezier.Points[^1] = position;
            }
        }
    }
}
