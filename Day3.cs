using System.Text.RegularExpressions;

class Day3() {
    public static void GetSolutions() {
        Console.WriteLine(string.Format("Day 3 part 1: {0}", EnginePartSum()));
        //Console.WriteLine(string.Format("Day 3 part 2: {0}", EnginePartSum()));
    }

    static bool IsSchematicSymbol(char c) {
        return (!char.IsDigit(c)) && (c != '.');
    }

    static int EnginePartSum() {
        int sum = 0;

        string filename = "inputs/day3inputs.txt";
        string[] contents = File.ReadAllLines(filename);

        for (int x = 0; x < contents.Length; x++)
        {
            MatchCollection regMatchCollection = Regex.Matches(contents[x], @"[\d]+");
            foreach (Match regMatch in regMatchCollection) {
                var index = regMatch.Index;
                var matchEnd = index + regMatch.Value.Length;

                // Left/right adjacent
                if ((index > 0 && IsSchematicSymbol(contents[x][index - 1])) || (matchEnd < contents[x].Length && IsSchematicSymbol(contents[x][matchEnd]))) {
                    sum += Convert.ToInt32(regMatch.Value);
                    continue;
                }
                
                // Up/down/diagonal adjacent
                string prevLine = (x > 0) ? contents[x - 1] : "";
                string nextLine = (x < contents.Length - 1) ? contents[x + 1] : "";
                int startSearch = index > 0 ? index - 1 : 0;
                int searchLength = index > 0 ? regMatch.Value.Length + 2 : regMatch.Value.Length + 1;
                string substrprev = prevLine.Length > 0 ? prevLine.Substring(startSearch, Math.Min(searchLength, prevLine.Length - startSearch)) : "";
                string substrnext = nextLine.Length > 0 ? nextLine.Substring(startSearch, Math.Min(searchLength, nextLine.Length - startSearch)) : "";
                foreach (char c in substrprev + substrnext) {
                    if (IsSchematicSymbol(c)) {
                        sum += Convert.ToInt32(regMatch.Value);
                        break;
                    }
                }
            }
        }

        return sum;
    }
}