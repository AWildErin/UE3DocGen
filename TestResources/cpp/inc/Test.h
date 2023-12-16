#pragma once


/**
    @class Test
*/
class Test
{
public:
    /** Constructor for the class */
    Test();

    /** Deconstructor for the class */
    ~Test();

    /** A public int */
    int PublicInt

    /**
    * A function on Test
    * 
    * @todo Some todo text
    * @param Param1 Description for Param1
    * @param Param2 Description for Param2
    */
    void SomeFunction(int Param1, float Param2);

private:
    /** A private int */
    int PrivateInt;
};