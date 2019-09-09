#pragma once

#include "stack.h"

#include <iostream>
#include <string>
#include <queue>

//Get data from the file and convert from infix to postfix form
std::queue<std::string> eventLoop(Stack *stack);

//Print the answer
void print(std::queue<std::string>& output);
