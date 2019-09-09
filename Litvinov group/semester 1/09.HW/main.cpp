#include "hashTable.h"

#include <iostream>
#include <fstream>

using namespace std;

void input(HashTable* table) {
    ifstream fin;
    fin.open("input.txt");

    if (fin.is_open()) {
        while (!fin.eof()) {
            string str = "";
            fin >> str;
            addString(table, str);
        }
    } else {
        cout << "Error! There`s no input.txt";
    }
}

int main()
{
    HashTable* table = createHashTable();

    input(table);
    print(table);
    calculateLength(table);

    deleteHashTable(table);

    return 0;
}
