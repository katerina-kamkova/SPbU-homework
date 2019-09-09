#include "5.1.h"

#include <iostream>

using namespace std;

struct ListElement {
    int value;
    ListElement *next;
};

struct List {
    ListElement *head;
};

void menu() {
    cout << "¬озможные операции:" << endl;
    cout << "0 Ц выйти" << endl;
    cout << "1 Ц добавить значение в сортированный список" << endl;
    cout << "2 Ц удалить значение из списка" << endl;
    cout << "3 Ц распечатать список" << endl;
}

List *createList() {
    return new List{};
}

bool isEmpty(List *list) {
    return list->head == nullptr;
}

void insertElement(List *list, int value) {
    ListElement *newElement = new ListElement{value, nullptr};
    if (isEmpty(list)) {
        list->head = newElement;
    }  else if (list->head->value > value) {
         ListElement *temp = list->head;
         list->head = newElement;
         newElement->next = temp;
    } else {
        ListElement *index = list->head;
        while (index->next != nullptr && index->next->value < value) {
            index = index->next;
        }
        newElement->next = index->next;
        index->next = newElement;
    }
}

bool isEnd(ListElement *temp) {
    return temp == nullptr;
}

void deleteList(List *list) {
    while (list->head) {
        ListElement *temp = list->head;
        list->head = list->head->next;
        delete temp;
    }
    delete list;
}

void deleteElement(List *list, int value) {
    if (list->head != nullptr) {
        if (value == list->head->value) {
            ListElement *temp = list->head;
            list->head = list->head->next;
            delete temp;
        } else {
            ListElement *index = list->head;
            while (index->next != nullptr && index->next->value != value) {
                index = index->next;
            }
            ListElement *temp = index->next;
            index->next = temp->next;
            delete temp;
        }
    }
}

void print(List *list) {
    ListElement *index = list->head;
    while (!isEnd(index)) {
        cout << index->value << endl;
        index = index->next;
    }
}

void eventLoop(List *list) {
    menu();

    int choice = 1;
    while (choice != 0) {
        cout << endl << "¬ведите номер операции: ";
        cin >> choice;
        switch (choice){
            case 0:
            {
                cout << "¬ы вышли из системы";
                deleteList(list);
                break;
            }
            case 1:
            {
                cout << "¬ведите элемент: ";
                int element = 0;
                cin >> element;
                insertElement(list, element);
                cout << "Ёлемент добавлен";
                break;
            }
            case 2:
            {
                cout << "¬ведите удал€емый элемент: ";
                int element = 0;
                cin >> element;
                deleteElement(list, element);
                break;
            }
            case 3:
            {
                cout << "—писок: " << endl;
                print(list);
                break;
            }
            default:
            {
                cout << "ќшибка, повторите ввод!";
                break;
            }
        }
    }
}
