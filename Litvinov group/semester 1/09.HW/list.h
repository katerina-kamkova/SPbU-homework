#pragma once

#include <string>

//Pointer to the top
struct List;

//Create list
List* createList();

//Add element in the end of the list
void addElement(List* list, const std::string& word);

//Delete the whole list
void deleteList(List* list);

//Print list
void printList(List* list);

//Return the length of the list
int lengthOfList(List* list);



