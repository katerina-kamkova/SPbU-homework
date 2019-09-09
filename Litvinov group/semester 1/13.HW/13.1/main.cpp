#include <iostream>
#include <string>

using namespace std;

enum class Status {
    start,                //_123.45E+67
    integralPart,         //1_2_3_.45E+67
    digitAfterDot,        //123._45E+67
    endOfFiction,         //123.4_5_E+67
    afterE,               //123.45E_+67
    afterSign,            //123.45E+_67
    endOfNumber,          //123.45E+6_7_
    fail
};

int main()
{
    string str = "12.23E-2";
    int i = 0;


    Status state = Status::start;
    while (str[i] != '\0') {
        switch (state) {
        case Status::start:
            if (isdigit(str[i])) {
                state = Status::integralPart;
            } else {
                state = Status::fail;
            }
            ++i;
            break;
        case Status::integralPart:
            if (str[i] == 'E') {
                state = Status::afterE;
            } else if (str[i] == '.') {
                state = Status::digitAfterDot;
            } else if (!isdigit(str[i])) {
                state = Status::fail;
            }
            ++i;
            break;
        case Status::digitAfterDot:
            if (isdigit(str[i])) {
                state = Status::endOfFiction;
            } else {
                state = Status::fail;
            }
            ++i;
            break;
        case Status::endOfFiction:
            if (str[i] == 'E') {
                state = Status::afterE;
            } else if (!isdigit(str[i])) {
                state = Status::fail;
            }
            ++i;
            break;
        case Status::afterE:
            if (isdigit(str[i])) {
                state = Status::endOfNumber;
            } else if (str[i] == '+' || str[i] == '-') {
                state = Status::afterSign;
            } else {
                state = Status:: fail;
            }
            ++i;
            break;
        case Status::afterSign:
            if (isdigit(str[i])) {
                state = Status::endOfNumber;
            } else {
                state = Status::fail;
            }
            ++i;
            break;
        case Status::endOfNumber:
            if (!isdigit(str[i])) {
                state = Status::fail;
            }
            ++i;
            break;
        case Status::fail:
            i = str.size();
        }
    }
    if (state != Status::integralPart && state != Status::endOfFiction && state != Status::endOfNumber) {
        cout << "String isn`t a real number" << endl;
    } else {
        cout << "String is a real number" << endl;
    }

    return 0;
}
