MonoDebugUI made by Mx2L, aka. Michael M. Lukassen.

# How to use:
DebugInfo contains two dictionaries (will refer as lists from now on), one that accepts methods (Func<string>) with no parameters and the other an integer. Both lists returns strings.

Adding to string list is done with a unique identifier and a delegate method that returns a string.
- DebugInfo.AddString("identifier", method<string>);

Adding to the count list can be done with just a unique identifier or with an optional count. If count is not supplied, it will be set to 0.
- DebugInfo.AddCount("identifier");
- DebugInfo.AddCount("identifier", int count);

Recieving the info is done with the identifier. Both returns as string, in case of count identifier is not found.
- DebugInfo.GetString("identifier");
- DebugInfo.GetCount("identifier");

Alternative methods for recieving:
- DebugInfo.TryGetString("identifier", out string output), returns a boolean.
- DebugInfo.TryGetCount("identifier", out string output), returns a boolean.

# Shorthands:
- DebugInfo.Get("identifier") and TryGet("identifier", out string output).
	Gets the info from string list or the count.

	Note that both lists can each have the same identifier, thus the one in the string list will be returned.
	So it's best to have completely unique identifiers or explicitly use GetString() or GetCount().

- DebugInfo.Add("identifier", Func<string>), adds to string list.
- DebugInfo.Add("identifier"), adds 0 to count list.
- DebugInfo.Add("identifier", int count), adds count to list.

- Debug.Remove("identifier")
	Removes info from both string and count list.

# Draw
When outputting text to screen, all outputs returns "{identifier}: {output}".
The identifier is by default formatted from CamelCase to regular text.
All available identifier formatting:
- Unchanged,
- CamelCaseToText,
- AnyToText,
- AnyToCamelCase

To output a single info to screen, a sprite batch, identifier, position, sprite font and optionally color, has to be supplied.
- DrawInfo(SpriteBatch spriteBatch, string identifier, Vector2 position, SpriteFont font)
- DrawInfo(SpriteBatch spriteBatch, string identifier, Vector2 position, SpriteFont font, Color color)

To output multiple, you can apply IEnumerable<string>, like a List<string>, or an string[].
- DrawInfo(SpriteBatch spriteBatch, string identifier, Vector2 position, SpriteFont font, IEnumerable<string> identifiers)
- DrawInfo(SpriteBatch spriteBatch, string identifier, Vector2 position, SpriteFont font, Color color, params string[] identifiers)

To output all information to screen, a sprite batch, position, row spacing, sprite font and optionally color, has to be supplied.
- DrawAllInfo(SpriteBatch spriteBatch, Vector2 position, int rowSpacing, SpriteFont font)
- DrawAllInfo(SpriteBatch spriteBatch, Vector2 position, int rowSpacing, SpriteFont font, Color color)

# Methods
DebugInfo
- AddString(string, Func<string>);
- AddCount(string);
- AddCount(string, int);

- RemoveString(string);
- RemoveCount(string);

- GetString(string);
- GetCount(string);
- TryGetString(string);
- TryGetCount(string);

- Add(string, Func<string>); -> string list
- Add(string); -> count list
- Add(string, int) -> count list
- Remove(string); -> string list + count list

- DrawInfo(SpriteBatch, string, Vector2, SpriteFont)
- DrawInfo(SpriteBatch, string, Vector2, SpriteFont, Color)
- DrawInfo(SpriteBatch, string, Vector2, SpriteFont, IEnumerable<string>)
- DrawInfo(SpriteBatch, string, Vector2, SpriteFont, Color, IEnumerable<string>)
- DrawInfo(SpriteBatch, string, Vector2, SpriteFont, string[])
- DrawInfo(SpriteBatch, string, Vector2, SpriteFont, Color, string[])