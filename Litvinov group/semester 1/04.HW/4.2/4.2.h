#pragma once
#include <vector>

//Fill vector with elements from file
std::vector<int> createVector();

//Print all elements
void print(std::vector<int>& myArray);

//Find the most common number
int mostFrequent(const std::vector<int>& myArray);
