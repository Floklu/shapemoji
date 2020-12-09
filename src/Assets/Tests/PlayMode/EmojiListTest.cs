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
            yield return new WaitForEndOfFrame();
            var game = Game.Instance;
            Assert.NotNull(game, "Expected Object Game to be initialized");
            var sprite = game.GetEmoji(0); // get first emoji in list
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
            yield return new WaitForEndOfFrame();
            var game = Game.Instance;
            Assert.NotNull(game, "Expected Object Game to be initialized");
            var sprite = game.GetEmoji(0); // get first emoji in list
            var comparer = game.GetEmoji(game.GetSpriteListCount());
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
            yield return new WaitForEndOfFrame();
            var game = Game.Instance;
            Assert.NotNull(game, "Expected Object Game to be initialized");
            var sprites = new List<Sprite>();
            for (var i = 0; i < game.GetSpriteListCount(); i++)
            {
                var sprite = game.GetEmoji(i);
                if (sprites.Contains(sprite))
                    throw new AssertionException($"Emoji {sprite} is multiple times in list of emojis");
                sprites.Add(sprite);
            }

            yield return null;
        }
    }
}