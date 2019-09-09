#include "5.2.h"

#include <iostream>

using namespace std;

struct Position {
    int number;
    Position *left;
    Position *right;
};

Position* createPosition() {
    return new Position{1, nullptr, nullptr};
}

void createCircle(Position *first, int amount) {
    first->left = first;
    first->right = first;
    for (int i = 2; i <= amount; ++i) {
        Position *newPosition = new Position{i, first->left, first};
        first->left->right = newPosition;
        first->left = newPosition;
    }
}

int deleteAllButOne(Position *last, int amount, int period){
    Position *temp = last;
    while (temp->left != temp) {
        for (int i = 1; i < period; ++i) {
            temp = temp->right;
        }
        Position *buffer = temp->right;
        temp->right = buffer->right;
        temp->right->left = temp;
        delete buffer;
    }
    int answer = temp->number;
    delete temp;
    return answer;
}

Position* last(Position *first) {
    return first->left;
}
