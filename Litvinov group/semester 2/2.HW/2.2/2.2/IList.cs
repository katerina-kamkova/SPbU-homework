using System;

namespace List
{
    public interface IList
    {
        void Add(string value, string position);
        void DeleteElement(string str);
        string ReturnString(string position);
        bool IsEmpty();
        int Size();
        void Clean();
        void Print();
    }
}
