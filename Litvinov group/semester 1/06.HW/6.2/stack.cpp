#include "stack.h"

#include <iostream>
#include <string>

using namespace std;

struct StackElement {
    char value;
    StackElement *next;
};

struct Stack {
    StackElement *head;
};

Stack* createStack() {
    return new Stack{};
}

void push(Stack *stack, char value) {
    StackElement *newElement = new StackElement{value, stack->head};
    stack->head = newElement;
}

char pop(Stack *stack) {
    StackElement *temp = stack->head;
    stack->head = temp->next;
    char answer = temp->value;
    delete temp;
    return answer;
}

bool isEmpty(Stack* stack) {
    return stack->head == nullptr;
}

void deleteStack(Stack* stack) {
    while(!isEmpty(stack)) {
        int a = pop(stack);
    }
    delete stack;
}

StackElement* head(Stack* stack){
    return stack->head;
}
