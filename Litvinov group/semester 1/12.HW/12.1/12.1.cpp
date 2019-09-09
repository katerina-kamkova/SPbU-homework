#include <iostream>
#include <string>
#include <fstream>

using namespace std;

string input() {
    ifstream fin;
    fin.open("input.txt");

    string str = "";
    if (fin.is_open()) {
        while (!fin.eof()) {
            fin >> str;
        }
    }

    fin.close();
    return str;
}

int* prefixFunction(const std::string& str, const std::string& temp) {
    string newStr = temp + "#" + str;

    int length = str.size() + temp.size() + 1;
    int* array = new int[length] {};
    array[0] = 0;

    int counter = 0;
    for (int i = 1; i < length; ++i) {
        while (newStr[i] != newStr[counter] && counter != 0) {
            counter = array[counter - 1];
        }
        if (newStr[i] == newStr[counter]) {
            ++counter;
            array[i] = counter;
        } else {
            array[i] = 0;
        }
    }

    return array;
}

int findFirstEnter(int* array, int length, int n) {
    for (int i = n + 1; i < length; ++i) {
        if (array[i] == n) {
            return i - 2 * n + 1;
        }
    }
    return -1;
}

