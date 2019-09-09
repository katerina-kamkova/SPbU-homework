#include "Test2.1.h"

#include <iostream>

using namespace std;

int main()
{
    List* list = createList();
    input(list);

    swapList(list);
    print(list);

    deleteList(list);

    return 0;
}
