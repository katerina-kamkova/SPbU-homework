#include "5.1.h"

#include <iostream>
#include <locale.h>

using namespace std;

int main()
{
    setlocale(LC_CTYPE, "Russian");

    List *list = createList();

    eventLoop(list);

    cout << endl;

    return 0;
}
