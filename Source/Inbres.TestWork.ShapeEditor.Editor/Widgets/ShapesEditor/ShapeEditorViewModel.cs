using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Inbres.TestWork.ShapeEditor.Editor.Entities.Shapes;

namespace Inbres.TestWork.ShapeEditor.Editor.Widgets.ShapesEditor
{
    partial class ShapeEditorViewModel : ViewModelBase
    {
        [ObservableProperty]
        private ObservableCollection<EditorShape> _shapes = new();

        [ObservableProperty]
        private EditorShape? _selectedShape;

        public ShapeEditorViewModel()
        {
            // Подписка на изменения коллекции
            Shapes.CollectionChanged += (s, e) =>
            {
                System.Diagnostics.Debug.WriteLine($"CollectionChanged: {e.Action}");
                if (e.NewItems != null)
                    System.Diagnostics.Debug.WriteLine($"Добавлено: {e.NewItems.Count} элементов");
                if (e.OldItems != null)
                    System.Diagnostics.Debug.WriteLine($"Удалено: {e.OldItems.Count} элементов");

                System.Diagnostics.Debug.WriteLine($"Всего: {Shapes.Count} элементов");
            };
            
        }

        [RelayCommand]
        private void AddOval()
        {
            var oval = new OvalShape
            {
                X = 10,
                Y = 10,
                Width = 80,
                Height = 60
            };
            Shapes.Add(oval);
            SelectedShape = oval;
        }

        [RelayCommand]
        private void AddBezierCurve()
        {
        }

        [RelayCommand]
        private void SelectShape(EditorShape shape)
        {
            SelectedShape = shape;
        }

    }
}