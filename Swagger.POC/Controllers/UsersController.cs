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
        public IList<User> data = new List<User> {
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
        // Get: api/Users/1
        /// <response code="200">Countries returned OK</response>
        [HttpGet]
        [ResponseType(typeof(User))]
        public IHttpActionResult Get(int id)
        {
            var item = data.FirstOrDefault(o => o.Id == id);
            return Ok(item);
        }

        // Post: api/Users
        [ResponseType(typeof(User))]
        public IHttpActionResult Post([FromBody] User item)
        {
            return Ok(item);
        }
    }
}