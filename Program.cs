using System.Text.RegularExpressions;

void RunSolutions() {
    Console.WriteLine(string.Format("Day 1 part 1: {0}", CalculateCalibrations(false)));
    Console.WriteLine(string.Format("Day 1 part 2: {0}", CalculateCalibrations(true)));
    
    Console.WriteLine(string.Format("Day 2 part 1: {0}", IDSum()));
    Console.WriteLine(string.Format("Day 2 part 2: {0}", FewestCubes()));
}

int CalculateCalibrations(bool allowWords) {
    string filename = "day1inputs.txt";
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

int IDSum() {
    int sum = 0;

    string filename = "day2inputs.txt";
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

int FewestCubes() {
    int sum = 0;

    string filename = "day2inputs.txt";
    string[] contents = File.ReadAllLines(filename);

    Dictionary<string, int> allowed = new Dictionary<string, int>() {
        ["red"] = 12,
        ["green"] = 13,
        ["blue"] = 14,
    };

    int totalgameid = 0;
    foreach (string line in contents)
    {
        Dictionary<string, int> maximums = new Dictionary<string, int>() {
            ["red"] = 0,
            ["green"] = 0,
            ["blue"] = 0,
        };

        string gamestr = line.Split(':')[1];
        string[] games = gamestr.Split(';');
        int gameid = Convert.ToInt32(line.Split(':')[0].Substring(5));
        totalgameid += gameid;

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

RunSolutions();