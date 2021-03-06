/***
 * 
 *   MazeGenerator - It generates mazes for OSHWDEM's robot contest.
 * 
 *   Copyright (C) 2015 Bricolabs (bricolabs.cc) & Rafa Couto (aka caligari)
 * 
 *   This program is free software: you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation, either version 3 of the License, or
 *   (at your option) any later version.
 *
 *   This program is distributed in the hope that it will be useful,
 *   but WITHOUT ANY WARRANTY; without even the implied warranty of
 *   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *   GNU General Public License for more details.
 *
 *   You should have received a copy of the GNU General Public License
 *   along with this program.  If not, see <http://www.gnu.org/licenses/>.
 * 
 */

using System;

namespace Treboada.Net.Ia
{
	class Oshwdem
	{
		public static void Main (string[] args)
		{
			Oshwdem oshwdem = new Oshwdem ();
			oshwdem.Run ();
		}

		private void Run()
		{
			// create a square maze with 13 cells on every side
			Maze maze = CreateMaze (16);

			// the finish door
			maze.UnsetWall (8, 8, Maze.Direction.S);

			// prepare de generator
			MazeGenerator generator = SetupGenerator (maze);

			// generate from top-left corner, next to the starting cell
			generator.Generate (1, 0);

			// show the version
			Console.WriteLine ("OSHWDEM Maze Generator v{0}.{1} R{2}", Version.Major, Version.Minor, Version.Revision);

			// output to the console
			Console.Write (maze);

			// wait for <enter>
			Console.ReadLine ();
		}


		private Maze CreateMaze(int side)
		{
			// square and fully walled
			Maze maze = new Maze (side, side, Maze.WallInit.Full);

			// set the starting cell
			maze.UnsetWall (0, 0, Maze.Direction.E);

			// clear the walls inside 2x2 center cells
			maze.UnsetWall (7, 7, Maze.Direction.S);
			maze.UnsetWall (7, 7, Maze.Direction.E);
			maze.UnsetWall (8, 7, Maze.Direction.S);
			maze.UnsetWall (7, 8, Maze.Direction.E);

			return maze;
		}


		private MazeGenerator SetupGenerator (Maze maze)
		{
			// pretty algorithm to generate mazes
			DepthFirst generator = new DepthFirst (maze);

			// starting cell is set
			generator.SetVisited (0, 0, true);

			// dont enter into the 3x3 center
			generator.SetVisited (7, 7, true);
			generator.SetVisited (8, 7, true);
			generator.SetVisited (7, 8, true);
			generator.SetVisited (8, 8, true);

			return generator;
		}


		public static Version Version { 
			get { return System.Reflection.Assembly.GetEntryAssembly().GetName().Version; } 
		}
	}
}
