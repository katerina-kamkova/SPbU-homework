#include <iostream>
#include <fstream>
#include <vector>
#include <string>

#include "Test-3.2.h"

using namespace std;

struct Element {
    string str;
    Element* next;
};

struct List {
    Element* first;
};

List* createList() {
    return new List{};
}

void add(List* list, const string& str) {
    Element* newElement = new Element{str, list->first};
    list->first = newElement;
}

void input(List* list) {
    ifstream fin;
    fin.open("input.txt");

    if (!fin.is_open()) {
        cout << "Error! File isn`t found";
        return;
    } else {
        while (!fin.eof()) {
            string str = "";
            fin >> str;
            add(list, str);
        }
        fin.close();
    }
}

void deleteElement(Element* parent) {
    Element* temp = parent->next;
    parent->next = parent->next->next;
    delete temp;
}

void eventLoop(List* list, vector<string> table) {
    Element* temp = list->first;
    table.push_back(temp->str);
    while (temp->next != nullptr) {
        for (int i = 0; i < table.size(); ++i) {
            if (temp->next == nullptr) {
                break;
            } else if (table[i] == temp->next->str) {
                deleteElement(temp);
                i = 0;
            }
        }
        if (temp->next != nullptr) {
            table.push_back(temp->next->str);
            temp = temp->next;
        }
    }
}

void print(List* list) {
    cout << "List without repeating elements: ";
    Element* temp = list->first;
    while (temp != nullptr) {
        cout << temp->str << ' ';
        temp = temp->next;
    }
    cout << endl;
}

void deleteList(List* list) {
    while (list->first != nullptr) {
        Element* temp = list->first;
        list->first = temp->next;
        delete temp;
    }
    delete list;
}

bool isEmpty(List* list) {
    return list->first == nullptr;
}
