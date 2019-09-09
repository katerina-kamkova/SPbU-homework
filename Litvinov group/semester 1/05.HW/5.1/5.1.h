#pragma once

//The pointer to the least element
struct List;

//The function to make the list
List *createList();

//The event loop
void eventLoop(List *list);
