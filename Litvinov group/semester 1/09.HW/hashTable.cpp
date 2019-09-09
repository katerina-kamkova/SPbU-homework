#include "hashTable.h"

#include <iostream>
#include <cmath>

using namespace std;

const int lengthOfTable = 100;
const int primeNumber = 13;

struct HashTable {
    List* tableOfLists[lengthOfTable];
};

HashTable* createHashTable() {
    HashTable* newTable = new HashTable{};
    for (int i = 0; i < lengthOfTable; i ++) {
        newTable->tableOfLists[i] = createList();
    }
    return newTable;
}

int countNumber(const string& str) {
    long long answer = 0;
    for (int i = 0; i < str.size(); ++i) {
        answer += str[i] * pow(primeNumber, i);
    }
    return answer % lengthOfTable;
}

void addString(HashTable* table, const string& str) {
    int number = countNumber(str);
    addElement(table->tableOfLists[number], str);
}

void print(HashTable* table) {
    cout << "All words mentioned in the text(word, amount): " << endl;
    for (int i = 0; i < lengthOfTable; ++i) {
        if (table->tableOfLists[i] != nullptr) {
            printList(table->tableOfLists[i]);
        }
    }
}

void calculateLength(HashTable* table) {
    int maximum = 0;
    int average = 0;
    int counter = 0;
    for (int i = 0; i < lengthOfTable; i ++) {
        int length = lengthOfList(table->tableOfLists[i]);
        if (length > maximum) {
            maximum = length;
        }
        average += length;
        if (length != 0) {
            ++counter;
        }
    }
    cout << "Maximal length of the list is: " << maximum << endl;
    cout << "Average length of the full list is: " << average / counter << endl;
    cout << "Average length of the list is: " << average / lengthOfTable << endl;
}

void deleteHashTable(HashTable* table) {
    for (int i = 0; i < lengthOfTable; ++i) {
        deleteList(table->tableOfLists[i]);
    }
    delete table;
}
