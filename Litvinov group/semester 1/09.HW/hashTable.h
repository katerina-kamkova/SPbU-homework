#pragma once

#include "list.h"

#include <string>

//Array of lists
struct HashTable;

//Create hash table
HashTable* createHashTable();

//Add string in hash table
void addString(HashTable* table, const std::string& str);

//Calculate and print max and average length of lists in hash table
void calculateLength(HashTable* table);

//Print word and amount of it in the text
void print(HashTable* table);

//Delete hash table
void deleteHashTable(HashTable* table);
