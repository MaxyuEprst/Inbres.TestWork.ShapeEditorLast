using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Editor.Entities.Shapes;
using Editor.Shared;
using Editor.Widgets.Editor;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace Editor.ViewModels
{
    public partial class EditorViewModel : ViewModelBase
    {
        private readonly ShapeEditorModel _model;

        private Point? _firstClickPoint;
        private bool _isFirstClick = true;
        private Point _currentMousePosition;


        [ObservableProperty]
        private bool _isDrawingMode = false;

        [ObservableProperty]
        private ShapeType _currentShapeType = ShapeType.Oval;

        [ObservableProperty]
        private EditorShape? _selectedShape;

        [ObservableProperty]
        private EditorShape? _previewShape;

        private void ResetDrawingState()
        {
            _firstClickPoint = null;
            _isFirstClick = true;
            IsDrawingMode = false;
            PreviewShape = null;
        }
        [RelayCommand]
        private void SetOvalMode()
        {
            CurrentShapeType = CurrentShapeType == ShapeType.Oval ? ShapeType.None : ShapeType.Oval;
        }

        public EditorViewModel()
        {
            _model = new ShapeEditorModel();
            _model.Shapes.CollectionChanged += (s, e) => OnPropertyChanged(nameof(Shapes));
        }

        public ObservableCollection<EditorShape> Shapes => _model.Shapes;

        public void CreateShapeAtPoint(Point point)
        {
            if (CurrentShapeType == ShapeType.None) return;

            if (_isFirstClick)
            {
                _firstClickPoint = point;
                _isFirstClick = false;
                IsDrawingMode = true;
                CreatePreviewShape(point);
            }
            else
            {
                if (_firstClickPoint.HasValue)
                {
                    CreateFinalShape(_firstClickPoint.Value, point);
                    ResetDrawingState();
                }
            }
        }

        public void UpdatePreview(Point currentPoint)
        {
            if (!IsDrawingMode || !_firstClickPoint.HasValue) return;

            _currentMousePosition = currentPoint;
            UpdatePreviewShape(_firstClickPoint.Value, currentPoint);
        }

        private void CreatePreviewShape(Point firstPoint)
        {
            PreviewShape = CurrentShapeType switch
            {
                ShapeType.Oval => new OvalShape
                {
                    X = firstPoint.X,
                    Y = firstPoint.Y,
                    Width = 0,
                    Height = 0,
                    IsPreview = true 
                },
                _ => null
            };
        }

        private void UpdatePreviewShape(Point firstPoint, Point currentPoint)
        {
            if (PreviewShape == null) return;

            var x = System.Math.Min(firstPoint.X, currentPoint.X);
            var y = System.Math.Min(firstPoint.Y, currentPoint.Y);
            var width = System.Math.Abs(currentPoint.X - firstPoint.X);
            var height = System.Math.Abs(currentPoint.Y - firstPoint.Y);

            width = System.Math.Max(width, 1);
            height = System.Math.Max(height, 1);

            PreviewShape.X = x;
            PreviewShape.Y = y;
            PreviewShape.Width = width;
            PreviewShape.Height = height;
        }

        private void CreateFinalShape(Point firstPoint, Point secondPoint)
        {
            var x = System.Math.Min(firstPoint.X, secondPoint.X);
            var y = System.Math.Min(firstPoint.Y, secondPoint.Y);
            var width = System.Math.Abs(secondPoint.X - firstPoint.X);
            var height = System.Math.Abs(secondPoint.Y - firstPoint.Y);

            width = System.Math.Max(width, 10);
            height = System.Math.Max(height, 10);

            _model.CreateShape(CurrentShapeType, new Point(x, y), width, height);
            SelectedShape = _model.Shapes.LastOrDefault();
        }

        public void CancelDrawing()
        {
            ResetDrawingState();
        }
    }
}