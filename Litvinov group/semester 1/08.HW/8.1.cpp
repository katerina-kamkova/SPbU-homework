#include "8.1.h"

#include <iostream>
#include <string>

using namespace std;

void menu() {
    cout << "Available options:" << endl;
    cout << "0 - to exit the program" << endl;
    cout << "1 - to add the element" << endl;
    cout << "2 - to get the meaning by the key" << endl;
    cout << "3 - to check, whether such element exists" << endl;
    cout << "4 - to delete the element" << endl;
}

void eventLoop(Tree* tree) {
    menu();
    int choice = -1;
    while (choice != 0) {
        cout << endl;
        cout << "Enter your choice: ";
        cin >> choice;
        switch (choice) {
        case 0:
        {
            cout << "You`ve left the program" << endl;
            break;
        }
        case 1:
        {
            cout << "Enter the key: ";
            int key = 0;
            cin >> key;
            cout << "Enter the string: ";
            string str = "";
            cin >> str;
            addNode(tree, key, str);
            cout << endl;
            break;
        }
        case 2:
        {
            cout << "Enter thee key: ";
            int key = 0;
            cin >> key;
            cout << "The string: " << getStr(tree, key) << endl;
            break;
        }
        case 3:
        {
            cout << "Enter the key: ";
            int key = 0;
            cin >> key;
            if (checkPresence(tree, key)) {
                cout << "There is such element in the tree" << endl;
            } else {
                cout << "There is no such element in the string" << endl;
            }
            break;
        }
        case 4:
        {
            cout << "Enter the key: ";
            int key = 0;
            cin >> key;
            deleteNode(tree, key);
            break;
        }
        default:
        {
            cout << "Error!" << endl;
            break;
        }
        }
    }
}
