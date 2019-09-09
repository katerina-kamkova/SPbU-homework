#include "Test-3.1.h"

#include <iostream>
#include <ctime>
#include <cstdlib>

using namespace std;

int main()
{
    srand(time(nullptr));

    int length = 20;
    int array[length];

    cout << "Array: ";
    for (int i = 0; i < length; ++i) {
        array[i] = 1 + rand() % 100;
        cout << array[i] << " ";
    }
    cout << endl;

    int maxSum = 0;
    Stack* answer = createStack();
    for (int i = 0; i < length; ++i) {
        int tempSum = 0;
        int temp = array[i];
        while (temp != 0) {
            tempSum += temp % 10;
            temp /= 10;
        }

        if (tempSum > maxSum) {
            while (!isEmpty(answer)) {
                int unnescessary = pop(answer);
            }
            maxSum = tempSum;
            push(answer, array[i]);
        } else if (tempSum == maxSum) {
            push(answer, array[i]);
        }
    }

    cout << "The elements with max sum of figures: ";
    while (!isEmpty(answer)) {
        cout << pop(answer) << ' ';
    }
    cout << endl;

    deleteStack(answer);

    return 0;
}
