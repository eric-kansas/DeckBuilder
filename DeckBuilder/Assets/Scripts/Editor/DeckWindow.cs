/// C# Example
// Simple script that saves frames from the Game View when on play mode
//
// You can put later the frames togheter and create a video.
// Note: The frames are saved next to the Assets folder.

using UnityEngine;
using UnityEditor;

public class DeckWindow : EditorWindow {
	[MenuItem ("Example/Count")]
	static void DoIt () {
		Selection.activeGameObject = GameObject.Find ("Deck");
		Deck[] decks = (Deck[]) Resources.FindObjectsOfTypeAll(typeof(Deck));
		if (decks.Length > 0 ) {
			decks[0].Count();
		}
	}
}