#include <iostream>
#include <fstream>

using namespace std;

struct Contact {
    string name;
    string number;
    Contact *next;
};

struct Notebook {
    Contact *firstContact;
};

bool menu() {
    cout << "Choose the type of sorting:" << endl;
    cout << "1 - by name" << endl;
    cout << "0 - by number" << endl;
    cout << "Enter your decision: ";
    bool answer = false;
    cin >> answer;
    return answer;
}

Notebook *createNotebook() {
    return new Notebook{};
}

void addNote(const string& name, const string& number, Notebook *notebook) {
    Contact *newContact = new Contact{name, number, notebook->firstContact};
    notebook->firstContact = newContact;
}

void input(Notebook *notebook) {
    ifstream fin;
    fin.open("input.txt");

    if (!fin.is_open()) {
        cout << "Error! File isn`t found";
        return;
    } else {
        while (!fin.eof()) {
            string name = "";
            string number = "";
            fin >> name >> number;
            addNote(name, number, notebook);
        }
    }

    fin.close();
}

void deleteNotebook(Notebook *notebook) {
    while (notebook->firstContact != nullptr) {
        Contact *temp = notebook->firstContact;
        notebook->firstContact = temp->next;
        delete temp;
    }
    delete notebook;
}

void print(Notebook *notebook) {
    Contact *index = notebook->firstContact;
    while (index != nullptr) {
        cout << index->name << " " << index->number << endl;
        index = index->next;
    }
}

void changeNameNumbers(Notebook *notebook) {
    Contact *temp = notebook->firstContact;
    while(temp != nullptr) {
        swap(temp->name, temp->number);
        temp = temp->next;
    }
}

int length(Notebook *notebook) {
    Contact *temp = notebook->firstContact;
    int answer = 0;
    while (temp != nullptr) {
        ++answer;
        temp = temp->next;
    }
    return answer;
}

Contact *nextToN(Contact *contact, int n) {
    if (contact->next != nullptr) {
        for (int i = 0; i < n; ++i) {
            contact = contact->next;
        }
    }
    return contact;
}

Contact *first(Notebook *notebook) {
    return notebook->firstContact;
}

void realMergeSort(Notebook *notebook, Contact *first, int length, Contact* previous, Contact* next) {
    Contact *second = nextToN(first, length / 2);
    if (length > 1) {
        realMergeSort(notebook, first, length / 2, previous, second);
        realMergeSort(notebook, second, length - length / 2, nextToN(first, length / 2 - 1), next);
    } else {
        return;
    }

    Contact* middle = second;
    Contact* newList = new Contact{};
    Contact* temp = newList;
    while (first != middle || second != nullptr) {
        if ((second == nullptr || first->name < second->name) && first != middle) {
            temp->next = first;
            temp = first;
            first = first->next;
        } else {
            temp->next = second;
            temp = second;
            second = second->next;
        }
    }

    if (previous == nullptr) {
        notebook->firstContact = newList->next;
    } else {
        previous->next = newList->next;
    }
    temp->next = next;
    delete newList;
}

void mergeSort(Notebook *notebook, bool choice) {
    if (choice) {
        realMergeSort(notebook, first(notebook), length(notebook), nullptr, nullptr);
    } else {
        changeNameNumbers(notebook);
        realMergeSort(notebook, first(notebook), length(notebook), nullptr, nullptr);
        changeNameNumbers(notebook);
    }
}
