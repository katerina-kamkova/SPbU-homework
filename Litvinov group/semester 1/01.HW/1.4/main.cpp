#include <iostream>

using namespace std;

int main()
{
    int i, answer = 0;
    int Array[28];

    for (i = 0; i <28; ++i)
        Array[i] = 0;

    for (i = 0; i < 10; ++i)
        for (int j = 0; j < 10; ++j)
            for (int k = 0; k < 10; ++k)
                Array[i+j+k] ++;

    for(i = 0; i < 28; ++i)
        answer += Array[i]*Array[i];

    cout << answer << endl;
    return 0;
}
