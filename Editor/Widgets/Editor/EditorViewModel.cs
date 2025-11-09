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

        [ObservableProperty]
        private ShapeType _currentShapeType = ShapeType.Oval;

        [ObservableProperty]
        private EditorShape? _selectedShape;

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

        [RelayCommand]
        private void AddOval()
        {
            var oval = new OvalShape
            {
                X = 100,
                Y = 100,
                Width = 80,
                Height = 60
            };
            Shapes.Add(oval);
        }

        public void CreateShapeAtPoint(Point point)
        {
            if (CurrentShapeType == ShapeType.None) return;

            _model.CreateShape(CurrentShapeType, point);
            SelectedShape = _model.Shapes.LastOrDefault();
        }
    }
}
