
# SaidOut.StringExtensions [![NuGet Version](https://img.shields.io/nuget/v/SaidOut.StringExtensions.svg?style=flat)](https://www.nuget.org/packages/SaidOut.StringExtensions/)
String extensions for .NET that takes a type and return a string or takes a string and return a type.

---
## Table of Content
 * [Classes](#classes)
   * [SaidOut.StringExtensions.Base64Extension](#base64extension)
   * [SaidOut.StringExtensions.EnumerableExtension](#enumerableextension)
   * [SaidOut.StringExtensions.HexExtension](#hexextension)
   * [SaidOut.StringExtensions.StringExtension](#stringextension)

---
## Classes
 * [SaidOut.StringExtensions.Base64Extension](#base64extension) Extension converts a Base64 encoded string into a byte array or a byte array into a Base64 encoded string.
 * [SaidOut.StringExtensions.EnumerableExtension](#enumerableextension) Extension creates string representation out of collection.
 * [SaidOut.StringExtensions.HexExtension](#hexextension) Extension converts a Hex string into a byte array or a byte array into a Hex string.
 * [SaidOut.StringExtensions.StringExtension](#stringextension) Extension for string manipulation for example truncating a string.


---
### Base64Extension

| Name | Description |
|--------|-------------|
| `ToBase64String` | Create a Base64 encoded string from a byte array. |
| `FromBase64StringToByteArray` | Create a byte array from a Base64 encoded string. |
| `ToBase64UrlString` | Create a Base64 URL encoded string from a byte array. |
| `FromBase64UrlStringToByteArray` | Create a byte array from a Base64 URL encoded string. |

Example
```cs
    var byteArrayA = "bGVhc3VyZ+/=".FromBase64StringToByteArray();    // byteArrayA set to the byte representation of bGVhc3VyZS4=  
    var byteArrayB = "3d?".FromBase64StringToByteArray();             // byteArrayB set to null  
    var byteArrayC = "3d?".FromBase64StringToByteArray(false);        // An ArgumentException will be thrown  
    var byteArrayD = "bGVhc3VyZ-_".FromBase64UrlStringToByteArray();  // byteArrayAA set to the byte representation of bGVhc3VyZS4  

    var array = new byte[] {0x23, 0xff};  
    var base64 = array.ToBase64String();        // base64 set to      I/8=  
    var base64Url = array.ToBase64UrlString();  // base64Url set to>  I_8  
    var base64UrlWithPadding = array.ToBase64UrlString(false);  // base64UrlWithPadding set to>  I_8%3D  
```


---
### EnumerableExtension

| Name | Description |
|--------|-------------|
| `ToDelimitedString` | Create a string representation for a `collection` where each element is separated with the delimiter specified. |

Example
```cs
    var values = new[] {3, 5, 8, 10};  
    var strA = values.ToDelimitedString(", ", " and ");  // strA set to>  3, 5, 8 and 10  
    var strB = values.ToDelimitedString(val => "$" + val + "%", ", ", " or ");  // strA set to>  $3%, $5%, $8% or $10%  
```


---
### HexExtension

| Name | Description |
|--------|-------------|
| `ToHexString` | Create a hex string from a byte array. |
| `FromHexStringToByteArray` | Create a byte array from a hex string. |

Example
```cs
    var byteArrayA = "12fed3".FromHexStringToByteArray();    // byteArrayA set to the byte representation of 12fed3  
    var byteArrayB = "0x12fed3".FromHexStringToByteArray();  // byteArrayB set to the byte representation of 12fed3  
    var byteArrayC = "3de".FromHexStringToByteArray();       // byteArrayC set to null  
    var byteArrayD = "3de".FromHexStringToByteArray(false);  // An ArgumentException will be thrown  

    var array = new byte[] { 0x23, 0xff };  
    var hexA = array.ToHexString();       // hexA set to>  23FF  
    var hexB = array.ToHexString(false);  // hexA set to>  23ff  
```


---
### StringExtension

| Name | Description |
|--------|-------------|
| `Truncate` | Will truncate text so that it will fit inside `maxLength` including the `truncateSymbol`. |
| `AppendSymbolIfMissing` | Make sure that the `input` ends with symbol by appending it to the `input` if it does not end with symbol. |
| `ReplaceKeyWithValue` | Use the `keyValues` object to replace text matching the properties in keyValues with their corresponding property value. |

Example
```cs
    var strA = "this is a long string that should be truncated".Truncate(10, "~~");  // strA set to>  this is ~~  

    var strB = "/test".AppendSymbolIfMissing("/");   // strB set to>  /test/  
    var strC = "/test/".AppendSymbolIfMissing("/");  // strB set to>  /test/  

    var strD = "trying $old to replace keywords $new".ReplaceKeyWithValue(new  { old = "OLD_VALUE", @new = "NEW_VALUE" }, "$", null);  
    // strD set to>  trying OLD_VALUE to replace keywords NEW_VALUE  
```
