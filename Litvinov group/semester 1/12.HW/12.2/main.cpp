#include "kruskal.h"

#include <iostream>

using namespace std;

int main()
{
    int size = matrixSize();

    Edge* firstE = createEList(size);

    eventLoop(firstE, size);
    print(firstE);

    deleteList(firstE);

    return 0;
}


