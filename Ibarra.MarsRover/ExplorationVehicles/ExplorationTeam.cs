using System;
using System.Collections.Generic;
using System.Text;
using Ibarra.MarsRover.Landscapes;
using Ibarra.MarsRover.Navigation;

namespace Ibarra.MarsRover.ExplorationVehicles {
    /// <summary>
    /// A collection of explorers that make up a team.
    /// </summary>
    public class ExplorationTeam : List<Explorer> {
        /// <summary>
        /// The region that this exploration team is responsible for exploring.
        /// </summary>
        public IDeploymentZoneChart DeploymentZoneChart { get; }

        /// <summary>
        /// Constructs an exploration team responsible for the provided region.
        /// </summary>
        /// <param name="deploymentZoneChart">The region/zone that this exploration crew is responsible for exploring.
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown if deployment zone chart is null.</exception>
        public ExplorationTeam(IDeploymentZoneChart deploymentZoneChart)
            => DeploymentZoneChart = deploymentZoneChart ??
                                     throw new ArgumentNullException(nameof(deploymentZoneChart),
                                         "Deployment zone chart cannot be null.");

        /// <summary>
        /// Generates a report of all explorers currently deployed and their current locations.
        /// </summary>
        /// <returns>A string representation of the current location of all currently deployed explorers.</returns>
        public string GenerateExplorationReport() {
            var reports = new StringBuilder();
            foreach (var explorer in this) {
                if (!explorer.IsLaunched) {
                    reports.Append("Explorer not launched.");
                } else {
                    reports.AppendFormat("{0} {1} {2}", explorer.Position.X, explorer.Position.Y,
                        explorer.Heading.GetString());
                }

                reports.AppendLine();
            }

            return reports.ToString();
        }
    }
}