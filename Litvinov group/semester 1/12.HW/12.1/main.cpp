#include <iostream>
#include "12.1.h"

using namespace std;

int main()
{
    string str = input();
    string temp = "";

    cout << "Enter the template: ";
    cin >> temp;
    cout << endl;

    int* array = prefixFunction(str, temp);
    int length = str.size() + temp.size() + 1;

    int answer = findFirstEnter(array, length, temp.size());

    cout << endl;
    if (answer == -1) {
        cout << "There`s no example in this string" << endl;
    } else {
        cout << "The position of the first occurrence of the entered line in the file: " << answer << endl;
    }

    delete[] array;

    return 0;
}
