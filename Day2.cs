using System.Text.RegularExpressions;

class Day2() {
    public static void GetSolutions() {
        Console.WriteLine(string.Format("Day 2 part 1: {0}", IDSum()));
        Console.WriteLine(string.Format("Day 2 part 2: {0}", FewestCubes()));
    }

    static int IDSum() {
        int sum = 0;

        string filename = "inputs/day2inputs.txt";
        string[] contents = File.ReadAllLines(filename);

        Dictionary<string, int> allowed = new Dictionary<string, int>() {
            ["red"] = 12,
            ["green"] = 13,
            ["blue"] = 14,
        };

        int totalgameid = 0;
        foreach (string line in contents)
        {
            string gamestr = line.Split(':')[1];
            string[] games = gamestr.Split(';');
            int gameid = Convert.ToInt32(line.Split(':')[0].Substring(5));
            totalgameid += gameid;

            foreach (string game in games) {
                MatchCollection redmatches = Regex.Matches(game, @"[\d]+ red");
                MatchCollection bluematches = Regex.Matches(game, @"[\d]+ blue");
                MatchCollection greenmatches = Regex.Matches(game, @"[\d]+ green");

                string regex = @"\d+";
                if (redmatches.Count > 0 && Convert.ToInt32(Regex.Matches(redmatches[0].Value, regex)[0].Value) > allowed["red"]) {
                    sum += gameid;
                    break;
                } else if (bluematches.Count > 0 && Convert.ToInt32(Regex.Matches(bluematches[0].Value, regex)[0].Value) > allowed["blue"]) {
                    sum += gameid;
                    break;
                } else if (greenmatches.Count > 0 && Convert.ToInt32(Regex.Matches(greenmatches[0].Value, regex)[0].Value) > allowed["green"]) {
                    sum += gameid;
                    break;
                }
            }
        }

        return totalgameid - sum;
    }

    static int FewestCubes() {
        int sum = 0;

        string filename = "inputs/day2inputs.txt";
        string[] contents = File.ReadAllLines(filename);

        Dictionary<string, int> allowed = new Dictionary<string, int>() {
            ["red"] = 12,
            ["green"] = 13,
            ["blue"] = 14,
        };

        foreach (string line in contents)
        {
            Dictionary<string, int> maximums = new Dictionary<string, int>() {
                ["red"] = 0,
                ["green"] = 0,
                ["blue"] = 0,
            };

            string gamestr = line.Split(':')[1];
            string[] games = gamestr.Split(';');
            foreach (string game in games) {
                MatchCollection redmatches = Regex.Matches(game, @"[\d]+ red");
                MatchCollection bluematches = Regex.Matches(game, @"[\d]+ blue");
                MatchCollection greenmatches = Regex.Matches(game, @"[\d]+ green");

                string regex = @"\d+";
                if (redmatches.Count > 0) {
                    int val = Convert.ToInt32(Regex.Matches(redmatches[0].Value, regex)[0].Value);
                    if (val > maximums["red"]) {
                        maximums["red"] = val;
                    }
                } if (bluematches.Count > 0) {
                    int val = Convert.ToInt32(Regex.Matches(bluematches[0].Value, regex)[0].Value);
                    if (val > maximums["blue"]) {
                        maximums["blue"] = val;
                    }
                } if (greenmatches.Count > 0) {
                    int val = Convert.ToInt32(Regex.Matches(greenmatches[0].Value, regex)[0].Value);
                    if (val > maximums["green"]) {
                        maximums["green"] = val;
                    }
                }
            }

            int power = maximums["green"] * maximums["blue"] * maximums["red"];
            sum += power;
        }

        return sum;
    }
}