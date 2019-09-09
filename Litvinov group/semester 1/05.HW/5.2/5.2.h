#pragma once

//The warrior
struct Position;

//The function making the first warrior
Position* createPosition();

//The function making the circle of warriors
void createCircle(Position *first, int amount);

//The process of killing
int deleteAllButOne(Position *first, int amount, int period);

//The function returning the position of the last warrior
Position* last(Position *first);
