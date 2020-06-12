// Copyright(c) 2019 Nuno Fachada
// Distributed under the MIT License(See accompanying file LICENSE or copy 
// at http://opensource.org/licenses/MIT)
using System;

namespace Roguelike
{
    /// <summary>
    /// Static class containing some useful functions for procedural generation.
    /// </summary>
    public static class ProcGenFunctions
    {
        /// <summary>
        /// Logistic function
        /// </summary>
        /// <param name="x">Input variable x</param>
        /// <param name="L">The curve's maximum value</param>
        /// <param name="x0">The x-value of the sigmoid's midpoint</param>
        /// <param name="k">The steepness of the curve</param>
        /// <returns>The y output variable</returns>
        public static double Logistic(double x, double L, double x0, double k)
        {
            return L / (1 + Math.Exp(-k * (x - x0)));
        }
    }
}