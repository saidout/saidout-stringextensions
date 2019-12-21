namespace SaidOut.StringExtensions
{

    internal static class ExceptionMessages
    {

        /// <summary>{paramName} can't be null or empty.</summary>
        public static string StringParamCannotBeNullOrEmpty(string paramName) => $"{paramName} can't be null or empty.";

        /// <summary>{paramName} can't be less then {lowerBound}. Value was {paramValue}.</summary>
        public static string ParamCannotBeLessThan(int paramValue, string paramName, int lowerBound) =>
            $"{paramName} can't be less then {lowerBound}. Value was {paramValue}.";

        /// <summary>{paramAName} string length ({paramAStrLen}) can't be greater than value of {paramBName} ({paramBValue}).</summary>
        public static string ParamAStrLenCannotBeGreaterThanValueOfParamB(string paramAName, int paramAStrLen,  string paramBName, int paramBValue) =>
            $"{paramAName} string length ({paramAStrLen}) can't be greater than value of {paramBName} ({paramBValue}).";

        /// <summary>The input is not a valid Base-64 url string as it contains a non-base 64 url character, more than two padding characters, or an illegal character among the padding characters.</summary>
        public static string Base64UrlIllegalCharacter => "The input is not a valid Base-64 url string as it contains a non-base 64 url character, more than two padding characters, or an illegal character among the padding characters.";

        /// <summary>The input is not a valid hex string as it contains a non-hex character.</summary>
        public static string HexStringHasIllegalCharacter => "The input is not a valid hex string as it contains a non-hex character.";

        /// <summary>Invalid length for a hex char array or string.</summary>
        public static string HexStringInvalidLength => "Invalid length for a hex char array or string.";
    }
}