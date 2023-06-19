using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace ReviewService.Model
{
    public class Neo4jEntity : IEntity
    {
        protected Neo4jEntity()
        {
            LastUpdated = DateTime.UtcNow;
        }

        /// <summary>
        /// Gets or sets the label on a Neo4jEntity
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        public string Label { get; protected set; }

        /// <summary>
        /// Gets or sets the last updated.
        /// </summary>
        /// <value>
        /// The last updated.
        /// </value>
        [JsonIgnore]
        [XmlIgnore]
        public DateTimeOffset LastUpdated { get; set; }
    }
}
