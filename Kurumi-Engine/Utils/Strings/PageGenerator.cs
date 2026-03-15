namespace Utils.Strings;

/// <summary>
/// The page generator class, this is used to turn a string into pages changing '-' into ',' and using '/' as a new line indicator.
/// </summary>
public static class PageGenerator {
    /// <summary>
    /// Static script function that turns a passed string into a set of pages used in text boxes.
    /// </summary>
    /// <param name="text">The unfilitered text that needs to be seperated.</param>
    /// <param name="maxLines">The max number of lines.</param>
    /// <returns>Returns a set of pages in the form of a string array.</returns>
    public static string[,] TurnTextIntoPages(string text, int maxLines) {
        // We have to replace '-' as the comma is used inside scripts to seperate information.
        text = text.Replace('-', ',');
        int lines = text.Count(f => f == '/');
        string[,] pages;
        // If multiple lines.
        if (lines > 0) {
            pages = new string[(lines / maxLines) + 1, maxLines];
            // Loop over the text splitting up the pages correctly.
            for (int currentLine = 1; currentLine < lines + 1; currentLine++) {
                int currentPage = currentLine / (maxLines + 1);
                int currentLineInPage = currentLine % (maxLines + 1);
                pages[currentPage, currentLineInPage - 1] = text[..text.IndexOf('/')];
                text = text[(text.IndexOf('/') + 1)..];
            }
            // Get final line.
            int finalPage = lines / maxLines;
            int finalLine = lines % maxLines;
            pages[finalPage, finalLine] = text; 
        }
        else {
            pages = new string[1, 1];
            pages[0, 0] = text;
        }
        return pages;
    }
}