#pragma once

//Vertex: number and status checked(true/false)
struct City;

//Edge: length and incidental vertex
struct Road;

//Fill all necessary structures
bool* input(int& n, int& k, int**& matrix, City*& states);

//Main processes
void eventLoop(int n, int k, int** matrix, City* states, bool* allCities);

//Print states
void printResults(int k, City* states);

//Free memory
void freeMemory(int n, int k, int** matrix, City* states, bool* allCities);
