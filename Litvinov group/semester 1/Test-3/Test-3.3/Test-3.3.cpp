#include <iostream>
#include <fstream>
#include <queue>

using namespace std;

struct Vertex {
    int number;
    bool checked;
};

int** createArray(int& length) {
    ifstream fin;
    fin.open("input.txt");

    if (fin.is_open()) {
        while (!fin.eof()) {
            fin >> length;
            int** array = new int*[length];
            for (int i = 0; i < length; ++i) {
                array[i] = new int[length];
                for (int j = 0; j < length; ++j) {
                    fin >> array[i][j];
                }
            }
            fin.close();
            return array;
        }
    } else {
        cout << "Error! There`s no input.txt";
        return nullptr;
    }
}

Vertex* createVert(int length) {
    Vertex* vert = new Vertex[length];
    for (int i = 0; i < length; ++i) {
        vert[i].number = i;
        vert[i].checked = false;
    }
    return vert;
}

void printVertexes(int** array, int length, Vertex* vert) {
    queue<int> check;
    check.push(vert[0].number);
    vert[0].checked = true;
    while (check.size() != 0) {
        int coords = check.front();
        for (int i = 0; i < length; ++i) {
            if (array[coords][i] == 1 && !vert[i].checked) {
                check.push(i);
                vert[i].checked = true;
            }
        }
        cout << check.front() << ' ';
        check.pop();
    }
}

void deleteArray(int** array, int length) {
    for (int i = 0; i < length; ++i) {
        delete[] array[i];
    }
    delete[] array;
}

void deleteVert(Vertex* vert) {
    delete[] vert;
}
