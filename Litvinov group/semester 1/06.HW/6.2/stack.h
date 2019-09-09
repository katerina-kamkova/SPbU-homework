#pragma once

//The stack element
struct StackElement;

//The pointer to the top of the stack
struct Stack;

//The function making the stack
Stack* createStack();

//The function, adding the character to stack
void push(Stack *stack, char value);

//The function returning the top element
char pop(Stack *stack);

//The function checking whether the stack is empty
bool isEmpty(Stack* stack);

//The function deleting the stack
void deleteStack(Stack* stack);

//The function returning the pointer to the top of the stack
StackElement* head(Stack* stack);
