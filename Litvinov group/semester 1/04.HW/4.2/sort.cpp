#include "sort.h"

#include <iostream>

using namespace std;

void makeHeap(vector<int>& myArray, int n, int i) {
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

void heapSort(vector<int>& myArray) {
    int n = myArray.size();
    for (int i = n / 2 - 1; i >= 0; --i) {
        makeHeap(myArray, n, i);
    }

    for (int i = n - 1; i >= 0; --i) {
        swap(myArray[0], myArray[i]);
        makeHeap(myArray, i, 0);
    }
}
