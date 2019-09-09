#pragma once

//Make string from the file
std::string input();

//Make prefix-function
int* prefixFunction(const std::string& str, const std::string& temp);

//Return first enter
int findFirstEnter(int* array, int length, int n);
