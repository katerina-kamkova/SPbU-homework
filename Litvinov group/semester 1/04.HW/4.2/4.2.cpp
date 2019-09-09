#include "4.2.h"

#include <iostream>
#include <fstream>

using namespace std;

vector<int> createVector() {
    ifstream fin;
    fin.open("input.txt");

    vector<int> myArray;
    if (fin.is_open()) {
        while (!fin.eof()) {
            int temp = 0;
            fin >> temp;
            myArray.push_back(temp);
        }
    } else {
        cout << "Error! File isn`t found";
    }
    fin.close();
    return myArray;
}

void print(vector<int>& myArray) {
    int n = myArray.size();
    for (int i = 0; i < n; ++i) {
        cout << myArray[i] << " ";
    }
}

int mostFrequent(const vector<int>& myArray) {
    int n = myArray.size();
    int answer = 0;
    int answerCounter = 0;
    int current = myArray[0];
    int currentCounter = 1;
    for (int i = 1; i < n; ++i) {
        if (myArray[i] == current) {
            ++currentCounter;
        } else {
            if (currentCounter > answerCounter) {
                answerCounter = currentCounter;
                answer = current;
            }
            currentCounter = 1;
            current = myArray[i];
        }
    }
    if (currentCounter > answerCounter) {
        answer = current;
    }
    return answer;
}
