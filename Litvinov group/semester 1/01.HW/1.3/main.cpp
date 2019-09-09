#include <iostream>

using namespace std;

void Change(int* A, int k, int j)
{
    for (int i = k; i < j - (j - k) / 2; ++i)
    {
        swap (A[i], A[j- 1 - i + k]);
    }
    return;
}

int main()
{
    int n = 0;
    int m = 0;
    cout << "Enter the amount of elements in the first segment: ";
    cin >> n;
    cout << endl << "Enter the amount of elements in the second segment: ";
    cin >> m;
    int *Array = new int[n + m];

    cout << endl << "Enter the array: ";
    for (int i = 0; i < n + m; ++i)
    {
        cin >> Array[i];
    }

    Change(Array, 0, n);
    Change(Array, n, n + m);
    Change(Array, 0, n + m);

    cout << endl << "The new array: ";
    for (int i = 0; i < n + m; ++i)
    {
        cout << Array[i] << " ";
    }
    cout << endl;

    delete Array;

    return 0;
}
