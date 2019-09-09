#include "6.1.h"
#include "stack.h"

#include <iostream>
#include <locale.h>

using namespace std;

int main()
{
    setlocale(LC_CTYPE, "Russian");

    int n = 0;
    cout << "Введите количество элементов: ";
    cin >> n;

    Stack *stack = createStack();

    cout << "Введите элементы: ";
    for (int i = 0; i < n; i ++) {
        string symbol = "";
        cin >> symbol;
        definer(stack, symbol);
    }

    cout << endl << "Ответ: " << pop(stack) << endl;

    deleteStack(stack);

    return 0;
}
