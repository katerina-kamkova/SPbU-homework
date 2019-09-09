#include <iostream>
#include <queue>
#include <cstdlib>

using namespace std;

struct Node {
    int key;
    string str;
    Node* left;
    Node* right;
    Node* parent;
};

struct Tree {
    Node* first;
};

Tree* createTree() {
    return new Tree{};
}

Node *parentForNewNode(Node* top, int key) {
    if (top->key > key && top->left != nullptr) {
        return parentForNewNode(top->left, key);
    } else if (top->key < key && top->right != nullptr) {
        return parentForNewNode(top->right, key);
    }
    return top;
}

void zig(Tree* tree, Node* node) {
    node->parent->left = node->right;
    if (node->right != nullptr) {
        node->right->parent = node->parent;
    }
    node->right = node->parent;
    node->parent = node->parent->parent;
    node->right->parent = node;
    if (node->parent == nullptr) {
        tree->first = node;
    }
}

void zag(Tree* tree, Node* node) {
    node->parent->right = node->left;
    if (node->left != nullptr) {
        node->left->parent = node->parent;
    }
    node->left = node->parent;
    node->parent = node->parent->parent;
    node->left->parent = node;
    if (node->parent == nullptr) {
        tree->first = node;
    }
}

void splay(Tree* tree, Node* node) {
    if (node->parent != nullptr) {
        if (node == node->parent->left) {
            if (node->parent->parent == nullptr) {
                zig(tree, node);
            } else if (node->parent == node->parent->parent->left) {
                zig(tree, node->parent);
                zig(tree, node);
            } else {
                zig(tree, node);
                zag(tree, node);
            }
        } else {
            if (node->parent->parent == nullptr) {
                zag(tree, node);
            } else if (node->parent == node->parent->parent->right) {
                zag(tree, node->parent);
                zag(tree, node);
            } else {
                zag(tree, node);
                zig(tree, node);
            }
        }
        splay(tree, node);
    }
}

void addNode(Tree *tree, int key, const string& str) {
    if (tree->first == nullptr) {
        Node *newNode = new Node{key, str, nullptr, nullptr, nullptr};
        tree->first = newNode;
    } else {
        Node *temp = parentForNewNode(tree->first, key);
        if (temp->key > key) {
            Node *newNode = new Node{key, str, nullptr, nullptr, temp};
            temp->left = newNode;
            splay(tree, newNode);
        } else if (temp->key == key) {
            temp->str = str;
            splay(tree, temp);
        } else {
            Node *newNode = new Node{key, str, nullptr, nullptr, temp};
            temp->right = newNode;
            splay(tree, newNode);
        }
    }
}

Node *findNode(Node *top, int key) {
    if (top == nullptr || top->key == key) {
        return top;
    } else if (top->key > key) {
        return findNode(top->left, key);
    } else {
        return findNode(top->right, key);
    }
}

string getStr(Tree* tree, int key) {
    Node* temp = findNode(tree->first, key);
    if (temp == nullptr) {
        return "";
    } else {
        return temp->str;
    }
}

bool checkPresence(Tree* tree, int key) {
    Node* temp = findNode(tree->first, key);
    if (temp != nullptr) {
        splay(tree, temp);
    }
    return temp;
}

void deleteNode(Tree *tree, int key) {
    Node *delNode = findNode(tree->first, key);
    if (delNode == nullptr) {
        cout << "Error! No such element" << endl;
        return;
    }
    if (delNode->left != nullptr && delNode->right != nullptr) {
        Node* temp = delNode->left;
        while (temp->right != nullptr) {
            temp = temp->right;
        }
        swap(temp->key, delNode->key);
        swap(temp->str, delNode->str);
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
        if (delNode->parent != nullptr) {
            splay(tree, delNode->parent);
        }
    } else if (delNode->left != nullptr) {
        if (delNode->parent == nullptr) {
            tree->first = delNode->left;
            tree->first->parent = nullptr;
        } else {
            if (delNode->parent->left == delNode) {
                delNode->parent->left = delNode->left;
            } else {
                delNode->parent->right = delNode->left;
            }
            delNode->left->parent = delNode->parent;
            splay(tree, delNode->parent);
        }
        delete delNode;
    } else if (delNode->right != nullptr) {
        if (delNode->parent == nullptr) {
            tree->first = delNode->right;
            tree->first->parent = nullptr;
        } else {
            if (delNode->parent->left == delNode) {
                delNode->parent->left = delNode->right;
            } else {
                delNode->parent->right = delNode->right;
            }
            delNode->right->parent = delNode->parent;
            splay(tree, delNode->parent);
        }
       delete delNode;
    } else {
        if (delNode->parent != nullptr) {
            if (delNode->parent->left == delNode) {
                delNode->parent->left = nullptr;
            } else {
                delNode->parent->right = nullptr;
            }
            splay(tree, delNode->parent);
        } else {
            tree->first = nullptr;
        }
        delete delNode;
    }
}

void deleteAllNodes(Node* temp) {
    if (temp->left != nullptr) {
        deleteAllNodes(temp->left);
    }
    if (temp->right != nullptr) {
        deleteAllNodes(temp->right);
    }
    delete temp;
}

void deleteTree(Tree* tree) {
    if (tree->first != nullptr) {
        deleteAllNodes(tree->first);
    }
    delete tree;
}
