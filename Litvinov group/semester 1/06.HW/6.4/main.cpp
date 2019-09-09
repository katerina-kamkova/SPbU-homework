#include "6.4.h"

#include <iostream>

using namespace std;

int main()
{
    Notebook *notebook = createNotebook();
    input(notebook);

    mergeSort(notebook, menu());

    print(notebook);

    deleteNotebook(notebook);

    return 0;
}
