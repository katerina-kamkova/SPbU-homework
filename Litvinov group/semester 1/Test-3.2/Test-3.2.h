#pragma once

#include <vector>
#include <string>

//List
struct List;

//Create list
List* createList();

//Fill List
void input(List* list);

//Main processes
void eventLoop(List* list, std::vector<std::string> table);

//Print list
void print(List* list);

//Delete list
void deleteList(List* list);

//Check whether the list is empty
bool isEmpty(List* list);
