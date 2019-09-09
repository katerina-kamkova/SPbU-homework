#include <iostream>
#include <locale.h>

using namespace std;

const int length = 100000;

void bubbleSort(int n, int *myArray) {
    for (int i = 0; i < n; ++i) {
        for (int j = n - 1; j > i; --j) {
            if (myArray[j] < myArray[j - 1]) {
                swap(myArray[j], myArray[j - 1]);
            }
        }
    }
}

void countingSort(int n, int *myArray) {
    int help[length] = {};

    for (int i = 0; i < n; ++i) {
        ++help[myArray[i]];
    }

    int helpNumber = 0;
    for (int i = 0; i < n; ++i) {
        while (help[helpNumber] == 0) {
            ++helpNumber;
        }
        myArray[i] = helpNumber;
        --help[helpNumber];
    }
}

int main()
{
    setlocale(LC_CTYPE, "Russian");

    cout << "Введите количество чисел в массиве: ";
    int n = 0;
    cin >> n;

    cout << endl << "Введите массив: ";
    int *myArray = new int[n];
    for (int i = 0; i < n; ++i) {
        cin >> myArray[i];
    }

    cout << endl << "Каким способом отсортировать массив: пузырьком - 1 или подсчётом - 2: ";
    int choice = 0;
    cin >> choice;

    if (choice == 1) {
        bubbleSort (n, myArray);
    } else {
        countingSort (n, myArray);
    }

    cout << endl;
    for (int i = 0; i < n; i ++) {
        cout << myArray[i] << " ";
    }
    cout << endl;

    delete[] myArray;

    return 0;
}
