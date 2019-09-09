#include <iostream>
#include "6.3.h"
#include "stack.h"

using namespace std;

int main()
{
    Stack* stack = createStack();

    queue<string> output = eventLoop(stack);
    print(output);

    deleteStack(stack);

    return 0;
}
