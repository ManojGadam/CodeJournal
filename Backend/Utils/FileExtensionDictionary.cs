namespace PersonalWebsite.Utils
{
    public static class FileExtensionDictionary
    {
        private static Dictionary<string, string> _fileExtensions = new Dictionary<string, string>() {
          { "C", ".c" },
          { "C++", ".cpp" },
          { "C#", ".cs" },
          {"Dart", ".dart" },
          { "Elixir", ".ex" },
          {"Erlang", ".erl" },
          {"Go", ".go" },
          {"Java", ".java" },
          {"JavaScript", ".js" },
          {"Kotlin", ".kt" },
          {"PHP", ".php" },
          {"Python", ".py" },
          {"Python3", ".py" },
          {"Racket", ".rkt" },
          {"Ruby", ".rb" },
          {"Rust", ".rs" },
          {"Scala", ".scala" },
          {"Swift", ".swift" },
          {"TypeScript", ".ts" },
          {"MySQL", ".sql" },
          {"PostgreSQL", ".sql" },
          {"Oracle", ".sql" },
          {"MS SQL Server", ".tsql" },
          {"Pandas", ".py" },
        };
        public static string GetExtension(string lang)
        {
            return _fileExtensions[lang];
        }
    }
}
