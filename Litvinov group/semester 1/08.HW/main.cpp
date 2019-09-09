#include "8.1.h"

#include <iostream>

using namespace std;

int main()
{
    Tree* tree = createTree();
    eventLoop(tree);

    deleteTree(tree);

    return 0;
}
