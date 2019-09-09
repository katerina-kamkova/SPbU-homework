#pragma once

#include <string>

//Stack
struct Stack;

//Create stack
Stack* createStack();

//Push element into stack
void push(Stack *stack, const std::string& str);

//Take element from the stack
std::string pop(Stack *stack);

//Delete stack
void deleteStack(Stack* stack);

//Check whether the stack is empty
bool isEmpty(Stack* stack);
