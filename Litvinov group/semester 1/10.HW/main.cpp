#include "10.1.h"

#include <iostream>

using namespace std;

int main()
{
    int n = 0;
    int k = 0;

    int** matrix = nullptr;
    City* states = nullptr;
    bool* allCities = input(n, k, matrix, states);

    if (allCities != nullptr) {
        eventLoop(n, k, matrix, states, allCities);
        printResults(k, states);
        freeMemory(n, k, matrix, states, allCities);
    }

    return 0;
}
