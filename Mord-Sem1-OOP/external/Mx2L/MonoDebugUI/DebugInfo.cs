// MonoDebugUI made by Mx2L, aka. Michael M. Lukassen.

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Mx2L.MonoDebugUI
{
    public static class DebugInfo
    {
        private static Dictionary<string, Func<string>> _stringList = new Dictionary<string, Func<string>>();
        private static Dictionary<string, int> _countList = new Dictionary<string, int>();
        private static formats formatOutput = formats.CamelCaseToText;

        private enum formats
        {
            Unchanged,
            CamelCaseToText,
            AnyToText,
            AnyToCamelCase
        }

        /* Shortcuts */
        public static void Add(string identifier, Func<string> method)
        { AddString(identifier, method); }
        public static void Add(string identifier, int count)
        { AddCount(identifier, count); }

        public static void Remove(string identifier)
        {
            RemoveString(identifier);
            RemoveCount(identifier);
        }

        public static string Get(string identifier)
        {
            TryGet(identifier, out string output);
            return output;
        }
        public static bool TryGet(string identifier, out string output)
        {
            if (TryGetString(identifier, out output))
                return true;
            if (TryGetCount(identifier, out output))
                return true;
            return false;
        }
        public static string[] GetAllInfo()
        {
            string[] list = new string[_stringList.Count + _countList.Count];

            int i = 0;
            foreach (var entry in _stringList)
            {
                string identifier = FormatIdentifierToText(entry.Key);
                list[i++] = $"{identifier}: {entry.Value.Invoke()}";
            }

            foreach (var entry in _countList)
            {
                string identifier = FormatIdentifierToText(entry.Key);
                list[i++] = $"{identifier}: {entry.Value}";
            }

            return list;
        }

        public static string[] GetAllIdentifiers()
        {
            string[] list = new string[_stringList.Count + _countList.Count];


            for (int i = 0; i < _stringList.Count; i++)
                list[i] = _stringList.ElementAt(i).Key;

            for (int i = 0; i < _countList.Count; i++)
                list[_stringList.Count + i] = _countList.ElementAt(i).Key;

            return list;
        }

        public static string GetString(string identifier)
        {
            TryGetString(identifier, out string output);
            return output;
        }
        public static bool TryGetString(string identifier, out string output)
        {
            output = $"{identifier} not found";

            if (!_stringList.TryGetValue(identifier, out Func<string> method))
                return false;

            output = method();
            return true;
        }

        /* Strings */
        public static void AddString(string identifier, Func<string> method)
        { _stringList.Add(identifier, method); }
        public static void RemoveString(string identifier)
        { _stringList.Remove(identifier); }

        /* Counter */
        public static void AddCount(string identifier)
        { AddCount(identifier, 0); }
        public static void AddCount(string identifier, int count)
        { _countList.Add(identifier, count); }
        public static void RemoveCount(string identifier)
        { _stringList.Remove(identifier); }

        public static string GetCount(string identifier)
        {
            TryGetCount(identifier, out string output);
            return output;
        }
        public static bool TryGetCount(string identifier, out string output)
        {
            output = $"{identifier} not found";

            if (!_countList.TryGetValue(identifier, out int count))
                return false;

            output = count.ToString();
            return true;
        }

        public static void IncreaseCount(string identifier)
        {
            if (!_countList.ContainsKey(identifier))
                return;
            _countList[identifier]++;
        }
        public static void DecreaseCount(string identifier)
        {
            if (!_countList.ContainsKey(identifier))
                return;
            _countList[identifier]--;
        }
        public static void IncreaseCount(string identifier, int count)
        {
            if (!_countList.ContainsKey(identifier))
                return;
            _countList[identifier] += count;
        }
        public static void DecreaseCount(string identifier, int count)
        {
            if (!_countList.ContainsKey(identifier))
                return;
            _countList[identifier] -= count;
        }

        /* Draw */
        public static void DrawInfo(SpriteBatch spriteBatch, string identifier, Vector2 position, SpriteFont font)
        { DrawInfo(spriteBatch, identifier, position, font, Color.White); }
        public static void DrawInfo(SpriteBatch spriteBatch, string identifier, Vector2 position, SpriteFont font, Color color)
        {
            string output = Get(identifier);
            output = $"{FormatIdentifierToText(identifier)}: {output}";

            spriteBatch.DrawString(font, output, position, color);
        }

        public static void DrawInfo(SpriteBatch spriteBatch, Vector2 position, int rowSpacing, SpriteFont font, IEnumerable<string> identifiers)
        { DrawInfo(spriteBatch, position, rowSpacing, font, Color.White, identifiers.ToArray()); }
        public static void DrawInfo(SpriteBatch spriteBatch, Vector2 position, int rowSpacing, SpriteFont font, Color color, IEnumerable<string> identifiers)
        { DrawInfo(spriteBatch, position, rowSpacing, font, color, identifiers.ToArray()); }
        public static void DrawInfo(SpriteBatch spriteBatch, Vector2 position, int rowSpacing, SpriteFont font, params string[] identifiers)
        { DrawInfo(spriteBatch, position, rowSpacing, font, Color.White, identifiers); }
        public static void DrawInfo(SpriteBatch spriteBatch, Vector2 position, int rowSpacing, SpriteFont font, Color color, params string[] identifiers)
        {
            int row = 0;
            foreach (string identifier in identifiers)
                DrawInfo(spriteBatch, identifier, position + new Vector2(0, rowSpacing * row++), font, color);
        }

        public static void DrawAllInfo(SpriteBatch spriteBatch, Vector2 position, int rowSpacing, SpriteFont font)
        { DrawAllInfo(spriteBatch, position, rowSpacing, font, Color.White); }
        public static void DrawAllInfo(SpriteBatch spriteBatch, Vector2 position, int rowSpacing, SpriteFont font, Color color)
        {
            int row = 0;
            foreach (string output in GetAllInfo())
                spriteBatch.DrawString(font, output, position + new Vector2(0, rowSpacing * row++), color);
        }

        private static string FormatIdentifierToText(string identifier)
        {
            switch (formatOutput)
            {
                case formats.Unchanged:
                    return identifier;

                case formats.CamelCaseToText:
                    return FormatCamelCaseToText(identifier);

                case formats.AnyToText:
                    return FormatToText(identifier);

                case formats.AnyToCamelCase:
                    return FormatToCamelCase(identifier);
            }

            return identifier;
        }

        private static string FormatCamelCaseToText(string identifier)
        {
            identifier = Regex.Replace(identifier, @"[A-Z]", m => " " + m.ToString().ToLower());
            identifier = Regex.Replace(identifier, @"\s+", " ");
            return $"{identifier.Trim().First().ToString().ToUpper() + identifier.Substring(1)}";
        }

        private static string FormatToText(string identifier)
        {
            identifier = Regex.Replace(identifier, @"(?<=[A-z])-|-(?! )", " ");
            identifier = Regex.Replace(identifier, @"_", " ");
            identifier = Regex.Replace(identifier, @"[A-Z]", m => " " + m.ToString().ToLower());
            identifier = Regex.Replace(identifier, @"\s+", " ");
            return $"{identifier.Trim().First().ToString().ToUpper() + identifier.Substring(1)}";
        }

        private static string FormatToCamelCase(string identifier)
        {
            identifier = Regex.Replace(identifier, @"((?<=-)|(?<=_)| )[a-z]", m => m.ToString().ToUpper());
            identifier = Regex.Replace(identifier, @"\s|_|-", "");
            return $"{identifier.First().ToString().ToLower() + identifier.Substring(1)}";
        }
    }
}