#include "6.2.h"
#include "stack.h"

#include <iostream>

using namespace std;

bool eventLoop(Stack* stack, const string& characters) {
    for (int i = 0; i < characters.size(); ++i) {
        if (head(stack) == nullptr || characters[i] == '(' || characters[i] == '[' || characters[i] == '{') {
            push(stack, characters[i]);
        } else {
            char temp = pop(stack);
            if (characters[i] == ')' && temp != '(' ||
                characters[i] == ']' && temp != '[' ||
                characters[i] == '}' && temp != '{') {
                return false;
            }
        }
    }
    return true;
}
