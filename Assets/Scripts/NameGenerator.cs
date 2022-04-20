using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class NameGenerator
{
    public static string GenerateProfileId(int length)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";

        var random = new System.Random();

        return new string(Enumerable.Repeat(chars, length)
            .Select(letter => letter[random.Next(letter.Length)]).ToArray());
    }
}
