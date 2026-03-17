using System;
using System.Collections.Generic;
using System.Drawing;

namespace SpaceShooters
{
    internal class EnemyAI
    {
        private int numOfActions, spaceShipSize, columns;
        private List<Point> actions;
        private const double alpha = 0.01, gamma = 0.6;
        private double epsilon = 1.0;
        private Dictionary<string, List<double>> Q, tempQ;

        public EnemyAI(int gameAreaWidth, int gameAreaHeight, int spaceshipSize)
        {
            this.spaceShipSize = spaceshipSize;
            columns = ValToBlock(gameAreaWidth);
            if (gameAreaWidth % spaceShipSize != 0)
            {
                columns++;
            }
            int[] dx = { 0, 1, 1, 1, 0, -1, -1, -1 };
            int[] dy = { -1, -1, 0, 1, 1, 1, 0, -1 };
            actions = new List<Point>();
            //for (int x = 1; x <= gameAreaWidth - spaceShipSize; x++)
            //{
            //    for (int y = 1; y <= gameAreaHeight - spaceShipSize * 2; y++)
            //    {
            //        for (int k = 0; k < 8; k++)
            //        {
            //            actions.Add(new Point(dx[k] * d, dy[k] * d));
            //        }
            //    }
            //}
            for (int d = 1; d <= 10; d++)
            {
                for (int k = 0; k < 8; k++)
                {
                    actions.Add(new Point(dx[k] * d, dy[k] * d));
                }
            }
            numOfActions = actions.Count;
            Q = new Dictionary<string, List<double>>();
            tempQ = new Dictionary<string, List<double>>();
        }

        private int Reward(Enemy enemy, Enemy prevEnemy, List<Bullet> enemyBullets)
        {
            int reward = 0;
            bool[] colHasEnemyBullet = new bool[columns];
            for (int i = 0; i < columns; i++)
            {
                colHasEnemyBullet[i] = false;
            }
            foreach (Bullet bullet in enemyBullets)
            {
                int bulletCol = ValToBlock(bullet.X);
                if (!colHasEnemyBullet[bulletCol])
                {
                    reward += 10;
                    colHasEnemyBullet[bulletCol] = true;
                }
            }
            if (enemy.Life < prevEnemy.Life)
            {
                reward -= 100;
            }
            return reward;
        }

        private int ValToBlock(int val)
        {
            return val / spaceShipSize;
        }

        private string State(Enemy enemy, Player player, List<Bullet> enemyBullets, List<Bullet> playerBullets)
        {
            int enemyBlockX = ValToBlock(enemy.X), enemyBlockY = ValToBlock(enemy.Y);
            int playerBlockX = ValToBlock(player.X);
            string state = $"enemy_pos:({enemyBlockX},{enemyBlockY})|player_pos:{playerBlockX}|enemy_is_safe:";
            if (enemy.Life >= player.Life)
            {
                state += "true";
            }
            else
            {
                state += "false";
            }
            state += "|enemy_bullets_pos:[";
            for (int i = 0; i < enemyBullets.Count; i++)
            {
                int enemyBulletBlockX = ValToBlock(enemyBullets[i].X);
                int enemyBulletBlockY = ValToBlock(enemyBullets[i].Y);
                state += $"({enemyBulletBlockX},{enemyBulletBlockY})";
                if (i < enemyBullets.Count - 1)
                {
                    state += ",";
                }
            }
            state += "]|player_bullets_pos:[";
            for (int i = 0; i < playerBullets.Count; i++)
            {
                int playerBulletBlockX = ValToBlock(playerBullets[i].X);
                int playerBulletBlockY = ValToBlock(playerBullets[i].Y);
                state += $"({playerBulletBlockX},{playerBulletBlockY})";
                if (i < playerBullets.Count - 1)
                {
                    state += ",";
                }
            }
            state += "]";
            return state;
        }

        private Dictionary<string, List<double>> AddQState(Dictionary<string, List<double>> Q, string state)
        {
            if (!Q.ContainsKey(state))
            {
                Q[state] = new List<double>();
                for (int a = 0; a < numOfActions; a++)
                {
                    Q[state].Add(0);
                }
            }
            return Q;
        }

        public int Action(Enemy enemy, Player player, List<Bullet> enemyBullets, List<Bullet> playerBullets)
        {
            string state = State(enemy, player, enemyBullets, playerBullets);
            Random rn = new Random();
            int action = -1;
            if (rn.NextDouble() < epsilon)
            {
                action = rn.Next(numOfActions);
            }
            else
            {
                double maxQ = double.MinValue;
                Q = AddQState(Q, state);
                for (int a = 0; a < numOfActions; a++)
                {
                    if (Q[state][a] > maxQ)
                    {
                        maxQ = Q[state][a];
                        action = a;
                    }
                }
            }
            return action;
        }

        public Point GetAction(int action)
        {
            return actions[action];
        }

        private double MaxQ(Dictionary<string, List<double>> Q, string state)
        {
            double mxQ = double.MinValue;
            for (int a = 0; a < numOfActions; a++)
            {
                mxQ = Math.Max(mxQ, Q[state][a]);
            }
            return mxQ;
        }

        public void UpdateQTable(int action, Enemy prevEnemy, Player prevPlayer, List<Bullet> prevEnemyBullets, List<Bullet> prevPlayerBullets, Enemy currEnemy, Player currPlayer, List<Bullet> currEnemyBullets, List<Bullet> currPlayerBullets, int rwd = 0)
        {
            string prevState = State(prevEnemy, prevPlayer, prevEnemyBullets, prevPlayerBullets);
            string nextState = State(currEnemy, currPlayer, currEnemyBullets, currPlayerBullets);
            int reward = rwd;
            if (rwd == 0)
            {
                reward = Reward(currEnemy, prevEnemy, currEnemyBullets);
            }
            tempQ = AddQState(tempQ, prevState);
            tempQ = AddQState(tempQ, nextState);
            tempQ[prevState][action] += alpha * (reward + gamma * MaxQ(tempQ, nextState) - tempQ[prevState][action]);
        }

        public void NextEpoch()
        {
            foreach (string state in tempQ.Keys)
            {
                Q = AddQState(Q, state);
                for (int a = 0; a < tempQ[state].Count; a++)
                {
                    Q[state][a] = tempQ[state][a];
                }
            }
            epsilon -= 0.01;
        }
    }
}
