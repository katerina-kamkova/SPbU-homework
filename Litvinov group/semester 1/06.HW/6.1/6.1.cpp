#include "6.1.h"
#include "stack.h"

#include <iostream>
#include <string>
#include <cmath>

using namespace std;

void definer(Stack *stack, const string& symbol) {
    char first = symbol[0];
    if (int(first) == 42) {
        int second = pop(stack);
        int first = pop(stack);
        addElement(stack, first * second);
    } else if (int(first) == 43) {
        char second = pop(stack);
        char first = pop(stack);
        addElement(stack, first + second);
    } else if (int(first) == 45) {
        int second = pop(stack);
        int first = pop(stack);
        addElement(stack, first - second);
    } else if (int(first) == 47) {
        int second = pop(stack);
        int first = pop(stack);
        addElement(stack, first / second);
    } else {
        int value = 0;
        int length = symbol.size();
        for (int i = length; i > 0; --i) {
            value += (symbol[length - i] - '0') * pow(10, i - 1);
        }
        addElement(stack, value);
    }
}
