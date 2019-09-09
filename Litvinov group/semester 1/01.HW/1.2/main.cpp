#include <iostream>
#include <cmath>

using namespace std;

int main()
{
    int number, del, answer = 0, answer_sign = 1;
    cin >> number >> del;

    if (number < 0)
    {
        number *= -1;
        if (del < 0)
            del *= -1;
        else
            answer_sign = -1;
    }
    else
        if (del < 0)
        {
            answer_sign = -1;
            del *= -1;
        }

    while (number >= del)
    {
        answer ++;
        number -= del;
    }

    cout << answer*answer_sign << endl;
    return 0;
}
