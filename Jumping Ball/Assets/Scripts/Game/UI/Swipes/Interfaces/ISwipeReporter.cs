using System;

namespace Game.UI.Swipes.Interfaces
{
    public interface ISwipeReporter
    {
        event Action OnSwipeLeft;
        event Action OnSwipeRight;
    }
}