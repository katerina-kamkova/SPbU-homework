#include <iostream>
#include <string>
#include <fstream>

using namespace std;

int status(char symbol) {
    if (symbol == '/') {
        return 0;
    } else if (symbol == '*') {
        return 1;
    } else {
        return 2;
    }
}

string input(int**& table, int& n, int& m) {
    ifstream fin;
    fin.open("input.txt");

    string str = "";
    if (!fin.is_open()) {
        cout <<"Error! File isn`t found";
    } else {
        fin >> n >> m;
        table = new int*[n];
        for (int i = 0; i < n; ++i) {
            table[i] = new int[m];
            for (int j = 0; j < m; ++j) {
                fin >> table[i][j];
            }
        }

        while (!fin.eof()) {
            fin >> str;
        }
        fin.close();
    }

    return str;
}

int main()
{
    int n = 0;
    int m = 0;
    int** table = nullptr;
    string str = input(table, n, m);
    int i = 0;
    int state = 0;

    while (str[i] != '\0') {
        if (state == 1 && str[i] == '*') {
            cout << "/*";
        } else if (state == 2 || state == 3) {
            cout << str[i];
        }
        state = table[state][status(str[i])];
        ++i;
    }

    return 0;
}
