using System.Windows;

namespace Ordinacija.Features.Common
{
    public class PageNavigationService
    {
        private Window _currentWindow;

        public void ShowWindow(Window window)
        {
            _currentWindow?.Hide();
            _currentWindow = window;
            _currentWindow.Show();
        }
    }
}
