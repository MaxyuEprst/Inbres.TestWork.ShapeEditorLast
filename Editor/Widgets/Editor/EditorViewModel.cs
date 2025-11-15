using Avalonia;
using Avalonia.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Editor.Entities.Shape.Models;
using Editor.Features.Drawing;
using Editor.Features.Saving;
using Editor.Shared;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Editor.ViewModels
{
    public partial class EditorViewModel : ViewModelBase
    {
        private readonly ShapeEditorModel _model;
        private IShapeDrawer? _currentDrawer;
        private FileShapeStorage? _shapeStorage;

        private FileShapeStorage ShapeStorage => _shapeStorage ??= new FileShapeStorage(new ShapeSerializer());

        [ObservableProperty]
        private ShapeType _currentShapeType = ShapeType.None;

        [ObservableProperty]
        private string _statusMessage = "Ready";

        [ObservableProperty]
        private EditorShape? _selectedShape;

        public ObservableCollection<EditorShape> Shapes => _model.Shapes;

        public EditorViewModel()
        {
            _model = new ShapeEditorModel();

            // Явно устанавливаем drawer при инициализации
            SetDrawerForShape(CurrentShapeType);

            LoadShapesOnStartup();
        }

        // Этот метод вызывается при изменении CurrentShapeType через свойство
        partial void OnCurrentShapeTypeChanged(ShapeType value)
        {
            CancelCurrentDrawing();
            SetDrawerForShape(value);
        }

        [RelayCommand]
        private void ChangeMod(ShapeType shapeType) => CurrentShapeType = shapeType;

        [RelayCommand]
        private void ClearAll() => _model.ClearShapes();

        [RelayCommand]
        private async Task SaveShapesAsync()
        {
            try
            {
                await ShapeStorage.SaveShapesAsync(Shapes.ToList());
                StatusMessage = $"Saved {Shapes.Count} shapes";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Save failed: {ex.Message}";
            }
        }

        [RelayCommand]
        private async Task LoadShapesAsync()
        {
            try
            {
                var shapes = await ShapeStorage.LoadShapesAsync();
                _model.LoadShapes(shapes);
                StatusMessage = $"Loaded {shapes.Count} shapes";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Load failed: {ex.Message}";
            }
        }

        private async void LoadShapesOnStartup()
        {
            try
            {
                var shapes = await ShapeStorage.LoadShapesAsync();
                if (shapes.Any())
                {
                    _model.LoadShapes(shapes);
                    StatusMessage = $"Auto-loaded {shapes.Count} shapes";
                }
            }
            catch
            {
                // Игнорируем ошибки при автозагрузке
            }
        }

        private void SetDrawerForShape(ShapeType type)
        {
            _currentDrawer = type switch
            {
                ShapeType.Oval => new OvalDrawer(_model),
                ShapeType.BezierCurve => new BezierDrawer(_model),
                _ => null
            };

            // Обновляем статус
            StatusMessage = type == ShapeType.None ? "Select shape type" : $"Drawing: {type}";
        }

        public void OnPointerPressed(Point position, PointerUpdateKind updateKind)
        {
            if (CurrentShapeType == ShapeType.None) return;

            if (updateKind == PointerUpdateKind.RightButtonPressed)
            {
                _currentDrawer?.Cancel();
                return;
            }

            var shape = _currentDrawer?.OnPointerPressed(position);
            if (shape != null && !_model.Shapes.Contains(shape))
                _model.Shapes.Add(shape);
        }

        public void OnPointerMoved(Point position) =>
            _currentDrawer?.OnPointerMoved(position);

        public void OnPointerReleased(Point position)
        {
            var shape = _currentDrawer?.OnPointerReleased(position);
            if (shape != null && !_model.Shapes.Contains(shape))
                _model.Shapes.Add(shape);
        }

        private void CancelCurrentDrawing() => _currentDrawer?.Cancel();

        public void OnKeyPressed(Key key)
        {
            if (key == Key.Escape)
                _currentDrawer?.Cancel();
        }
    }
}