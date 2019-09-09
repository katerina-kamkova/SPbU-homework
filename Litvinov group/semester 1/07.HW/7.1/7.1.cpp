#include <iostream>
#include "7.1.h"

using namespace std;

struct Node {
    int value;
    Node *parent;
    Node *left;
    Node *right;
};

struct Tree {
    Node *top;
};

Tree *createTree() {
    return new Tree{};
}

Node *parentForNewNode(Node *top, int value) {
    if (top->value > value && top->left != nullptr) {
        return parentForNewNode(top->left, value);
    } else if (top->value < value && top->right != nullptr) {
        return parentForNewNode(top->right, value);
    }
    return top;
}

void addNode(Tree *tree, int value) {
    if (tree->top == nullptr) {
        Node *newNode = new Node{value, nullptr, nullptr, nullptr};
        tree->top = newNode;
        return;
    }

    Node *temp = parentForNewNode(tree->top, value);
    if (temp->value > value) {
        Node *newNode = new Node{value, temp, nullptr, nullptr};
        temp->left = newNode;
    } else if (temp->value == value) {
        return;
    } else {
        Node *newNode = new Node{value, temp, nullptr, nullptr};
        temp->right = newNode;
    }
}

Node *findNode(Node *top, int value) {
    if (top == nullptr || top->value == value) {
        return top;
    } else if (top->value > value) {
        return findNode(top->left, value);
    } else {
        return findNode(top->right, value);
    }
}

void deleteNode(Tree *tree, int value) {
    Node *delNode = findNode(tree->top, value);
    if (delNode == nullptr) {
        cout << "Error! No such element";
        return;
    }
    if (delNode->left != nullptr && delNode->right != nullptr) {
        Node* temp = delNode->left;
        while (temp->right != nullptr) {
            temp = temp->right;
        }
        swap(temp->value, delNode->value);
        if (temp->parent->right == temp) {
            if (temp->left != nullptr) {
                temp->parent->right = temp->left;
            } else {
                temp->parent->right = nullptr;
            }
        } else {
            if (temp->left != nullptr) {
                temp->parent->left = temp->left;
            } else {
                temp->parent->left = nullptr;
            }
        }
        delete temp;
    } else if (delNode->left != nullptr) {
        if (delNode->parent == nullptr) {
            tree->top = delNode->left;
            tree->top->parent = nullptr;
        } else {
            if (delNode->parent->left == delNode) {
                delNode->parent->left = delNode->left;
            } else {
                delNode->parent->right = delNode->left;
            }
            delNode->left->parent = delNode->parent;
        }
        delete delNode;
    } else if (delNode->right != nullptr) {
        if (delNode->parent == nullptr) {
            tree->top = delNode->right;
            tree->top->parent = nullptr;
        } else {
            if (delNode->parent->left == delNode) {
                delNode->parent->left = delNode->right;
            } else {
                delNode->parent->right = delNode->right;
            }
            delNode->right->parent = delNode->parent;
        }
        delete delNode;
    } else {
        if (delNode->parent != nullptr) {
            if (delNode->parent->left == delNode) {
                delNode->parent->left = nullptr;
            } else {
                delNode->parent->right = nullptr;
            }
        } else {
            tree->top = nullptr;
        }
        delete delNode;
    }
}

bool checkPresence(Tree* tree, int value) {
    Node* top = tree->top;
    return findNode(top, value) != nullptr;
}

void up(Node *top) {
    if (top->left != nullptr) {
        up(top->left);
    }
    cout << top->value << ' ';
    if (top->right != nullptr) {
        up(top->right);
    }
}

void printUp(Tree* tree) {
    up(tree->top);
}

void down(Node *top) {
    if (top->right != nullptr) {
        down(top->right);
    }
    cout << top->value << ' ';
    if (top->left != nullptr) {
        down(top->left);
    }
}

void printDown(Tree* tree) {
    down(tree->top);
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

void deleteTree(Tree *tree) {
    if (tree->top != nullptr) {
        deleteAllNodes(tree->top);
    }
    delete tree;
}

bool isEmpty(Tree* tree) {
    return tree->top == nullptr;
}
