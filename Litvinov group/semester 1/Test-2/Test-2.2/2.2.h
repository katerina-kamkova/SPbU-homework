#pragma once

//Pointer to the first number
struct Head;
//Pointer to the last number
struct End;

//Create Head
Head* createHead();

//Create End
End* createEnd();

//Read the numbers from the file and put them into list
void input(Head* head, End* end);

//To check whether the list is symmetric
bool check(Head* head, End* end);

//Delete the list
void deleteList(Head* head);
