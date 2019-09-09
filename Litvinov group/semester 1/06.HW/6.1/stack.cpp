#include "stack.h"

#include <iostream>

using namespace std;

struct StackElement {
    int value;
    StackElement* next;
};

struct Stack {
    StackElement* head;
};

Stack* createStack() {
    return new Stack{};
}

void addElement(Stack* stack, int value) {
    StackElement* newElement = new StackElement{value, stack->head};
    stack->head = newElement;
}

int pop(Stack* stack) {
    StackElement* temp = stack->head;
    stack->head = temp->next;
    int answer = temp->value;
    delete temp;
    return answer;
}

void deleteStack(Stack* stack) {
    delete stack;
}
