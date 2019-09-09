#include <iostream>

using namespace std;

int main()
{
    cout << "Enter the amount of elements: ";
    int n = 0;
    cin >> n;
    int counter = 0;
    int *Array = new int[n];

    cout << endl << "Enter the array: ";
    for (int i = 0; i < n; ++i)
    {
        cin >> Array[i];
        if (Array[i] == 0)
            counter ++;
    }

    cout << endl << "The amount the elements = 0: " << counter << endl;

    delete Array;

    return 0;
}
