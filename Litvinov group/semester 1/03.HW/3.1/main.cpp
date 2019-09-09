#include <iostream>
#include <locale.h>
#include <ctime>
#include <cstdlib>

using namespace std;

void insertSort(int *myArray, int l, int r) {
    for (int i = 0; i < r; ++i) {
        int j = i;
        while (myArray[j + 1] < myArray[j] && j != l - 1) {
            swap(myArray[j + 1], myArray[j]);
            --j;
        }
    }
}

void quickSortReal(int l, int r, int *myArray) {
    if (r - l < 10) {
        insertSort(myArray, l, r);
    } else {
        int x = myArray[(r + l) / 2];
        int i = l;
        int j = r;

        while (i <= j) {
            while (myArray[i] < x) {
                ++i;
            }
            while (myArray[j] > x) {
                --j;
            }
            if (i <= j) {
                swap (myArray[i], myArray[j]);
                ++i;
                --j;
            }
        }

        if (i < r) {
            quickSortReal(i, r, myArray);
        }
        if (l < j) {
            quickSortReal(l, j, myArray);
        }
    }
}

void quickSort(int *myArray, int n)
{
    quickSortReal(0, n - 1, myArray);
}

void outPut(int* myArray, int n) {
    for (int i = 0; i < n; ++i) {
        cout << myArray[i] << " ";
    }
}

int main() {
    setlocale(LC_CTYPE, "Russian");
    srand(time(nullptr));

    cout << "Введите количество элементов в массиве: ";
    int n = 0;
    cin >> n;

    int *myArray = new int[n];
    for (int i = 0; i < n; ++i) {
        myArray[i] = 1 + rand() % 100;
    }

    cout << endl << "Заполненный массив: ";
    outPut(myArray, n);

    quickSort(myArray, n);

    cout << endl << "Отсортированный массив: ";
    outPut(myArray, n);
    cout << endl;

    delete[] myArray;

    return 0;
}
