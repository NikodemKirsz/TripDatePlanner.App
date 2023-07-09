namespace TripDatePlanner.Helpers;

public static class UidUtils
{
    private static readonly Random Rand = new();
    private static readonly char[] AllowedCharacters =
    {
        'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
        'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
        '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
    };

    public static string CreateRandomId(int length)
    {
        char[] chars = new char[length];
        
        for (int i = 0; i < length; i++)
        {
            int index = Rand.Next(0, AllowedCharacters.Length);
            chars[i] = AllowedCharacters[index];
        }

        return new string(chars);
    }
}