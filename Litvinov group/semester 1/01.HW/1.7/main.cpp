#include <iostream>
#include <vector>

using namespace std;

int main()
{
    int n;
    cin >> n;

    if (n < 1)
        cout << "No" << endl;
    else if (n >= 1)
    {
        cout << 1;
        if (n >= 2)
        {
            cout << " " << 2;
            if (n >= 3)
                cout << " " << 3;
        }
    }

    vector<int> prime_numbers(1);
    prime_numbers[0] = 3;


    for (int i = 5; i <= n; i += 2)
    {
        for (int j = 0; j < prime_numbers.size(); ++j)
        {
            if (i%prime_numbers[j] == 0)
                j = prime_numbers.size();
            else if (j == prime_numbers.size()-1)
                cout << " " << i;
        }
    }

    while (prime_numbers.size() != 0)
        prime_numbers.pop_back();

    cout << endl;
    return 0;
}
