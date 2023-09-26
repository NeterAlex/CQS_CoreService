using System;

namespace CQS_CoreService.Core.Utils;

public class Desensitizer
{
    public static string GetSafePhone(string phone)
    {
        if (phone is null or "")
            return null;
        return string.Concat("******", phone.AsSpan(phone.Length - 4));
    }

    public static string GetSafeIdNumber(string idNumber)
    {
        if (idNumber is null or "")
            return null;
        return string.Concat(
            idNumber.AsSpan(0, 3),
            new string('*', idNumber.Length - 5),
            idNumber.AsSpan(idNumber.Length - 2));
    }
}