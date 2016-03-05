// testClassDll/h

#ifdef TESTCLASSDLL_EXPORTS

#define TESTCLASSDLL_API __declspec(dllexport)
#else
#define TESTCLASSDLL_API __declspec(dllimport)

#endif TESTCLASSDLL_EXPORTS

namespace testClass
{
	//This class is exported from the testClassDll.dll
	class MyTestClass
	{
	public:
		// Returns a + b
		static TESTCLASSDLL_API double Add(double a, double b);

		// Returns a - b
		static TESTCLASSDLL_API double Subtract(double a, double b);

		// Returns a * b
		static TESTCLASSDLL_API double Multiply(double a, double b);

		// Returns a / b
		// Throws const std::invalid_argument& if b is 0
		static TESTCLASSDLL_API double Divide(double a, double b);
	};
}
