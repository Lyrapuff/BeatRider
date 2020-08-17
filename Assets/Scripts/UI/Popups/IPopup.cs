using System;

namespace UI.Popups
{
    public interface IPopup
    {
        Action<string> OnClosed { get; set; }
        string Title { get; set; }
        string[] Options { get; set; }
        
        void Close(string optionName);
        void CloseSilent();
    }
}