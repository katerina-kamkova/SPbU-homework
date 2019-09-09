#include <string>
#include <fstream>
#include <iostream>

using namespace std;

struct Contact {
    string name;
    string number;
    Contact *next;
};

struct Notebook {
    Contact *firstRecord;
};

Notebook* createNotebook() {
    return new Notebook{};
}

void addNote(const string& name, const string& number, Notebook *notebook) {
    Contact *newContact = new Contact{name, number, notebook->firstRecord};
    notebook->firstRecord = newContact;
}

void input(Notebook *notebook) {
    ifstream fin;
    fin.open("input.txt");

    if (fin.is_open()) {
        while (!fin.eof()) {
            string name = "";
            string number = "";
            fin >> name >> number;
            addNote(name, number, notebook);
        }
    }

    fin.close();
}

void menu() {
    cout << "Доступные операции:" << endl;
    cout << "0 - выйти;" << endl;
    cout << "1 - добавить запись (имя и телефон);" << endl;
    cout << "2 - распечатать все имеющиеся записи;" << endl;
    cout << "3 - найти телефон по имени;" << endl;
    cout << "4 - найти имя по телефону;" << endl;
    cout << "5 - сохранить текущие данные в файл;";
}

void print(Notebook *notebook) {
    Contact *temp = notebook->firstRecord;
    while (temp != nullptr) {
        cout << temp->name << "   " << temp->number << endl;
        temp = temp->next;
    }
}

string findNumber(const string& name, Notebook *notebook) {
    Contact *temp = notebook->firstRecord;
    while (temp != nullptr) {
        if (temp->name == name) {
            return temp->number;
        } else {
            temp = temp->next;
        }
    }
    return "";
}

string findName(const string& number, Notebook *notebook) {
    Contact *temp = notebook->firstRecord;
    while (temp != nullptr) {
        if (temp->number == number) {
            return temp->name;
        } else {
            temp = temp->next;
        }
    }
    return "";
}

void save(Notebook *notebook) {
    if (notebook->firstRecord != nullptr) {
        ofstream fout;
        fout.open("input.txt");

        Contact *temp = notebook->firstRecord;
        while (temp != nullptr) {
            fout << temp->name << " " << temp->number << endl;
            temp = temp->next;
        }
        fout.close();
    }
}

void deleteContacts(Notebook *notebook) {
    Contact *temp = notebook->firstRecord;
    while (temp != nullptr) {
        Contact *buffer = temp;
        temp = temp->next;
        delete buffer;
    }
    delete notebook;
}

void eventLoop(Notebook *notebook) {
    int choice = 1;
    while (choice != 0) {
        cout << endl << endl << "Введите номер операции: ";
        cin >> choice;
        switch (choice){
            case 0:
            {
                break;
            }
            case 1:
            {
                string name = "0";
                string number = "0";
                cout << "Введите имя и номер: ";
                cin >> name >> number;
                addNote(name, number, notebook);
                cout << "Создан новый контакт";
                break;
            }
            case 2:
            {
                cout << "Все контакты: " << endl;
                print(notebook);
                break;
            }
            case 3:
            {
                string name = "0";
                string number = "0";
                cout << "Введите имя: ";
                cin >> name;
                number = findNumber(name, notebook);
                if (number == "") {
                    cout << "Номер не найден";
                } else {
                    cout << "Искомый номер: " << number;
                }
                break;
            }
            case 4:
            {
                string name = "0";
                string number = "0";
                cout << "Введите номер: ";
                cin >> number;
                name = findName(number, notebook);
                if (name == "") {
                    cout << "Имя не найден";
                } else {
                    cout << "Искомое имя: " << name;
                }
                break;
            }
            case 5:
            {
                save(notebook);
                cout << "Все записи сохранены";
                break;
            }
            default:
            {
                cout << "Ошибка, повторите ввод: ";
                break;
            }
        }
    }
}
