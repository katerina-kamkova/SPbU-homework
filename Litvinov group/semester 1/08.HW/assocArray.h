#pragma once

#include <string>

//The pointer to the top
struct Tree;

//Create pointer to the top
Tree* createTree();

//To add new node if necessary
void addNode(Tree* tree, int key, const std::string& str);

//Return meaning by key
std::string getStr(Tree* tree, int key);

//Check whether such element is in the tree
bool checkPresence(Tree* tree, int key);

//Delete element
void deleteNode(Tree* tree, int key);

//Delete the whole tree
void deleteTree(Tree* tree);
