namespace Swagger.POC.Controllers
{
    using Models;
    using Swashbuckle.Swagger.Annotations;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web.Http;
    using System.Web.Http.Description;

    public class UsersController : ApiController
    {
        private IList<User> data = new List<User> {
            new User() { Id=12341,Name="sk1",Address="Hyd1"},
            new User() { Id=12342,Name="sk2",Address="Hyd2"},
            new User() { Id=12343,Name="sk3",Address="Hyd3"},
            new User() { Id=12344,Name="sk4",Address="Hyd4"},
            new User() { Id=12345,Name="sk5",Address="Hyd5"},
            new User() { Id=12346,Name="sk6",Address="Hyd6"},
            new User() { Id=12347,Name="sk7",Address="Hyd7"},
            new User() { Id=12348,Name="sk8",Address="Hyd8"},
        };

        //If your API method can return multiple types, i.e. in the case of an error, 
        //then you can use the new SwaggerResponse attribute

        /// <summary>
        /// Get users.
        /// </summary>
        /// <remarks>
        /// Note that the key is a Id and its an integer.
        /// 
        /// Get: api/Users
        ///  
        ///     Get /User  (User List)
        ///          [
        ///             {
        ///             "id": 011,
        ///             "name": "admin1",
        ///             "address": "hyderabad1"
        ///             },
        ///             {
        ///             "id": 021,
        ///             "name": "admin2",
        ///             "address": "hyderabad2"
        ///             }
        ///          ]
        /// 
        /// </remarks>
        /// <returns>User Item</returns>
        /// <response code="200">Returns user item</response>
        /// <response code="400">If the item is null</response>
        /// 
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IEnumerable<User>))]
        [SwaggerResponseExamples(typeof(IEnumerable<User>), typeof(UserExamples))]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(IEnumerable<User>))]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(IEnumerable<User>))]
        public IHttpActionResult Get()
        {
            if (data == null || data?.Count <= 0)
                return NotFound();

            return Ok(data);
        }

        //One way of describing the response code and content for Swashbuckle is 
        //using a combination of XML comments, and the ResponseType attribute
        //Get: api/Users/1

        /// <summary>
        /// Get user by Id.
        /// </summary>
        /// <remarks>
        /// Note that the key is a Id and its an integer.
        /// 
        /// Get: api/Users/1
        ///  
        ///     Get /User
        ///     {
        ///        "Id": 12341,
        ///        "name": "sk1",
        ///        "Address": "Hyd1"
        ///     }
        /// 
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>User Item</returns>
        /// <response code="200">Returns user item</response>
        /// <response code="400">If the item is null</response>
        [HttpGet]
        [ResponseType(typeof(User))]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(User))]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(User))]
        public IHttpActionResult Get(int id)
        {
            var item = data.FirstOrDefault(o => o.Id == id);
            return Ok(item);
        }

        // Post: api/Users
        /// <summary>
        /// Creates a User.
        /// </summary>
        /// <remarks>
        /// Note that the key is a Id and its an integer.
        ///  
        ///     POST /User
        ///     {
        ///        "Id": 212341,
        ///        "name": "2sk1",
        ///        "Address": "2Hyd1"
        ///     }
        /// 
        /// </remarks>
        /// <param name="item"></param>
        /// <returns>New Created User Item</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        /// 
        [ResponseType(typeof(User))]
        [SwaggerResponse(HttpStatusCode.BadRequest, Type = typeof(User))]
        [SwaggerResponse(HttpStatusCode.Created, Type = typeof(User))]
        public IHttpActionResult Post([FromBody] User item)
        {
            return Ok(item);
        }
    }
}