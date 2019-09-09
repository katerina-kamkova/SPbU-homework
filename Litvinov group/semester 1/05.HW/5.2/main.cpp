#include "5.2.h"

#include <iostream>
#include <locale.h>

using namespace std;

int main()
{
    setlocale(LC_CTYPE, "Russian");

    cout << "Введите n - количество воинов: ";
    int n = 0;
    cin >> n;

    cout << "Введите m - номер воина, которого убьют следующим: ";
    int m = 0;
    cin >> m;

    Position *first = createPosition();

    createCircle(first, n);

    cout << endl << "Выживет воин на позиции: " << deleteAllButOne(last(first), n, m);

    return 0;
}
