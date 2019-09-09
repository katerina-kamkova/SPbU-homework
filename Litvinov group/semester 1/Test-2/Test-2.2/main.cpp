#include "2.2.h"

#include <iostream>

using namespace std;

int main()
{
    Head* head = createHead();
    End* end = createEnd();

    input(head, end);

    if (check(head, end)) {
        cout << "The list is symmetric" << endl;
    } else {
        cout << "The list isn`t symmetric" << endl;
    }

    deleteList(head);
    delete head;
    delete end;

    return 0;
}
