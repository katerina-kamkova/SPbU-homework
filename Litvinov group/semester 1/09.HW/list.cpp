#include <iostream>
#include <string>

using namespace std;

struct Element {
    string word;
    int amount;
    Element* next;
};

struct List {
    Element* first;
};

List* createList() {
    return new List{};
}

void addElement(List* list, const string& word) {
    Element* temp = list->first;
    bool alreadyInList = false;
    while (temp != nullptr) {
        if (temp->word == word) {
            alreadyInList = true;
            ++temp->amount;
        }
        temp = temp->next;
    }
    if (!alreadyInList) {
        Element* newElement = new Element{word, 1, nullptr};
        if (list->first == nullptr) {
            list->first = newElement;
        } else {
            temp = list->first;
            while (temp->next != nullptr) {
                temp = temp->next;
            }
            temp->next = newElement;
        }
    }
}

void deleteList(List* list) {
    while (list->first != nullptr) {
        Element* temp = list->first;
        list->first = temp->next;
        delete temp;
    }
    delete list;
}

void printList(List* list) {
    Element* temp = list->first;
    while (temp != nullptr) {
        cout << temp->word << ' ' << temp->amount << endl;
        temp = temp->next;
    }
}

int lengthOfList(List* list) {
    Element* temp = list->first;
    int counter = 0;
    while (temp != nullptr) {
        ++counter;
        temp = temp->next;
    }
    return counter;
}
