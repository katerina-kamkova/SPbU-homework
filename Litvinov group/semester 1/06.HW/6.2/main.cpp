#include "6.2.h"
#include "stack.h"

#include <iostream>

using namespace std;

int main()
{
    string characters = "";
    cout << "Enter the string: ";
    cin >> characters;

    Stack *stack = createStack();

    if (!eventLoop(stack, characters) || !isEmpty(stack))
    {
        cout << endl << "The string isn`t balanced" << endl;
    } else {
        cout << endl << "The string is balanced" << endl;
    }

    deleteStack(stack);

    return 0;
}
