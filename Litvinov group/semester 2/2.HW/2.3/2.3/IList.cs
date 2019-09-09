namespace HashTable
{
    public interface IList
    {
        void Add(string value, int position);
        bool IsEmpty();
        bool CheckPresence(string value);
        string ReturnString(int position);
        int ReturnNumber(string value);
        void DeleteElement(string str);
        int GetSize();
    }
}