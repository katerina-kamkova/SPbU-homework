#include <iostream>
#include <fstream>

using namespace std;

struct City {
    int number;
    City* next;
};

struct Road {
    int length;
    int secondCity;
    Road* next;
};

struct Head {
    Road* first;
};

bool* input(int& n, int& k, int**& matrix, City*& states) {
    ifstream fin;
    fin.open("input.txt");

    bool* allCities = nullptr;
    if (!fin.is_open()) {
        cout << "Error! File input.txt isn`t found";
        return allCities;
    } else {
        fin >> n;
        allCities = new bool[n]{};

        matrix = new int*[n];
        for (int i = 0; i < n; ++i) {
            matrix[i] = new int[n];
            for (int j = 0; j < n; ++j) {
                matrix[i][j] = 0;
            }
        }

        int m = 0;
        fin >> m;
        while (m != 0) {
            int i = 0;
            int j = 0;
            fin >> i >> j;
            fin >> matrix[i][j];
            matrix[j][i] = matrix[i][j];
            --m;
        }

        fin >> k;
        states = new City[k]{};
        for (int i = 0; i < k; ++i) {
            int number = 0;
            fin >> number;
            states[i].number = number;
            allCities[number] = true;
        }

        fin.close();
        return allCities;
    }
}

void add(Road& roads, int length, int secondCity) {
    Road* temp = &roads;
    if (temp->length == 0) {
        temp->length = length;
        temp->secondCity = secondCity;
    } else if (length < temp->length) {
        temp->next = new Road{temp->length, temp->secondCity, temp->next};
        temp->length = length;
        temp->secondCity = secondCity;
    } else {
        while (temp->next != nullptr && temp->next->length < length) {
            temp = temp->next;
        }
        temp->next = new Road{length, secondCity, temp->next};
    }
}

void eventLoop(int n, int k, int** matrix, City* states, bool* allCities) {
    Road* roads = new Road[k]{};
    int freeCities = n - k;
    bool* fullStates = new bool[k]{};

    while (freeCities != 0) {
        for (int i = 0; i < k; ++i) {
            if (!fullStates[i]) {
                City* temp = &states[i];
                while (temp->next != nullptr) {
                    temp = temp->next;
                }
                int coords = temp->number;

                for (int j = 0; j < n; ++j) {
                    if (matrix[coords][j] != 0 && !allCities[j]) {
                        add(roads[i], matrix[coords][j], j);
                    }
                }

                Road* shortest = &roads[i];
                while (shortest != nullptr && allCities[shortest->secondCity]) {
                    shortest = shortest->next;
                }

                if (shortest == nullptr || shortest->length == 0) {
                    fullStates[i] = true;
                } else {
                    --freeCities;
                    allCities[shortest->secondCity] = true;
                    temp->next = new City{shortest->secondCity, nullptr};
                }
            }
        }
    }

    delete[] fullStates;

    for (int i = 0; i < k; ++i) {
        Road* temp = &roads[i];
        while (temp != nullptr) {
            Road* del = temp;
            temp = temp->next;
            delete del;
        }
    }
}

void printResults(int k, City* states) {
    for (int i = 0; i < k; ++i) {
        cout << "Cities of the state number " << i << " :";
        City* temp = &states[i];
        while (temp != nullptr) {
            cout << temp->number << ' ';
            temp = temp->next;
        }
        cout << endl;
    }
}

void freeMemory(int n, int k, int** matrix, City* states, bool* allCities) {
    for (int i = 0; i < n; ++i) {
        delete[] matrix[i];
    }

    for (int i = 0; i < k; ++i) {
        City* temp = &states[i];
        while (temp != nullptr) {
            City* del = temp;
            temp = temp->next;
            delete del;
        }
    }

    delete[] allCities;
}
