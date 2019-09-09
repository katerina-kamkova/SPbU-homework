#pragma once

//Pointer to the last element
struct Stack;

//Create stack
Stack* createStack();

//Push element in stack
void push(Stack *justStack, int value);

//Delete element from stack
int pop(Stack *justStack);

//Check whether stack is empty
bool isEmpty(Stack *justStack);

//Delete stack
void deleteStack(Stack *justStack);
