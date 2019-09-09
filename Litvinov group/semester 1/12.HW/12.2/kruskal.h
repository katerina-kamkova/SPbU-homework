#pragma once

//Edge
struct Edge;

//Find the size of Matrix
int matrixSize();

//Create list from all edges
Edge* createEList(int size);

//Delete all extra edges
void eventLoop(Edge* firstE, int size);

//Print edges
void print(Edge* first);

//Delete list
void deleteList(Edge* first);
