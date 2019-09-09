#pragma once

//Pointer to the top node
struct Tree;

//Create the tree
Tree *createTree();

//The function which starts the function deleting all nodes
void deleteTree(Tree *tree);

//Add Node
void addNode(Tree *tree, int value);

//Delete Node
void deleteNode(Tree *tree, int value);

//Check presence
bool checkPresence(Tree* tree, int value);

//Check whether the tree is empty
bool isEmpty(Tree* tree);

//Print in ascending order
void printUp(Tree* tree);

//Print in descending order
void printDown(Tree* tree);
