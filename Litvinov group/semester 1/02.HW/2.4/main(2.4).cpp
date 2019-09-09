#include <iostream>
#include <locale.h>
#include <ctime>
#include <cstdlib>

using namespace std;

void change(int *myArray, int n) {
    swap(myArray[0], myArray[n / 2]);

    int i = 0;
    int j = n - 1;
    int x = myArray[n / 2];

    while (i <= j) {
        while (myArray[i] < x) {
            ++i;
        }
        while (myArray[j] > x) {
            --j;
        }
        if (i <= j) {
            swap(myArray[i], myArray[j]);
            ++i;
            --j;
        }
    }
}

int main()
{
    setlocale(LC_CTYPE, "Russian");

    srand(time(nullptr));

    cout << "Введите число элементов в массиве: ";
    int n = 0;
    cin >> n;

    cout << endl << "Получившийся массив: ";
    int *myArray = new int[n];
    for (int i = 0; i < n; ++i) {
        myArray[i] = rand() % 100;
        cout << myArray[i] << " ";
    }

    change(myArray, n);

    cout << endl << "Отсортированный массив: ";
    for (int i = 0; i < n; ++i) {
        cout << myArray[i] << " ";
    }
    cout << endl;

    delete[] myArray;

    return 0;
}
