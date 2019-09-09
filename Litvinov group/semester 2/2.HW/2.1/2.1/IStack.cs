using System;

namespace Stack
{
    public interface IStack
    {
        void Push(string meaning);
        string Pop();
        bool IsEmpty();
        int Size();
        void Clean();
    }
}
