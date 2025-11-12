using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;

namespace Editor.Shared
{
    public interface IShapeDrawer
    {
        bool IsDrawing { get; }
        void OnPointerPressed(Point position);
        void OnPointerMoved(Point position);
        void OnPointerReleased(Point position);
        void Cancel(); 
    }
}
