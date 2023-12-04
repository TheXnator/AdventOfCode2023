using System.Text.RegularExpressions;

class Day1() {
    public static void GetSolutions() {
        Console.WriteLine(string.Format("Day 1 part 1: {0}", CalculateCalibrations(false)));
        Console.WriteLine(string.Format("Day 1 part 2: {0}", CalculateCalibrations(true)));
    }

    static int CalculateCalibrations(bool allowWords) {
        string filename = "inputs/day1inputs.txt";
        int sum = 0;

        string[] contents = File.ReadAllLines(filename);
        Dictionary<string, int> numsDict = new Dictionary<string, int>() {
            ["one"] = 1,
            ["two"] = 2,
            ["three"] = 3,
            ["four"] = 4,
            ["five"] = 5,
            ["six"] = 6,
            ["seven"] = 7,
            ["eight"] = 8,
            ["nine"] = 9,
        };

        foreach (string line in contents)
        {
            string lineCopy = line;
            if (allowWords) {
                foreach (KeyValuePair<string, int> kv in numsDict) {
                    lineCopy = lineCopy.Replace(kv.Key, kv.Key[0] + Convert.ToString(kv.Value) + kv.Key[^1]);
                }
            }

            string regex = @"\d";
            MatchCollection result = Regex.Matches(lineCopy, regex);

            string firstnum = Convert.ToString(result[0].Value);
            string lastnum = Convert.ToString(result[^1].Value);
            string calibration = string.Format("{0}{1}", firstnum, lastnum);

            sum += Convert.ToInt32(calibration);
        }

        return sum;
    }
}