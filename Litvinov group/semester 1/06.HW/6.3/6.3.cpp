#include "6.3.h"

#include <iostream>
#include <string>
#include <fstream>
#include <queue>
#include <cmath>
#include <locale.h>

using namespace std;

void definer(queue<string>& output, Stack *stack, const string& symbol) {
    char first = symbol[0];
    char last = symbol[symbol.size() - 1];

    if (first == '(') {
        push(stack, "(");
        string newStr = "";

        for (int i = 1; i < symbol.size(); ++i) {
            newStr += symbol[i];
        }
        output.push(newStr);
    } else if (last == ')') {
        string newStr = "";
        for (int i = 0; i < symbol.size() - 1; ++i) {
            newStr += symbol[i];
        }
        output.push(newStr);

        string str = pop(stack);
        while (str != "(") {
            output.push(str);
            str = pop(stack);
        }
    } else if (isdigit(first)){
        output.push(symbol);
    } else {
        if (symbol == "*" || symbol == "/") {
            if (!isEmpty(stack)) {
                string temp = pop(stack);
                if (temp == "+" || temp == "-") {
                    push(stack, temp);
                } else if (temp != "(") {
                    output.push(temp);
                }
            }
            push(stack, symbol);
        } else {
            if (!isEmpty(stack)) {
                string temp = pop(stack);
                if (temp != "(") {
                    output.push(temp);
                } else {
                    push(stack, temp);
                }
            }
            push(stack, symbol);
        }
    }
}

queue<string> eventLoop(Stack *stack) {
    ifstream fin("input.txt");

    queue<string> output;
    if (!fin.is_open()) {
        cout << "Error! File isn`t found";
        return output;
    }
    while (!fin.eof()) {
        string str = "";
        fin >> str;
        definer(output, stack, str);
    }
    while (!isEmpty(stack)) {
        output.push(pop(stack));
    }

    fin.close();
    return output;
}

void print(queue<std::string>& output) {
    cout << "The expression in postfix form: ";
    while (output.size() != 0) {
        cout << output.front() << ' ';
        output.pop();
    }
    cout << endl;
}
