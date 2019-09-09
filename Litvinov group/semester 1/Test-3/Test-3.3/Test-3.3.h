#pragma once

//Vertex
struct Vertex;

//Create array
int** createArray(int& length);

//Create array of vertexes
Vertex* createVert(int length);

//Print vertexes
void printVertexes(int** array, int length, Vertex* vert);

//Delete array
void deleteArray(int** array, int length);

//Delete Vertexes
void deleteVert(Vertex* vert);
