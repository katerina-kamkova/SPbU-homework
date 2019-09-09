#include <iostream>
#include <locale.h>
#include <ctime>
#include <cstdlib>

using namespace std;

void makeHeap(long long *myArray, int n, int root) {
    int realRoot = root;
    int l = root * 2 + 1;
    int r = root * 2 + 2;

    if (l < n && myArray[l] > myArray[realRoot]) {
        realRoot = l;
    }

    if (r < n && myArray[r] > myArray[realRoot]) {
        realRoot = r;
    }

    if (realRoot != root) {
        swap(myArray[root], myArray[realRoot]);
        makeHeap(myArray, n, realRoot);
    }
}

void heapSort(long long *myArray, int n) {
    for (int i = n / 2 - 1; i >= 0; --i) {
        makeHeap(myArray, n, i);
    }

    for (int i = n - 1; i >= 0; --i) {
        swap(myArray[0], myArray[i]);
        makeHeap(myArray, i, 0);
    }
}

bool check(long long *nArray, int n, long long *kArray, int k) {
    int i = 0;
    int j = 0;
    bool check = false;
    while (i < n && j < k) {
        if (nArray[i] < kArray[j]) {
            ++i;
        } else if (nArray[i] > kArray[j]) {
            ++j;
        } else {
            cout << nArray[i] << ' ';
            ++i;
            ++j;
            check = true;
        }
    }
    return check;
}

int main() {
    setlocale(LC_CTYPE, "Russian");
    srand(time(nullptr));

    cout << "Введите количество элементов в массиве - n: ";
    int n = 0;
    cin >> n;

    cout << "Получившийся массив: ";
    long long *nArray = new long long[n];
    for (int i = 0; i < n; ++i) {
        nArray[i] = rand() % 1000000001;
        cout << nArray[i] << " ";
    }

    cout << endl << "Введите количество элементов для проверки - k: ";
    int k = 0;
    cin >> k;

    cout << "Сгенерировавшиеся элементы: ";
    long long *kArray = new long long[k];
    for (int i = 0; i < k; ++i) {
        kArray[i] = rand() % 1000000001;
        cout << kArray[i] << " ";
    }
    cout << endl;

    heapSort(nArray, n);
    heapSort(kArray, k);

    cout << endl << "Повторяющиеся элементы: ";
    if (!check(nArray, n, kArray, k)) {
        cout << "таких нет";
    }
    cout << endl;

    delete[] nArray;
    delete[] kArray;

    return 0;
}
