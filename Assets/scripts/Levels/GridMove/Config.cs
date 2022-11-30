using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Config {
    private static bool InstantFeedback = false;
    private static int levelNr = 1;

    private static string name = "Anon";

    private static readonly List<List<(int x, int y, bool goal)>> Levels = new() {
        new() {
            (2, 3, false),
            (2, 2, false),
            (2, 1, true)},
        new() {
            (4, 2, false),
            (3, 2, false),
            (2, 2, false),
            (2, 3, false),
            (1, 3, false),
            (0, 3, false),
            (0, 4, true)},
        new(){
            (3, 1, false),
            (2, 1, false),
            (1, 1, false),
            (1, 2, true)},
        new() {
            (2, 2, false),
            (2, 3, false),
            (2, 4, false),
            (2, 0, false),
            (2, 1, false),
            (1, 1, true)},
        new() {
            (0, 0, false),
            (0, 1, false),
            (1, 1, false),
            (1, 2, false),
            (2, 2, false),
            (3, 2, false),
            (3, 1, false),
            (4, 1, false),
            (4, 0, true)
        },
        new() {
            (2, 2, false),
            (1, 2, false),
            (1, 3, false),
            (2, 3, false),
            (3, 3, false),
            (3, 2, false),
            (3, 1, false),
            (2, 1, false),
            (1, 1, false),
            (0, 1, false),
            (0, 2, false),
            (0, 3, false),
            (0, 4, true)
        },
        new() {
            (1, 0, false),
            (1, 1, false),
            (1, 2, false),
            (1, 3, false),
            (1, 4, false),
            (0, 4, false),
            (4, 4, false),
            (3, 4, false),
            (2, 4, true)
        },
        new() {
            (0, 1, false),
            (0, 2, false),
            (1, 2, false),
            (1, 3, false),
            (2, 3, false),
            (2, 4, false),
            (2, 0, false),
            (1, 0, true)
        },
        new() {
            (2, 2, false),
            (3, 2, false),
            (4, 2, false),
            (0, 2, false),
            (1, 2, false),
            (2, 2, false),
            (3, 2, false),
            (4, 2, false),
            (0, 2, false),
            (1, 2, false),
            (2, 2, false),
            (3, 2, false),
            (4, 2, false),
            (4, 3, true)
        }
    };

    private static readonly List<int[,]> colors = new() {
        new int[5, 5] {
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0}},
        new int[5, 5] {
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0}},
        new int[5, 5] {
            {0, 0, 0, 0, 0},
            {0, 2, 0, 0, 0},
            {0, 1, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0}},
        new int[5, 5] {
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {3, 0, 3, 3, 3},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0}},
        new int[5, 5] {
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 4, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0}},
        new int[5, 5] {
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0}},
        new int[5, 5] {
            {3, 3, 3, 3, 0},
            {0, 0, 0, 0, 4},
            {3, 3, 3, 3, 3},
            {3, 3, 3, 3, 0},
            {3, 3, 3, 3, 0}},
        new int[5, 5] {
            {0, 2, 5, 0, 0},
            {0, 0, 2, 5, 0},
            {4, 0, 0, 2, 2},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0}},
        new int[5, 5] {
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0}}
    };

    private static readonly List<int[,]> gameArea = new() {
        new int[5, 5] {
            {1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1}},
        new int[5, 5] {
            {1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1}},
        new int[5, 5] {
            {1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1}},
        new int[5, 5] {
            {1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1},
            {2, 1, 1, 1, 2},
            {1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1}},
        new int[5, 5] {
            {1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1}},
        new int[5, 5] {
            {1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1}},
        new int[5, 5] {
            {1, 1, 1, 1, 3},
            {1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1},
            {1, 1, 1, 1, 3}},
        new int[5, 5] {
            {1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1},
            {2, 1, 1, 1, 2},
            {1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1}},
        new int[5, 5] {
            {1, 1, 3, 1, 1},
            {1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1},
            {1, 1, 3, 1, 1}}
    };

    public static bool getInstantFeedback(){
        return InstantFeedback;
    }

    public static void setInstantFeedback(bool a){
        InstantFeedback = a;
    }

    public static int getLevelNr() {
        return levelNr;
    }

    public static void setName(string a){
        name = a;
    }

    public static string getName() {
        return name;
    }

    public static void incrementLevelNr(){
        levelNr++;
    }
    public static void resetLevelNr(){
        levelNr = 1;
    }

    public static List<(int x, int y, bool goal)> getAllowedMoves() {
        return Levels[levelNr - 1];
    }

    public static int[,] getGameAreaColors() {
        return colors[levelNr - 1];
    }

    public static int[,] getGameArea() {
        return gameArea[levelNr - 1];
    }

    public static bool getWasFinalLevel() {
        return Levels.Count + 1 == levelNr;
    }
}
