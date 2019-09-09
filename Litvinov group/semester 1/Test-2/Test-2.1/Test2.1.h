#pragma once

//Pointer to the first element
struct List;

//Create list
List* createList();

//Make list with numbers from file input.txt
void input(List* list);

//To change the elements in reverse order
void swapList(List* list);

//Print list
void print(List* list);

//Delete list
void deleteList(List* list);

