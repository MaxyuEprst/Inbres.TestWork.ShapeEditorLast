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


        [RelayCommand]
        private void AddOval()
        {
            System.Diagnostics.Debug.WriteLine("AddOval вызван!");
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
            // Будущая реализация
        }

        [RelayCommand]
        private void SelectShape(EditorShape shape)
        {
            SelectedShape = shape;
        }

    }
}
