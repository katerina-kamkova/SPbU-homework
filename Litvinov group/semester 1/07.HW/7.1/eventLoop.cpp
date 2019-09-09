#include "eventLoop.h"

#include <iostream>

using namespace std;

void menu() {
    cout << "Choose what do you want to do with the tree:" << endl;
    cout << "0 - to exit the program;" << endl;
    cout << "1 - to add the element;" << endl;
    cout << "2 - to delete the element;" << endl;
    cout << "3 - to check, whether the element is in the tree;" << endl;
    cout << "4 - to print the tree in ascending order;" << endl;
    cout << "5 - to print the tree in descending order;" << endl;
}

void eventLoop(Tree *tree) {
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
                cout << "Enter the element: ";
                int value = 0;
                cin >> value;
                addNode(tree, value);
                break;
            }
            case 2:
            {
                cout << "Enter the element: ";
                int value = 0;
                cin >> value;
                deleteNode(tree, value);
                break;
            }
            case 3:
            {
                cout << "Enter the element: ";
                int value = 0;
                cin >> value;
                if (checkPresence(tree, value)) {
                    cout << "There is such element in the tree" << endl;
                } else {
                    cout << "There is no such element in the tree" << endl;
                }
                break;
            }
            case 4:
            {
                cout << "The tree in ascending order: ";
                if (isEmpty(tree)) {
                    cout << "Empty";
                } else {
                    printUp(tree);
                }
                cout << endl;
                break;
            }
            case 5:
            {
                cout << "The tree in descending order: ";
                if (isEmpty(tree)) {
                    cout << "Empty";
                } else {
                    printDown(tree);
                }
                cout << endl;
                break;
            }
            default:
            {
                cout << "Error! Enter your choice again" << endl;
                break;
            }
        }
    }
}
