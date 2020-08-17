using System;

namespace General.Services.Back
{
    public interface IBack
    {
        void Handle();

        void Add(Action handler);
        void Set(Action handler);
        void RemoveLast();
    }
}