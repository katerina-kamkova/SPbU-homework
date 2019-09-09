#include <iostream>
#include <stack>

using namespace std;

int main()
{
    string input;
    stack<char> brackets;
    cin >> input;
    int n = input.size();
    bool answer = false;

    for (int i = 0; i < n; ++i)
    {
        if (input[i] == '[' || input[i] == '{')
            brackets.push(input[i]);
        else
        {
            if ((input[i] == ']' && brackets.top() == '[') || (input[i] == ')' && brackets.top() == '('))
                brackets.pop();
            else
            {
                cout << "No" << endl;
                answer = true;
                break;
            }
        }
    }

    if (!answer)
    {
        if (brackets.size() == 0)
            cout << "Yes" << endl;
        else
            cout << "No" << endl;
    }

    while (brackets.size() != 0)
        brackets.pop();

    return 0;
}
