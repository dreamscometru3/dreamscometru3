using System;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace qwe
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var game = new GameLogic(0, new List<int>() { 1, 1, 1, 1, 1 });

            Assert.AreEqual(5, game.userPoints);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var game = new GameLogic(0, new List<int>() { 1, 0, 1, 1, 1, 1 });

            Assert.AreEqual(5, game.userPoints);
        }

        [TestMethod]
        public void TestMethod3()
        {
            var game = new GameLogic(0, new List<int>() { 0,1, 1, 1, 1, 1 });

            Assert.AreEqual(5, game.userPoints);
        }

        [TestMethod]
        public void TestMethod4()
        {
            var game = new GameLogic(1, new List<int>() { 1, 1, 1, 1, 1, 1 });

            Assert.AreEqual(1, game.opponentPoints);
            Assert.AreEqual(5, game.userPoints);
        }

        [TestMethod]
        public void TestMethod5()
        {
            var game = new GameLogic(1, new List<int>() { 0, 0, 1, 1, 1, 1, 0, 0, 1 });

            if (game.opponentPoints == 5)
            {
                Assert.AreEqual(5, game.opponentPoints);
                Assert.AreNotEqual(5, game.userPoints);
            }

            if (game.userPoints == 5)
            {
                Assert.AreEqual(5, game.userPoints);
                Assert.AreNotEqual(5, game.opponentPoints);
            }

        }

        [TestMethod]
        public void TestMethod6()
        {
            var game = new GameLogic(2, new List<int>() { 0, 0, 0, 0, 1, 1, 1, 1, 1 });

            Assert.AreEqual(4, game.opponentPoints);
            Assert.AreEqual(5, game.userPoints);
        }

        [TestMethod]
        public void TestMethod7()
        {
            var game = new GameLogic(2, new List<int>() { 1, 1, 1, 1, 1, 1, 1, 1, 1 });

            Assert.AreEqual(4, game.opponentPoints);
            Assert.AreEqual(5, game.userPoints);
        }

        [TestMethod]
        public void TestMethod8()
        {
            var game = new GameLogic(2, new List<int>() { 0,0,0,0,0,0,0, 0 });

            Assert.AreEqual(5, game.opponentPoints);
        }
    }
}

public class GameLogic
{
    public int rounds = 0;
    public int userPoints = 0;
    public int opponentPoints = 0;

    public bool stop = false;

    private int _startGroup;
    private List<int> _answares; 

    private Random rand = new Random();

    public GameLogic(int startGroup, List<int> answares)
    {
        this._startGroup = startGroup;
        this._answares = answares;

        this.Start();
    }

    private void Start()
    {
        for (var i = 0; i < this._answares.Count; i++)
        {
            if (!stop)
            {
                this.CalculateResult(_answares[i]);     
            }
           
        }
    }

    private void CalculateResult(int i)
    {
        if (this._startGroup == 0)
        {
            if (i == 1)
            {
                userPoints++;
            }

            if (i == 0)
            {
                this._startGroup = 1;
            }

            if (userPoints == 5)
            {
                stop = true;
            }
        }

        if (this._startGroup == 1)
        {
            if(i == 1)
            {
                var opponentWin = rand.Next(2);

                if (opponentWin == 1 && opponentPoints == 0)
                {
                    opponentPoints++;
                    return;
                }

                if (userPoints == 4 && opponentPoints == 0)
                {
                    opponentPoints++;
                    return;
                }

                if (opponentPoints > 1)
                {
                    this._startGroup = 2;
                    return;
                }

                userPoints++;
            }

            if (i == 0)
            {
                if (opponentPoints == 0 || opponentPoints == 1)
                {
                    opponentPoints++;
                }
            }

            if (opponentPoints > 1)
            {
                this._startGroup = 2;
                return;
            }

            if (userPoints == 5)
            {
                stop = true;
            }
        }

        if (this._startGroup == 2)
        {
            if (i == 1)
            {
                var opponentWin = rand.Next(2);

                if (opponentWin == 1 && opponentPoints < 4)
                {
                    opponentPoints++;
                    return;
                }

                if (userPoints == 4 && opponentPoints < 4)
                {
                    opponentPoints++;
                    return;
                }

                userPoints++;
            }

            if (i == 0)
            {
                
                if (opponentPoints <= 4)
                {
                    opponentPoints++;
                }
            }

            if (userPoints == 5 || opponentPoints == 5)
            {
                stop = true;
            }
        }
    }
}
