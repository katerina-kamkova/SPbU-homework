#include <iostream>
#include <locale.h>
#include <ctime>
#include <cstdlib>

using namespace std;

void makeHeap(int *myArray, int n, int i) {
    int root = i;
    int l = i * 2 + 1;
    int r = i * 2 + 2;

    if (l < n && myArray[l] > myArray[root]) {
        root = l;
    }

    if (r < n && myArray[r] > myArray[root]) {
        root = r;
    }

    if (root != i) {
        swap(myArray[i], myArray[root]);
        makeHeap(myArray, n, root);
    }
}

void heapSort(int *myArray, int n) {
    for (int i = n / 2 - 1; i >= 0; --i) {
        makeHeap(myArray, n, i);
    }

    for (int i = n - 1; i >= 0; --i) {
        swap(myArray[0], myArray[i]);
        makeHeap(myArray, i, 0);
    }
}

int main() {
    setlocale(LC_CTYPE, "Russian");
    srand(time(nullptr));

    cout << "Введите количество элементов в массиве: ";
    int n = 0;
    cin >> n;

    cout << endl << "Получившийся массив: ";
    int *myArray = new int[n];
    for (int i = 0; i < n; ++i) {
        myArray[i] = rand() % 11;
        cout << myArray[i] << " ";
    }

    heapSort(myArray, n);

    cout << endl << "Отсортированный массив: ";
    for (int i = 0; i < n; ++i) {
        cout << myArray[i] << " ";
    }

    int answer = 0;
    int answerCounter = 0;
    int current = myArray[0];
    int currentCounter = 1;
    for (int i = 1; i < n; ++i) {
        if (myArray[i] == current) {
            ++currentCounter;
        } else {
            if (currentCounter > answerCounter) {
                answerCounter = currentCounter;
                answer = current;
            }
            currentCounter = 1;
            current = myArray[i];
        }
    }
    if (currentCounter > answerCounter) {
            answer = current;
        }
    cout << endl << endl << "Наиболее чаасто повторяющийся элемент в массиве: " << answer;

    cout << endl;

    delete[] myArray;

    return 0;
}
