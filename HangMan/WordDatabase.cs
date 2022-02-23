using System.Reflection;
using Microsoft.Data.SqlClient;

namespace HangMan;

public class WordDatabase
{
    // todo: move to config file
    const string ConnectionString = @"abc";
        //@"Data Source=.;Initial Catalog=HangMan;Integrated Security=True;TrustServerCertificate=True;";

    const string SelectCommand = @"
SELECT
	Word
FROM
	Words";

    // todo: move option to config file
    /// <summary>
    /// True to read words from a database. Otherwise, text file is used.
    /// </summary>
    public static bool UseDatabase { get; set; } = true;

    public static string[] GetAllWords()
    {
        if (UseDatabase)
        {
            try
            {
                return GetAllWordsFromDatabase().ToArray();
            }
            catch
            {
                Console.WriteLine("Failed to connect to database. Falling back to local file word source.");
                // swallow exception and fall back to using local file
            }
        }

        return GetAllWordsFromFile().ToArray();
    }

    public static string[] GetAllWordsFromFile()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = "HangMan.Words.txt";
        string fullText;
        using (var stream = assembly.GetManifestResourceStream(resourceName))
        using (var sr = new StreamReader(stream))
        {
            fullText = sr.ReadToEnd();
        }

        return fullText.Split(Environment.NewLine);
    }

    public static string[] GetAllWordsFromDatabase()
    {
        var words = new List<string>();

        using (var connection = OpenConnection())
        using (var cmd = new SqlCommand(SelectCommand, connection))
        using (var reader = cmd.ExecuteReader())
        {
            while (reader.Read())
                words.Add(reader.GetString(0));

            reader.Close();
        }

        return words.ToArray();
    }

    static SqlConnection OpenConnection()
    {
        var connection = new SqlConnection(ConnectionString);
        connection.Open();
        return connection;
    }
}