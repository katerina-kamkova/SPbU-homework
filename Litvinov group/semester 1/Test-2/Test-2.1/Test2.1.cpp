#include "Test2.1.h"

#include <fstream>
#include <iostream>
#include <cmath>

using namespace std;

struct Number {
    int number;
    Number* next;
};

struct List {
    Number* first;
};

List* createList() {
    return new List{};
}

int defineNumber(const string& number) {
    int value = 0;
    int length = number.size();
    for (int i = length; i > 0; --i) {
        value += (number[length - i] - '0') * pow(10, i - 1);
    }
    return value;
}

void addNumber(List* list, const string& number) {
    int n = defineNumber(number);
    if (list->first == nullptr) {
        Number* newNumber = new Number{n, nullptr};
        list->first = newNumber;
    } else {
        Number* temp = list->first;
        while (temp->next != nullptr) {
            temp = temp->next;
        }
        Number* newNumber = new Number{n, nullptr};
        temp->next = newNumber;
    }
}

void input(List* list) {
    ifstream fin;
    fin.open("input.txt");

    if (fin.is_open()) {
        while (!fin.eof()) {
            string number = "";
            fin >> number;
            addNumber(list, number);
        }
    }

    fin.close();
}

void realSwap(Number*& first, Number* last, bool& stop) {
    if (last->next != nullptr) {
        realSwap(first, last->next, stop);
    }

    if (!stop && first->next != nullptr) {
        swap(last->number, first->number);
        first = first->next;

        if (first == last || first->next == last) {
            stop = true;
        }
    }
}

void swapList(List* list) {
    bool stop = false;
    Number* first = list->first;
    realSwap(first, list->first, stop);
}

void print(List* list) {
    cout << "List elements in reverse order: ";

    Number* temp = list->first;
    while (temp->next != nullptr) {
        cout << temp->number << ' ';
        temp = temp->next;
    }
    cout << temp->number << endl;
}

void deleteList(List* list) {
    while (list->first != nullptr) {
        Number* temp = list->first;
        list->first = temp->next;
        delete temp;
    }
    delete list;
}
