#include "Test-3.2.h"

#include <iostream>

using namespace std;

int main()
{
    List* list = createList();
    input(list);

    if (!isEmpty(list)) {
        vector<string> table;
        eventLoop(list, table);
        print(list);
    }

    deleteList(list);

    return 0;
}
