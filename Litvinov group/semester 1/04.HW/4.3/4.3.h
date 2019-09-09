#pragma once

#include <string>

//Elements, which consist names and numbers
struct Contact;

//The pointer to the first element
struct Notebook;

//Create Notebook
Notebook* createNotebook();

//Make the virtual notebook using data from file
void input(Notebook *notebook);

//The event loop
void eventLoop(Notebook *notebook);

//Delete all contacts function
void deleteContacts(Notebook *notebook);

//Print menu function
void menu();
