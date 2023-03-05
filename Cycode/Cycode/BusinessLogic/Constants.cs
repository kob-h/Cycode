using System;
namespace Cycode.BusinessLogic
{
	public static class Constants
	{
        public static class SplitRange
        {
            public const int LOGIC_OPERATOR_INDEX = 0;
            public const int VULNERABLE_PACKAGE_VERSION_INDEX = 1;
        }

        public static class LogicOperations
        {
            public const string GREATER_THAN = ">";
            public const string GREATER_THAN_OR_EQUAL = ">=";
            public const string EQUAL = "=";
            public const string LESS_THAN = "<";
            public const string LESS_THAN_OR_EQUAL = "<=";
        }
    }
}

