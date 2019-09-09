#include <iostream>
#include "7.2.h"

using namespace std;

int main()
{
    vector<string> expression;
    input(expression);

    Head* head = createHead();
    makeTree(head, expression);

    cout << "The expression: ";
    print(head);
    cout << endl;

    cout << "The answer: " << countExp(head) << endl;

    deleteTree(head);

    return 0;
}
