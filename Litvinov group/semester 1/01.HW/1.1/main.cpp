#include <iostream>

using namespace std;

int main()
{
    int x;
    cin >> x;
    int square = x*x;
    cout << (square + x)*(square + 1) + 1 << endl;
    return 0;
}
