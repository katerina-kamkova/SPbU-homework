#include <iostream>
#include <fstream>
#include <cmath>

using namespace std;

struct Vertex {
    int number;
    Vertex* parent;
};

struct Edge {
    int value;
    int first;
    int second;
    Edge* next;
};

int definer(const string& symbol) {
    int value = 0;
    int length = symbol.size();
    for (int i = length; i > 0; --i) {
        value += (symbol[length - i] - '0') * pow(10, i - 1);
    }
    return value;
}

int matrixSize() {
    ifstream fin;
    fin.open("input.txt");

    string symbol = "";
    if (fin.is_open()) {
        fin >> symbol;
    }

    fin.close();
    return definer(symbol);
}

void addEdge(Edge* firstE, int value, int first, int second) {
    Edge* newEdge = new Edge{value, first, second, nullptr};
    while (firstE->next != nullptr && firstE->next->value <= value) {
        firstE = firstE->next;
    }
    newEdge->next = firstE->next;
    firstE->next = newEdge;
}

Edge* createEList(int size) {
    ifstream fin;
    fin.open("input.txt");

    Edge* newE = new Edge{};
    if (!fin.is_open()) {
        cout << "Error! File isn`t found";
        return nullptr;
    } else {
        int unnecessary = 0;
        fin >> unnecessary;
        for (int i = 0; i < size; ++i) {
            for (int j = 0; j < size; ++j) {
                string symbol = "";
                fin >> symbol;
                int value = definer(symbol);
                if (i < j && value != 0) {
                    addEdge(newE, value, i, j);
                }
            }
        }
    }

    fin.close();
    Edge* temp = newE->next;
    delete newE;
    return temp;
}

int findParent(Vertex* vertexes, int number) {
    while (vertexes[number].parent != nullptr) {
        number = vertexes[number].parent->number;
    }
    return number;
}

void eventLoop(Edge* firstE, int size) {
    Vertex* vertexes = new Vertex[size];
    for (int i = 0; i < size; ++i) {
        vertexes[i].number = i;
        vertexes[i].parent = nullptr;
    }
    vertexes[firstE->second].parent = &vertexes[firstE->first];

    while (firstE != nullptr && firstE->next != nullptr) {
        int firstV = findParent(vertexes, firstE->next->first);
        int secondV = findParent(vertexes, firstE->next->second);
        if (firstV == secondV) {
            Edge* temp = firstE->next;
            firstE->next = temp->next;
            delete temp;
        } else {
            vertexes[secondV].parent = &vertexes[firstV];
            firstE = firstE->next;
        }
    }

    delete[] vertexes;
}

void print(Edge* first) {
    cout << "Edges of the tree (value 1vertex 2vertex): " << endl;
    while (first != nullptr) {
        cout << first->value << " " << first->first << " " << first->second << endl;
        first = first->next;
    }
}

void deleteList(Edge* first) {
    while (first->next != nullptr) {
        Edge* temp = first->next;
        first->next = temp->next;
        delete temp;
    }
    delete first;
}
