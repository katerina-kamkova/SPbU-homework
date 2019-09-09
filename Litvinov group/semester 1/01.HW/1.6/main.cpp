#include <iostream>
#include <string>
#include <conio.h>

using namespace std;

int main()
{
    string st = "0";
    string input = "0";

    cout << "Enter the main string: ";
    //cin >> input;
    getline(cin, input);
    cout << endl << "Enter the substring: ";
    cin >> st;
    int counter = 0;

    if (st.size() > input.size())
    {
        cout << endl << "The amount of substrings in the main string: 0" << endl;
    }
    else
    {
        for (int i = 0; i < input.size() - st.size() + 1; ++i)
        {
            for (int j = 0; j < st.size(); ++j)
            {
                if(input[i + j] != st[j])
                {
                    j = st.size();
                }
                else if (j == st.size() - 1)
                {
                    ++counter;
                }
            }
        }
        cout << endl << "The amount of substrings in the main string: " << counter << endl;
    }

    return 0;
}
