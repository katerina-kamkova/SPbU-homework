#include <iostream>
#include "eventLoop.h"

using namespace std;

int main()
{
    menu();

    Tree *tree = createTree();
    eventLoop(tree);

    deleteTree(tree);

    return 0;
}
