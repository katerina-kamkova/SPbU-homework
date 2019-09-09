#pragma once
#include <vector>
#include <string>

//Pointer to the top
struct Head;

//Create Head for tree
Head* createHead();

//Read symbols without brackets
void input(std::vector<std::string>& expression);

//Make tree
void makeTree(Head* head, std::vector<std::string>& exp);

//Print expression
void print(Head* head);

//Find answer
int countExp(Head* head);

//delete tree
void deleteTree(Head* head);
