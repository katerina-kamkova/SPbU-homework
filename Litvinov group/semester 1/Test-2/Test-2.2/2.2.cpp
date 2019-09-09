#include "2.2.h"

#include <fstream>
#include <cmath>
#include <string>

using namespace std;

//The number
struct Number {
    int number;
    Number* next;
    Number* previous;
};

//Pointer to the first number
struct Head {
    Number* first;
};

//Pointer to the last number
struct End {
    Number* last;
};

//Create tree
Head* createHead() {
    return new Head{};
}

//Create End
End* createEnd() {
    return new End{};
}

//To make the number from the character
int defineNumber(const string& number) {
    int value = 0;
    int length = number.size();
    for (int i = length; i > 0; --i) {
        value += (number[length - i] - 48) * pow(10, i - 1);
    }
    return value;
}

//Add the number to the list
void addNumber(Head* head, End* end, const string& number) {
    int value = defineNumber(number);
    Number* newNumber = new Number{value, nullptr, nullptr};
    if (head->first == nullptr) {
        head->first = newNumber;
        end->last = newNumber;
    } else {
        end->last->next = newNumber;
        newNumber->previous = end->last;
        end->last = newNumber;
    }
}

//Read the numbers from the file and put them into list
void input(Head* head, End* end) {
    ifstream fin;
    fin.open("input.txt");

    if (fin.is_open()) {
        while (!fin.eof()) {
            string number = "";
            fin >> number;
            addNumber(head, end, number);
        }
    }

    fin.close();
}

//To check whether the list is symmetric
bool check(Head* head, End* end) {
    Number* first = head->first;
    Number* last = end->last;
    while (first != last && first->previous != last) {
        if (first->number != last->number) {
            return false;
        }
        first = first->next;
        last = last->previous;
    }
    return true;
}

//Delete the list
void deleteList(Head* head) {
    while (head->first->next != nullptr) {
        Number* temp = head->first;
        head->first = head->first->next;
        delete temp;
    }
}
