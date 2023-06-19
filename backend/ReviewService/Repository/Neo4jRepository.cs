using Neo4j.Driver;
using ReviewService.Model;
using System.Linq.Expressions;
using IEntity = ReviewService.Model.IEntity;

namespace ReviewService.Repository
{
    public class Neo4jRepository<TEntity> : IRepository<TEntity>
    where TEntity : Neo4jEntity, IEntity, new()
    {
        protected readonly IDriver _driver;

        public Neo4jRepository()
        {
            _driver = GraphDatabase.Driver("neo4j+s://05b6487c.databases.neo4j.io:7687", AuthTokens.Basic("neo4j", "9oCVCDhxkBNsNU3HkXQbx3BmiBdQR4ELpTlQt2zR2zo"));
        }

        public virtual async Task<IEnumerable<TEntity>> All()
        {
            using var session = _driver.AsyncSession();
            var entity = new TEntity();

            var result = await session.RunAsync(
                $"MATCH (e:{entity.Label}) RETURN e"
            );

            return await result.ToListAsync(record => record["e"].As<TEntity>());
        }

        public virtual async Task<IEnumerable<TEntity>> Where(Expression<Func<TEntity, bool>> query)
        {
            using var session = _driver.AsyncSession();
            string name = query.Parameters[0].Name;
            TEntity entity = new TEntity();
            var newQuery = PredicateRewriter.Rewrite(query, "e");

            var result = await session.RunAsync(
                $"MATCH (e:{entity.Label}) WHERE {newQuery} RETURN e"
            );

            return await result.ToListAsync(record => record["e"].As<TEntity>());
        }

        public virtual async Task<TEntity> Single(Expression<Func<TEntity, bool>> query)
        {
            var results = await Where(query);
            return results.FirstOrDefault();
        }

        public virtual async Task Add(TEntity item)
        {
            using var session = _driver.AsyncSession();
            await session.RunAsync(
                $"CREATE (e:{item.Label} $item)",
                new { item }
            );
        }

        public virtual async Task Update(Expression<Func<TEntity, bool>> query, TEntity newItem)
        {
            using var session = _driver.AsyncSession();
            string name = query.Parameters[0].Name;
            TEntity itemToUpdate = await Single(query);
            CopyValues(itemToUpdate, newItem);

            await session.RunAsync(
                $"MATCH ({name}:{newItem.Label}) WHERE {query} SET {name} = $item",
                new { item = itemToUpdate }
            );
        }

        public virtual async Task Patch(Expression<Func<TEntity, bool>> query, TEntity item)
        {
            using var session = _driver.AsyncSession();
            string name = query.Parameters[0].Name;

            await session.RunAsync(
                $"MATCH ({name}:{item.Label}) WHERE {query} SET {name} = $item",
                new { item }
            );
        }

        public virtual async Task Delete(Expression<Func<TEntity, bool>> query)
        {
            using var session = _driver.AsyncSession();
            string name = query.Parameters[0].Name;
            TEntity entity = new TEntity();

            await session.RunAsync(
                $"MATCH ({name}:{entity.Label}) WHERE {query} DELETE {name}"
            );
        }

        public virtual async Task Relate<TEntity2, TRelationship>(string query1, string query2, TRelationship relationship, DateTime date, int value)
            where TEntity2 : Neo4jEntity, new()
            where TRelationship : Neo4jRelationship, new()
        {
            using var session = _driver.AsyncSession();
            string name1 = "e1";
            TEntity entity1 = new TEntity();
            string name2 = "e2";
            TEntity2 entity2 = new TEntity2();

            await session.RunAsync(
                $"MERGE ({name1}:{entity1.Label} {query1})MERGE ({name2}:{entity2.Label} {query2}) " +
                $"MERGE ({name1})-[r:{relationship.Name}]->({name2}) SET r.value = $rating SET r.date = $ratingDate",
                new { rating= value, ratingDate=date }
            );
        }

        public virtual async Task<IEnumerable<TEntity2>> GetRelated<TEntity2, TRelationship>(Expression<Func<TEntity, bool>> query1, Expression<Func<TEntity2, bool>> query2, TRelationship relationship)
            where TEntity2 : Neo4jEntity, new()
            where TRelationship : Neo4jRelationship, new()
        {
            using var session = _driver.AsyncSession();
            string name1 = query1.Parameters[0].Name;
            TEntity entity1 = new TEntity();
            string name2 = query2.Parameters[0].Name;
            TEntity2 entity2 = new TEntity2();
            var newQuery2 = PredicateRewriter.Rewrite(query2, "e");

            var result = await session.RunAsync(
                $"MATCH ({name1}:{entity1.Label})-[r:{relationship.Name}]->({name2}:{entity2.Label}) " +
                $"WHERE {query1} AND {newQuery2} RETURN {name2}"
            );

            return await result.ToListAsync(record => record[name2].As<TEntity2>());
        }

        public async Task<GetRatingsForAccommodationResponse> GetAccommodationRatings(string nodeQuery, string whereQuery, string objectId)
        {
            string query = $" MATCH ({nodeQuery})-[r]-()" +
                           $"{whereQuery}" +
                           $"RETURN n, r, startNode(r) as relatedEntity";
            using var session = _driver.AsyncSession();
            var result = await session.RunAsync(query);
            List<AccommodationRating> ratings = new List<AccommodationRating>();
            double average = 0;
            while (await result.FetchAsync())
            {
                var node = result.Current["n"].As<INode>();
                var relationship = result.Current["r"].As<IRelationship>();
                var relatedEntity = result.Current["relatedEntity"].As<INode>();

                // Access the properties of the node, relationship, and related entity as needed
                var rating = relationship.Properties["value"].As<double>();
                var userId = relatedEntity.Properties["guestId"].As<string>();
                AccommodationRating accRating = new AccommodationRating() { Rating = rating, UserId = userId};
                ratings.Add(accRating);
                average += rating;
            }

            average /= ratings.Count();
            GetRatingsForAccommodationResponse response = new GetRatingsForAccommodationResponse() { AverageRating = average };
            response.Ratings.AddRange(ratings);
            return response;


        }


        public async Task<GetRatingsForHostResponse> GetHostRatings(string nodeQuery, string whereQuery, string objectId)
        {
            string query = $" MATCH ({nodeQuery})-[r]-()" +
                           $"{whereQuery}" +
                           $"RETURN n, r, startNode(r) as relatedEntity";
            using var session = _driver.AsyncSession();
            var result = await session.RunAsync(query);
            List<HostRating> ratings = new List<HostRating>();
            double average = 0;
            while (await result.FetchAsync())
            {
                var node = result.Current["n"].As<INode>();
                var relationship = result.Current["r"].As<IRelationship>();
                var relatedEntity = result.Current["relatedEntity"].As<INode>();

                // Access the properties of the node, relationship, and related entity as needed
                var rating = relationship.Properties["value"].As<double>();
                var userId = relatedEntity.Properties["guestId"].As<string>();
                HostRating accRating = new HostRating() { Rating = rating, UserId = userId };
                ratings.Add(accRating);
                average += rating;
            }
            average /= ratings.Count();
            GetRatingsForHostResponse response = new GetRatingsForHostResponse() { AverageRating = average };
            response.Ratings.AddRange(ratings);
            return response;
        }


        public async Task<GetRecommendedAccommodationsRequest> GetReccommendedAccommodations(string guestId)
        {
            string query = $" MATCH (g:Guest)-[r1:RATE]->(a:Accommodation) " +
                           $"WHERE g.guestId = '{guestId}' " +
                           $" WITH g, COLLECT(DISTINCT a.accommodationId) AS reservedAccommodations " +
                           $"MATCH (g)-[r2:RATE]->(a2:Accommodation)<-[r3:RATE]-(g2:Guest) " +
                           $"WHERE a2.accommodationId IN reservedAccommodations AND ABS(r2.value - r3.value) <= 1 " +
                           $"WITH g2, a2 " +
                           $" MATCH (g2)-[r4:RATE]->(a3:Accommodation) " +
                           $"WHERE r4.value >= 3 AND r4.date >= datetime().epochSeconds - 7776000 " +
                           $"WITH a3, COUNT(*) AS ratingsCount " +
                           $" WHERE ratingsCount <= 5 " +
                           $" RETURN a3";

            using var session = _driver.AsyncSession();
            var result = await session.RunAsync(query);
            while (await result.FetchAsync())
            {
                var node = result.Current["a3"].As<INode>();


                // Access the properties of the node, relationship, and related entity as needed
                var accId = node.Properties["accommodationId"].As<string>();
            }
            return null;

        }  

        public virtual async Task DeleteRelationship<TEntity2, TRelationship>(Expression<Func<TEntity, bool>> query1, Expression<Func<TEntity2, bool>> query2, TRelationship relationship)
            where TEntity2 : Neo4jEntity, new()
            where TRelationship : Neo4jRelationship, new()
        {
            using var session = _driver.AsyncSession();
            string name1 = query1.Parameters[0].Name;
            TEntity entity1 = new TEntity();
            string name2 = query2.Parameters[0].Name;
            TEntity2 entity2 = new TEntity2();

            await session.RunAsync(
                $"MATCH ({name1}:{entity1.Label})-[r:{relationship.Name}]->({name2}:{entity2.Label}) " +
                $"WHERE {query1} AND {query2} DELETE r"
            );
        }

        public void CopyValues(TEntity target, TEntity source)
        {
            Type t = typeof(TEntity);
            var properties = t.GetProperties().Where(prop => prop.CanRead && prop.CanWrite);

            foreach (var prop in properties)
            {
                var value = prop.GetValue(source, null);
                if (value != null)
                    prop.SetValue(target, value, null);
            }
        }
    }
}
