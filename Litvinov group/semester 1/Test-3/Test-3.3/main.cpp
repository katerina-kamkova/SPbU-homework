#include "Test-3.3.h"

#include <iostream>

using namespace std;

int main()
{
    int length = 0;
    int** array = createArray(length);

    if (array != nullptr) {
        Vertex* vert = createVert(length);

        cout << "Vertexes: ";
        printVertexes(array, length, vert);
        cout << endl;
        deleteVert(vert);
    }

    deleteArray(array, length);
    return 0;
}
