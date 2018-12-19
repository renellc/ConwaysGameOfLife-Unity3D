# Conway's Game of Life (implemented in Unity3D)

Conway's Game of Life (or just 'Life') is an example of cellular automaton, in which cells in a grid are either 'alive' or 'dead' based on some predetermined rules. It is a zero-player game in which the player chooses an initial state for the grid and then observes how the cells evolve and mutate over time. A useful application of cellular automaton is procedurally generating levels (specifically cave systems) that look and feel natural. This is an advantage over Binary Space Partitioning in which levels generated often feel carefully generated.

## Rules
1.) If a living cell has 0 or 1 living neighbors, the cell dies (this is known as underpopulation).

2.) If a living cell has 2 or 3 living neighbors, the cell lives on to the next cycle.

3.) If a living cell has more than 3 living neighbors, the cell dies (this is known as overpopulation).

4.) If a cell is dead but has exactly 3 living neighbors, the cell comes to life.
