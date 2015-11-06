using System.Text.RegularExpressions;

namespace csgo_AliasFileGen
{
    public class CsgoAlias
    {
        // Regex Pattern to recognise Alias statements with
        // example: alias "gm_guardian" "game_mode 0; game_type 4" // Sets Gamemode to Guardian
        public static string AliasRegexPattern = "^ *alias *(\".*\" *){2}.*$";

        public string AliasName { get; set; }

        public string AliasCommand { get; set; }

        public string AliasComment { get; set; }

        public CsgoAlias()
        {
            AliasName = "";
            AliasCommand = "";
            AliasComment = "";
        }

        public CsgoAlias(string name, string command, string comment)
        {
            AliasName = name;
            AliasCommand = command;
            AliasComment = comment;
        }

        public CsgoAlias(string cfgLine)
        {
            string name, command, comment;
            ExtractDataFromConfigLine(cfgLine, out name, out command, out comment);

            AliasName = name;
            AliasCommand = command;
            AliasComment = comment;
        }

        public static void ExtractDataFromConfigLine(string cfgLine, out string aliasName, out string aliasCommand, out string aliasComment)
        {
            aliasName = ExtractAliasName(cfgLine);
            aliasCommand = ExtractAliasCommand(cfgLine);
            aliasComment = ExtractAliasComment(cfgLine);
        }

        private static string ExtractAliasName(string cfgLine)
        {
            // replaces 'alias "' with empty string
            var name = Regex.Replace(cfgLine, "^ *alias *\"", "");
            // replaces '" "command" // comment' with empty string
            name = Regex.Replace(name, "\" *\".*\".*", "");
            return name;
        }

        private static string ExtractAliasCommand(string cfgLine)
        {
            // replaces 'alias "name" "' with empty string
            var command = Regex.Replace(cfgLine, "^ *alias *\".*\" *\"", "");
            // replaces '" // comment' with empty string
            command = Regex.Replace(command, "\".*", "");
            return command;
        }

        private static string ExtractAliasComment(string cfgLine)
        {
            // replaces 'alias "name" "command" // ' with empty string
            var comment = Regex.Replace(cfgLine, "^ *alias *(\".*\" *){2}/?/? ?", "");
            return comment;
        }
    }
}
