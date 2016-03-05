// MathFuncsDll.cpp : Defines the exported functions for the DLL application.
//

//#include "stdafx.h"
#include "testClassDll.h"
#include <stdexcept>

using namespace std;

namespace testClass
{
	double MyTestClass::Add(double a, double b)
	{
		return a + b;
	}

	double MyTestClass::Subtract(double a, double b)
	{
		return a - b;
	}

	double MyTestClass::Multiply(double a, double b)
	{
		return a * b;
	}

	double MyTestClass::Divide(double a, double b)
	{
		if (b == 0)
		{
			throw invalid_argument("b cannot be zero!");
		}

		return a / b;
	}
}