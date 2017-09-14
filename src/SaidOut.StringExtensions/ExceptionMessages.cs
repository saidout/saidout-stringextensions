namespace SaidOut.StringExtensions
{

    internal static class ExceptionMessages
    {

        /// <summary>{0} can't be null or empty.</summary>
        public const string StringParamCannotBeNullOrEmpty_ParamName = "{0} can't be null or empty.";

        /// <summary>{0} can't be less then {1}. Value was {2}.</summary>
        public const string ParamCannotBeLessThan_ParamName_MinValue_ActualValue = "{0} can't be less then {1}. Value was {2}.";

        /// <summary>{0} string length can't be greater than value of {1}.</summary>
        public const string ParamAStringLengthCannotBeGreaterThanValueOfParamB_ParamAStrLen_ParamBValue = "{0} string length can't be greater than value of {1}.";

        /// <summary>The input is not a valid Base-64 url string as it contains a non-base 64 url character, more than two padding characters, or an illegal character among the padding characters.</summary>
        public const string Base64UrlIllegalCharacter = "The input is not a valid Base-64 url string as it contains a non-base 64 url character, more than two padding characters, or an illegal character among the padding characters.";

        /// <summary>The input is not a valid hex string as it contains a non-hex character.</summary>
        public const string HexStringHasIllegalCharacter = "The input is not a valid hex string as it contains a non-hex character.";

        /// <summary>Invalid length for a hex char array or string.</summary>
        public const string HexStringInvalidLength = "Invalid length for a hex char array or string.";
    }
}