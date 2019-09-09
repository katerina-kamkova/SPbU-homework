#include "4.3.h"

#include <iostream>
#include <locale.h>

using namespace std;

int main()
{
    setlocale(LC_CTYPE, "Russian");

    Notebook* notebook = createNotebook();
    input(notebook);

    menu();
    eventLoop(notebook);
    cout << endl;

    deleteContacts(notebook);

    return 0;
}
