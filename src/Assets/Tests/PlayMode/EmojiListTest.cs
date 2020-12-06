using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests.PlayMode    
{
    /**
     * Tests the access of Emojis from Game-Class 
     */
    public class EmojiListTest
    {
        // Current Number of Emojis placed in GameObjects/PlayingField::Game
        private const int NUM_EMOJIS = 14;
        
        /**
         * Setup test environment
         */
        [SetUp]
        public void Setup()
        {
            SceneManager.LoadScene("Scenes/Scene_Playground_2vs2");
        }
        
        /**
         * Tests, if the EmojiListManager is properly initialized and returns a sprite
         *
         * @returns true, if Object Game exists and Instance doesn't return null and if Game returns a non null Object for the first object
         */
        [UnityTest]
        public IEnumerator EmojiListSimpleAccessTest()
        {
            yield return new WaitForSeconds(0.1f);
            Game game = Game.Instance;
            Assert.NotNull(game, "Expected Object Game to be initialized");
            Sprite sprite = game.GetEmoji(0); // get first emoji in list
            Assert.NotNull(sprite, "Didn't Get Sprite From Game");
            yield return null;
        }
        
        /**
         * Tests, if the EmojiListManager returns the same Emoji sprite, if the given number is NUM_EMOJIS
         *
         * @returns true, if Object Game exists and Instance doesn't return null and if Game returns a non null Object for the first object
         */
        [UnityTest]
        public IEnumerator EmojiListOverMaxNumberEmojisTest()
        {
            yield return new WaitForSeconds(0.1f);
            Game game = Game.Instance;
            Assert.NotNull(game, "Expected Object Game to be initialized");
            Sprite sprite = game.GetEmoji(0); // get first emoji in list
            Sprite comparer = game.GetEmoji(NUM_EMOJIS);
            Assert.AreEqual(sprite, comparer, "The two given emoji sprites are not the same.");
            yield return null;
        }
        
        /**
         * Tests, if the List of Emojis contains an emoji multiple times
         *
         * @returns true, if there is no duplicate emoji
         */
        [UnityTest]
        public IEnumerator EmojiListCheckDuplicatesTest()
        {
            yield return new WaitForSeconds(0.1f);
            Game game = Game.Instance;
            Assert.NotNull(game, "Expected Object Game to be initialized");
            List<Sprite> sprites = new List<Sprite>();
            for (var i = 0; i < NUM_EMOJIS; i++)
            {
                Sprite sprite = game.GetEmoji(i);
                if (sprites.Contains(sprite)) throw new AssertionException($"Emoji {sprite} is multiple times in list of emojis");
                sprites.Add(sprite);
            }
            
            yield return null;
        }
        
    }
}