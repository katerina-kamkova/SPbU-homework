#include "7.2.h"

#include <iostream>
#include <fstream>
#include <cmath>

using namespace std;

struct Node {
    string value;
    Node* left;
    Node* right;
};

struct Head {
    Node* top;
};

Head* createHead() {
    return new Head{};
}

void amountOfSymbols(const string& str, vector<string>& expression) {
    string first = "";
    string second = "";
    bool which = true;
    for (int i = 0; i < str.size(); ++i) {
        if (str[i] == '(' || str[i] == ')') {
            which = false;
        } else {
            if (which) {
                first += str[i];
            } else {
                second += str[i];
            }
        }
    }

    if (first.size() > 0) {
        expression.insert(expression.end(), first);
    }
    if (second.size() > 0) {
        expression.insert(expression.end(), second);
    }
}

void input(vector<string>& expression) {
    ifstream fin;
    fin.open("input.txt");

    if (fin.is_open()) {
        while (!fin.eof()) {
            string str = "";
            fin >> str;
            amountOfSymbols(str, expression);
        }
    }

    fin.close();
}

void addNode(vector<string>& exp, Node* temp, int& number) {
    Node* newLeft = new Node{exp[number], nullptr, nullptr};
    temp->left = newLeft;
    ++number;
    if (exp[number - 1] == "*" || exp[number - 1] == "+" || exp[number - 1] == "-" || exp[number - 1] == "/") {
        addNode(exp, temp->left, number);
    }

    Node* newRight = new Node{exp[number], nullptr, nullptr};
    temp->right = newRight;
    ++number;
    if (exp[number - 1] == "*" || exp[number - 1] == "+" || exp[number - 1] == "-" || exp[number - 1] == "/") {
        addNode(exp, temp->right, number);
    }
}

void makeTree(Head* head, vector<string>& exp) {
    Node* newNode = new Node{exp[0], nullptr, nullptr};
    head->top = newNode;
    Node* temp = newNode;
    int number = 1;
    addNode(exp, temp, number);
}

void realPrint(Node* temp) {
    if (temp->left != nullptr) {
        cout << '(';
        realPrint(temp->left);
    }

    cout << temp->value;

    if (temp->right != nullptr) {
        realPrint(temp->right);
        cout << ')';
    }
}

void print(Head* head) {
    realPrint(head->top);
}

int definer(const string& symbol) {
    int value = 0;
    int length = symbol.size();
    for (int i = length; i > 0; --i) {
        value += (symbol[length - i] - '0') * pow(10, i - 1);
    }
    return value;
}

int realCount(Node* temp) {
    if (temp->value == "*" || temp->value == "+" || temp->value == "-" || temp->value == "/") {
        int first = 0;
        if (temp->left != nullptr) {
            first = realCount(temp->left);
        }

        int second = 0;
        if (temp->right != nullptr) {
            second = realCount(temp->right);
        }

        if (temp->value == "*") {
            return first * second;
        } else if (temp->value == "+") {
            return first + second;
        } else if (temp->value == "-") {
            return first - second;
        } else {
            return first / second;
        }
    } else {
        return definer(temp->value);
    }
}

int countExp(Head* head) {
    return realCount(head->top);
}

void deleteAllNodes(Node *top) {
    if (top->left != nullptr) {
        deleteAllNodes(top->left);
    }
    if (top->right != nullptr) {
        deleteAllNodes(top->right);
    }
    delete top;
}

void deleteTree(Head *tree) {
    deleteAllNodes(tree->top);
    delete tree;
}
