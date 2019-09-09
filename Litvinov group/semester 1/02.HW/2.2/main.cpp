#include <iostream>
#include <locale.h>

using namespace std;

int headOn(int number, int degree) {
    int num = number;
    while (degree > 1) {
        num *= number;
        --degree;
    }
    return num;
}

int betterWay(int number, int degree) {
    if (degree == 1) {
        return number;
    }
    if (degree % 2 == 0) {
        return betterWay(number * number, degree / 2);
    }
    return number * betterWay(number * number, (degree - 1) / 2);
}

int main()
{
    setlocale(LC_CTYPE, "Russian");

    cout << "Введите число, возводимое в степень: ";
    int number = 0;
    cin >> number;

    cout << endl << "Введите степень: ";
    int degree = 0;
    cin >> degree;

    cout << endl << "Каким способом возвести в степень: 'в лоб' - 1 или за O(log n) - 2 : ";
    int choice = 0;
    cin >> choice;

    if (choice == 1) {
        cout << endl << endl << "Ответ: " << headOn(number, degree) << endl;
    } else {
        cout << endl << endl << "Ответ: " << betterWay(number, degree) << endl;
    }

    return 0;
}
