using System;
using System.Collections.Generic;
using System.Text;

namespace BattleSystem
{
    /// <summary>
    /// A static class That I made to Handle Console functions.
    /// </summary>
    public static class ConsoleExtracts
    {
        /// <summary>
        /// Color the text on the same line.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="color"></param>
        public static void ColorText(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }
        /// <summary>
        /// Color the text on a new line.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="color"></param>
        public static void ColorTextLine(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
        /// <summary>
        /// Highlight Text background.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="color"></param>
        public static void HighlightText(string text, ConsoleColor color)
        {
            Console.BackgroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
        /// <summary>
        /// Highlights the text background and color the text.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="colorBackground"></param>
        /// <param name="colorText"></param>
        public static void HighlightText(string text, ConsoleColor colorBackground, ConsoleColor colorText)
        {
            Console.BackgroundColor = colorBackground;
            Console.ForegroundColor = colorText;
            Console.WriteLine(text);
            Console.ResetColor();
        }
        /// <summary>
        /// Highlight the text background on a new line.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="color"></param>
        public static void HighlightTextLine(string text, ConsoleColor color)
        {
            Console.BackgroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
        /// <summary>
        /// Highlight the text background and change the text color.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="colorBackground"></param>
        /// <param name="colorText"></param>
        public static void HighlightTextLine(string text, ConsoleColor colorBackground, ConsoleColor colorText)
        {
            Console.BackgroundColor = colorBackground;
            Console.ForegroundColor = colorText;
            Console.WriteLine(text);
            Console.ResetColor();
        }
        /// <summary>
        /// Color center text.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="colorText"></param>
        /// <returns></returns>
        public static string CenterTextLine(string text, ConsoleColor colorText)
        {
            int windowWidth = (Console.WindowWidth / 2) + (text.Length / 2);
            Console.ForegroundColor = colorText;
            Console.WriteLine(string.Format("{0," + windowWidth + "}", text));
            Console.ResetColor();
            return text;
        }

        public static void ClearLines(int offset,int numLineClear)
        {
            int currentLinePos = (Console.CursorTop - offset);

            for(int i = currentLinePos; i != (currentLinePos + numLineClear); i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(new string(' ',Console.WindowWidth));
            }
            Console.SetCursorPosition(0, currentLinePos);


        }
    }
}
