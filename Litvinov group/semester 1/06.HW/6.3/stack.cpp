#include <iostream>
#include <string>
#include <cmath>

using namespace std;

struct StackElement {
    string str;
    StackElement *next;
};

struct Stack {
    StackElement *head;
};

Stack* createStack() {
    return new Stack{};
}

void push(Stack *stack, const string& str) {
    StackElement *newElement = new StackElement{str, stack->head};
    stack->head = newElement;
}

string pop(Stack *stack) {
    StackElement *temp = stack->head;
    stack->head = temp->next;
    string answer = temp->str;
    delete temp;
    return answer;
}

void deleteStack(Stack* stack) {
    StackElement* temp = stack->head;
    while (temp != nullptr) {
        StackElement* del = temp;
        temp = temp->next;
        delete del;
    }
    delete stack;
}

bool isEmpty(Stack* stack) {
    return stack->head == nullptr;
}
