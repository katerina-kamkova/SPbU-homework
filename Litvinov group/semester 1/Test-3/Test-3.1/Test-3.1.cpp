#include <iostream>

using namespace std;

struct StackElement {
    int value;
    StackElement *next;
};

struct Stack {
    StackElement *head;
};

Stack* createStack() {
    return new Stack{};
}

void push(Stack *justStack, int value) {
    StackElement *newHead = new StackElement{value, justStack->head};
    justStack->head = newHead;
}

int pop(Stack *justStack) {
    int value = justStack->head->value;
    StackElement *temp = justStack->head;
    justStack->head = justStack->head->next;
    delete temp;
    return value;
}

bool isEmpty(Stack *justStack) {
    return justStack->head == nullptr;
}

void deleteStack(Stack *justStack) {
    while (!isEmpty(justStack)) {
        StackElement *temp = justStack->head;
        justStack->head = justStack->head->next;
        delete temp;
    }
    delete justStack;
}
