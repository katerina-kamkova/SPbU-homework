#pragma once

//The pointer to the top of the stack
struct Stack;

//The function making the stack
Stack* createStack();

//The function, adding the number to stack
void addElement(Stack* stack, int value);

//The function returning the top element
int pop(Stack* stack);

//Delete stack
void deleteStack(Stack* stack);
